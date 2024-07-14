using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using API.Model.DesignModel;
using API.Model.RequirementModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Entity;
using SWP391Project.Services.WorkingBoard.Hubs;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RequirementController : ControllerBase
{
	private readonly UnitOfWork _unitOfWork;

	private readonly IHubContext<WorkingHub> _hubContext;

	public RequirementController(UnitOfWork unitOfWork, IHubContext<WorkingHub> hubContext)
	{
		_unitOfWork = unitOfWork;
		_hubContext = hubContext;
	}

	[HttpGet]
	public IActionResult SearchBlog([FromQuery] RequestSearchRequirementModel requestSearchRequirementModel)
	{
		RequestSearchRequirementModel requestSearchRequirementModel2 = requestSearchRequirementModel;
		try
		{
			string sortBy = ((requestSearchRequirementModel2.SortContent == null) ? null : requestSearchRequirementModel2.SortContent?.sortRequirementBy.ToString());
			string sortType = ((requestSearchRequirementModel2.SortContent == null) ? null : requestSearchRequirementModel2.SortContent?.sortRequirementType.ToString());
			Expression<Func<Requirement, bool>> filter = (Requirement x) => (string.IsNullOrEmpty(requestSearchRequirementModel2.Status) || x.Status.Equals(requestSearchRequirementModel2.Status)) && (x.Size == requestSearchRequirementModel2.Size || requestSearchRequirementModel2.Size == null) && ((int?)x.DesignId == requestSearchRequirementModel2.DesignId || requestSearchRequirementModel2.DesignId == null) && x.MaterialPriceAtMoment >= requestSearchRequirementModel2.FromMaterialPriceAtMoment && (x.MaterialPriceAtMoment <= requestSearchRequirementModel2.ToMaterialPriceAtMoment || requestSearchRequirementModel2.ToMaterialPriceAtMoment == null) && x.WeightOfMaterial >= requestSearchRequirementModel2.FromWeightOfMaterial && (x.WeightOfMaterial <= requestSearchRequirementModel2.ToWeightOfMaterial || requestSearchRequirementModel2.ToWeightOfMaterial == null) && x.StonePriceAtMoment >= requestSearchRequirementModel2.FromStonePriceAtMoment && (x.StonePriceAtMoment <= requestSearchRequirementModel2.ToStonePriceAtMoment || requestSearchRequirementModel2.ToStonePriceAtMoment == null) && x.MachiningFee >= requestSearchRequirementModel2.FromMachiningFee && (x.MachiningFee <= requestSearchRequirementModel2.ToMachiningFee || requestSearchRequirementModel2.ToMachiningFee == null) && x.TotalMoney >= requestSearchRequirementModel2.FromTotalMoney && (x.TotalMoney <= requestSearchRequirementModel2.ToTotalMoney || requestSearchRequirementModel2.ToTotalMoney == null);
			Func<IQueryable<Requirement>, IOrderedQueryable<Requirement>> orderBy = null;
			if (!string.IsNullOrEmpty(sortBy))
			{
				if (sortType == SortRequirementTypeEnum.Ascending.ToString())
				{
					orderBy = (IQueryable<Requirement> query) => query.OrderBy((Requirement p) => EF.Property<object>(p, sortBy));
				}
				else if (sortType == SortDesignTypeEnum.Descending.ToString())
				{
					orderBy = (IQueryable<Requirement> query) => query.OrderByDescending((Requirement p) => EF.Property<object>(p, sortBy));
				}
			}
			IEnumerable<ReponseRequirement> reponseDesign = from x in _unitOfWork.RequirementRepository.Get(filter, orderBy, "", requestSearchRequirementModel2.pageIndex, requestSearchRequirementModel2.pageSize)
				select x.toRequirementDTO();
			return Ok(reponseDesign);
		}
		catch (Exception)
		{
			return BadRequest("Something wrong in SearchBlog");
		}
	}

	[HttpGet("{id}")]
	public IActionResult GetRequirementById(int id, [FromQuery] int? userId)
	{
		try
		{
			if (userId.HasValue)
			{
				UserRequirement userRequirement = _unitOfWork.UserRequirementRepository.Get((UserRequirement x) => (int?)x.UsersId == userId && x.RequirementId == id).FirstOrDefault();
				if (userRequirement == null)
				{
					return BadRequest("You don't not allow to see detail this requirement");
				}
			}
			Requirement Requirement = _unitOfWork.RequirementRepository.GetByID(id);
			if (Requirement == null)
			{
				return NotFound("Requiremnet is not existed");
			}
			return Ok(Requirement.toRequirementDTO());
		}
		catch (Exception)
		{
			return BadRequest("Something wrong in GetRequirementById");
		}
	}

	[HttpPost]
	public IActionResult CreateRequirement(RequestCreateRequirementModel requestCreateRequirementModel)
	{
		try
		{
			string error = "";
			PropertyInfo[] properties = typeof(RequestCreateRequirementModel).GetProperties();
			PropertyInfo[] array = properties;
			foreach (PropertyInfo property in array)
			{
				if (property.PropertyType == typeof(decimal))
				{
					object value = property.GetValue(requestCreateRequirementModel);
					if ((decimal)value < 0m)
					{
						error = property.Name + " must be positive number";
						break;
					}
				}
			}
			if (!string.IsNullOrEmpty(error))
			{
				return BadRequest(error);
			}
			Requirement Requirement = requestCreateRequirementModel.toRequirementEntity();
			_unitOfWork.RequirementRepository.Insert(Requirement);
			_unitOfWork.Save();
			int requirementId = Requirement.RequirementId;
			_hubContext.Clients.All.SendAsync("ReceiveOrderCreate", requirementId);
			return Ok(Requirement);
		}
		catch (Exception)
		{
			return BadRequest("Create Requirement failed");
		}
	}

	[HttpPut("{id}")]
	public IActionResult UpdateRequirement([FromRoute] int id, RequestCreateRequirementModel requestCreateRequirementModel)
	{
		try
		{
			Requirement existedRequirement = _unitOfWork.RequirementRepository.GetByID(id);
			if (existedRequirement == null)
			{
				return NotFound("Requiremnet is not existed");
			}
			existedRequirement.Status = requestCreateRequirementModel.Status;
			if (requestCreateRequirementModel.Status.Equals("4"))
			{
				existedRequirement.ExpectedDelivery = DateOnly.FromDateTime(DateTime.Now.AddDays(14.0));
			}
			existedRequirement.Size = requestCreateRequirementModel.Size;
			existedRequirement.DesignId = requestCreateRequirementModel.DesignId.Value;
			existedRequirement.Design3D = requestCreateRequirementModel.Design3D;
			existedRequirement.WeightOfMaterial = requestCreateRequirementModel.WeightOfMaterial;
			existedRequirement.MaterialPriceAtMoment = requestCreateRequirementModel.MaterialPriceAtMoment;
			existedRequirement.StonePriceAtMoment = requestCreateRequirementModel.StonePriceAtMoment;
			existedRequirement.MachiningFee = requestCreateRequirementModel.MachiningFee;
			existedRequirement.TotalMoney = requestCreateRequirementModel.TotalMoney;
			existedRequirement.CustomerNote = requestCreateRequirementModel.CustomerNote;
			existedRequirement.StaffNote = requestCreateRequirementModel.StaffNote;
			existedRequirement.MasterGemStonePriceAtMoment = requestCreateRequirementModel.MasterGemStonePriceAtMoment;
			_unitOfWork.RequirementRepository.Update(existedRequirement);
			_unitOfWork.Save();
			_hubContext.Clients.All.SendAsync("ReceiveOrderUpdate", id);
			return Ok("Update Requirement successfully");
		}
		catch (Exception)
		{
			return BadRequest("Update Requirement failed");
		}
	}

	[HttpDelete]
	public IActionResult DeleteRequirement(int id)
	{
		Requirement existedRequirement = _unitOfWork.RequirementRepository.GetByID(id);
		if (existedRequirement == null)
		{
			return NotFound("Requiremnet is not existed");
		}
		try
		{
			_unitOfWork.RequirementRepository.Delete(existedRequirement);
			_unitOfWork.Save();
		}
		catch (DbUpdateException ex)
		{
			if (_unitOfWork.IsForeignKeyConstraintViolation(ex))
			{
				return BadRequest("Cannot delete this item because it is referenced by another entity");
			}
			throw;
		}
		return Ok("Delete Requirement successfully");
	}

	[HttpGet("GetRequirementByRole")]
	public IActionResult GetRequirement(int userId, string status)
	{
		string status2 = status;
		try
		{
			List<Requirement> RequirementByStatus = _unitOfWork.RequirementRepository.Get((Requirement x) => x.Status.Equals(status2)).ToList();
			List<int> UserRequirementByUserId = (from x in _unitOfWork.UserRequirementRepository.Get((UserRequirement x) => x.UsersId == userId)
				select x.RequirementId).ToList();
			List<ReponseRequirement> Result = (from x in RequirementByStatus
				where UserRequirementByUserId.Contains(x.RequirementId)
				select x.toRequirementDTO()).ToList();
			return Ok(Result);
		}
		catch (Exception)
		{
			return BadRequest("Something wrong in GetRequirement");
		}
	}

	[HttpGet("PriceOfRequirement")]
	public IActionResult GetPrice(int requirementId)
	{
		try
		{
			Requirement requirement = _unitOfWork.RequirementRepository.GetByID(requirementId);
			Design design = _unitOfWork.DesignRepository.Get((Design x) => x.DesignId == requirement.DesignId, null, null, null, (Design x) => x.MasterGemstone, (Design x) => x.Stone, (Design x) => x.Material).FirstOrDefault();
			if (requirement.Status.Equals("3") || requirement.Status.Equals("-2") || requirement.Status.Equals("-3"))
			{
				decimal price = design.Material.Price;
				decimal? weightOfMaterial = requirement.WeightOfMaterial;
				decimal MaterialPriceAtMoment = Math.Ceiling(((decimal?)price * weightOfMaterial).Value);
				decimal MasterGemStonePriceAtMoment = ((design.MasterGemstone != null) ? design.MasterGemstone.Price : 0m);
				decimal StonePriceAtMoment = ((design.Stone != null) ? design.Stone.Price : 0m);
				decimal? MachiningFee = requirement.MachiningFee;
				decimal? TotalMoney = (decimal?)(MaterialPriceAtMoment + MasterGemStonePriceAtMoment + StonePriceAtMoment) + MachiningFee;
				return Ok(new
				{
					RequirementId = requirementId,
					MaterialPriceAtMomentAnon = MaterialPriceAtMoment,
					MasterGemStonePriceAtMomentAnon = MasterGemStonePriceAtMoment,
					StonePriceAtMomentAnon = StonePriceAtMoment,
					MachiningFeeAnon = MachiningFee,
					TotalMoneyAnon = TotalMoney
				});
			}
			decimal MaterialPriceAtMoment2 = Math.Ceiling((requirement.MaterialPriceAtMoment * requirement.WeightOfMaterial).Value);
			decimal? MasterGemStonePriceAtMoment2 = requirement.MasterGemStonePriceAtMoment;
			decimal? StonePriceAtMoment2 = requirement.StonePriceAtMoment;
			decimal? MachiningFee2 = requirement.MachiningFee;
			decimal? TotalMoney2 = (decimal?)MaterialPriceAtMoment2 + MasterGemStonePriceAtMoment2 + StonePriceAtMoment2 + MachiningFee2;
			if (requirement.Status.Equals("4"))
			{
				var anonymousRequirementDeposit = new
				{
					RequirementId = requirementId,
					MaterialPriceAtMomentAnon = MaterialPriceAtMoment2,
					MasterGemStonePriceAtMomentAnon = MasterGemStonePriceAtMoment2,
					StonePriceAtMomentAnon = StonePriceAtMoment2,
					MachiningFeeAnon = MachiningFee2,
					TotalMoneyAnon = TotalMoney2,
					Deposit = Math.Ceiling((TotalMoney2 / (decimal?)2).Value)
				};
				return Ok(anonymousRequirementDeposit);
			}
			Payment paymentDeposit = _unitOfWork.PaymentRepository.Get((Payment x) => x.RequirementsId == (int?)requirementId && x.Status.Equals("Paid")).FirstOrDefault();
			var anonymousRequirement = new
			{
				RequirementId = requirementId,
				MaterialPriceAtMomentAnon = MaterialPriceAtMoment2,
				MasterGemStonePriceAtMomentAnon = MasterGemStonePriceAtMoment2,
				StonePriceAtMomentAnon = StonePriceAtMoment2,
				MachiningFeeAnon = MachiningFee2,
				TotalMoneyAnon = TotalMoney2,
				Deposit = paymentDeposit.Amount,
				PayTheRest = TotalMoney2 - (decimal?)paymentDeposit.Amount
			};
			return Ok(anonymousRequirement);
		}
		catch (Exception)
		{
			return BadRequest("Something wrong in GetPrice");
		}
	}
}
