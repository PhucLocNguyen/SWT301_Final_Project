using Microsoft.AspNetCore.Mvc;
using Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.PortableExecutable;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        readonly string[] monthNames = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        private readonly UnitOfWork _unitOfWork;

        public DashboardController(UnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet("RevenueByYear")]
        public IActionResult GetRevenueByYear(int? year)
        {
            try
            {
                
                Hashtable collections = new Hashtable();
                for (int i = 1; i <= 12; i++)
                {
                    int revenue = 0;
                    var PaymentByMonth = _unitOfWork.PaymentRepository.Get(filter:
                 x => x.CompletedAt.Value.Month.Equals(i) && x.CompletedAt.Value.Year.Equals(year) && x.Status.Equals("Paid")).ToList();
                    var RevenueByMonth = PaymentByMonth.GroupBy(x => x.CompletedAt.Value.Month).Select(x => new
                    {
                        month = i,
                        revenue = x.Sum(x => x.Amount)
                    });

                    collections.Add(i, RevenueByMonth.FirstOrDefault()?.revenue??0);
                }
                var data = collections.Cast<DictionaryEntry>().Select(entry => new
                {
                    month = monthNames[(int)entry.Key-1],
                    amount = entry.Value
                }).ToList();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong in GetRevenue");
            }
        }
        [HttpGet("RevenueByDate")]
        public IActionResult GetRevenueByDate(DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                Hashtable collections = new Hashtable();
                if (FromDate> ToDate)
                {
                    return BadRequest("The ToDate must be larger than FromDate");
                }
                long sum = 0;
                while (FromDate<= ToDate)
                {
                    var PaymentByDate = _unitOfWork.PaymentRepository.Get(x => x.CompletedAt.Value.Date == FromDate && x.Status.Equals("Paid")).ToList();
                    var RevenueByDate = PaymentByDate.GroupBy(x=>x.CompletedAt.Value).Select(x => new
                    {
                        Date = DateOnly.FromDateTime((DateTime)FromDate),
                        Revenue = x.Sum(x => x.Amount)
                    });
                    sum = (long)(sum + (RevenueByDate.FirstOrDefault()?.Revenue??0));
                    collections.Add(FromDate, RevenueByDate.FirstOrDefault()?.Revenue ?? 0);
                    FromDate = FromDate.Value.AddDays(1);
                }
                var totalMoney = new
                {
                    ToTal = "Total money from: "+ DateOnly.FromDateTime((DateTime)FromDate) + " to " + DateOnly.FromDateTime((DateTime)FromDate),
                    Revenue = sum
                };
                
                var data = collections.Cast<DictionaryEntry>().Select(entry => new
                {
                    Date = DateOnly.FromDateTime((DateTime)entry.Key),
                    amount = entry.Value
                }).OrderBy(x=>x.Date).ToList();
                return Ok(new
                {
                    totalMoney,
                    data
                });
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong in GetRevenue");
            }
        }
        [HttpGet("CountType")]
        public IActionResult GetCountByType(Month monthFromRequest, int year)
        {
            try
            {

                var TypeJewelleryObject = _unitOfWork.TypeOfJewellryRepository.Get().Select(x=>x.TypeOfJewelleryId).ToList();
                var responseData = new List<Object>();

                foreach (var item in TypeJewelleryObject)
                {
                    int i = 0;
                    var RequirementCurrentMonth = _unitOfWork.RequirementRepository.Get(filter: x => x.CreatedDate.Value.Month.Equals((int)monthFromRequest)
                 && x.CreatedDate.Value.Year.Equals(year)&&!x.Status.Equals("-1")).ToList();

                    var DesignHaveRequirement = _unitOfWork.DesignRepository.Get(filter:
                        x => x.TypeOfJewelleryId == item).Select(x => x.DesignId).ToList();
                    if ((int)monthFromRequest == 1)
                    {
                        var RequirementLastMonth = _unitOfWork.RequirementRepository.Get(filter: x => x.CreatedDate.Value.Month.Equals(12)
                        && x.CreatedDate.Value.Year.Equals(year - 1) && !x.Status.Equals("-1")).ToList();

                        var CountTypeCurrentMonth = RequirementCurrentMonth.Where(requirement => DesignHaveRequirement.Contains(requirement.DesignId)).ToList().Count;
                        var CountTypeLastMonth = RequirementLastMonth.Where(requirement => DesignHaveRequirement.Contains(requirement.DesignId)).ToList().Count;

                        responseData.Add(new
                        {
                            TypeOfJewelleryName = _unitOfWork.TypeOfJewellryRepository.GetByID(item).Name,
                            LastMonth = CountTypeLastMonth,
                            CurrentMonth = CountTypeCurrentMonth
                        });
                    }
                    else
                    {
                        var RequirementLastMonth = _unitOfWork.RequirementRepository.Get(filter: x => x.CreatedDate.Value.Month.Equals((int)monthFromRequest - 1)
                        && x.CreatedDate.Value.Year.Equals(year) && !x.Status.Equals("-1")).ToList();

                        var CountTypeCurrentMonth = RequirementCurrentMonth.Where(requirement => DesignHaveRequirement.Contains(requirement.DesignId)).ToList().Count;
                        var CountTypeLastMonth = RequirementLastMonth.Where(requirement => DesignHaveRequirement.Contains(requirement.DesignId)).ToList().Count;
                        responseData.Add(new
                        {
                            TypeOfJewelleryName = _unitOfWork.TypeOfJewellryRepository.GetByID(item).Name,
                            LastMonth = CountTypeLastMonth,
                            CurrentMonth = CountTypeCurrentMonth
                        });
                    }
                    i++;
                }
                return Ok(responseData);
                
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong in GetCountByType");
            }

        }
        [HttpGet("MostMasterGemstone")]
        public IActionResult GetMostMasterGemstone()
        {
            try
            {
                var MasterGemstone = _unitOfWork.MasterGemstoneRepository.Get().GroupBy(x => x.Kind).Select(x => new
                {
                    Name = x.Key,
                }).ToList();
                var responseData = new List<object>();
                foreach (var item in MasterGemstone)
                {
                    var DesignHaveMasterGemstone = _unitOfWork.DesignRepository.Get(filter: x=>x.MasterGemstoneId!=null&&x.ParentId!=null,includes: x=>x.MasterGemstone).ToList().
                        Where(x=>x.MasterGemstone.Kind.Equals(item.Name)).Select(x=>x.DesignId).ToList();
                    var Requirement = _unitOfWork.RequirementRepository.Get(x => !x.Status.Equals("-1")).ToList();
                    var CountMasterGemsonte = Requirement.Where(requirement => DesignHaveMasterGemstone.Contains(requirement.DesignId)).ToList().Count();
                    responseData.Add(new
                    {
                        masterGemstone = item.Name,
                        Amount = CountMasterGemsonte
                    });
                }
                responseData = responseData.OrderByDescending(x=>x.GetType().GetProperty("Amount").GetValue(x,null)).ToList();
                return Ok(responseData);
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong in GetNumberRequirementInMonth");
            }
            
        }

        [HttpGet("MostDesign")]
        public IActionResult GetMostDesign()
        {
            try
            {
                //Đếm số bản desgin mà requirement dùng
                var designCounts = _unitOfWork.DesignRepository.Get(filter: x => x.ParentId == null)
                    .GroupJoin(
                        _unitOfWork.RequirementRepository.Get(),
                        design => design.DesignId,
                        requirement => requirement.DesignId,
                        (design, requirements) => new
                        {
                            DesignId = design.DesignId,
                            Count = requirements.Count()
                        })
                        .OrderByDescending(x => x.Count);

                //Đếm design cha có bao nhiêu con 
                var parentDesigns = _unitOfWork.DesignRepository.Get(filter: x => x.ParentId == null).Select(x => x.DesignId);
                var childDesignCounts = _unitOfWork.DesignRepository.Get()
                    .GroupBy(x => x.ParentId)
                    .Select(x => new
                    {
                        ParentId = x.Key,
                        Count = x.Count()
                    }).ToList();

                var allParentDesignCounts = parentDesigns.GroupJoin(
                    childDesignCounts,
                    parent => parent,
                    child => child.ParentId,
                    (parent, children) => new
                    {
                        ParentId = parent,
                        Count = children.FirstOrDefault()?.Count ?? 0
                    })
                    .OrderByDescending(x => x.Count);
                // Đếm tổng 
                var Result = designCounts.GroupJoin(
                    allParentDesignCounts,
                    designCount => designCount.DesignId,
                    allParent => allParent.ParentId,
                    (designCounts, allParentCount) => new
                    {
                        DesignId = designCounts.DesignId,
                        Count = designCounts.Count + allParentCount.FirstOrDefault()?.Count ?? 0
                    }
                    ).OrderByDescending(x => x.Count).Take(3);
                return Ok(Result);
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong in GetMostDesign");
            }
        }

    }
    public enum Month
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12,
    }
}
