using API.Model.BlogModel;
using API.Model.UserModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        private readonly UnitOfWork _unitOfWork;

        public BlogController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("GetTotalRecords")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConst.Customer)]
        public IActionResult SearchBlogRecords([FromQuery] RequestSearchBlogModel requestSearchBlogModel)
        {
            try
            {
                var sortBy = requestSearchBlogModel.SortContent != null ? requestSearchBlogModel.SortContent?.sortBlogBy.ToString() : null;
                var sortType = requestSearchBlogModel.SortContent != null ? requestSearchBlogModel.SortContent?.sortBlogType.ToString() : null;
                Expression<Func<Blog, bool>> filter = x =>
                    (string.IsNullOrEmpty(requestSearchBlogModel.Title) || x.Title.Contains(requestSearchBlogModel.Title)) &&
                    (x.ManagerId == requestSearchBlogModel.ManagerId || requestSearchBlogModel.ManagerId == null);
                var totalRecords = _unitOfWork.BlogRepository.Count(filter);

                var response = new
                {
                    TotalRecords = totalRecords
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong appears in total blog");
            }

        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConst.Customer )]
        public IActionResult SearchBlog([FromQuery] RequestSearchBlogModel requestSearchBlogModel)
        {
            try
            {
                var sortBy = requestSearchBlogModel.SortContent != null ? requestSearchBlogModel.SortContent?.sortBlogBy.ToString() : null;
                var sortType = requestSearchBlogModel.SortContent != null ? requestSearchBlogModel.SortContent?.sortBlogType.ToString() : null;
                Expression<Func<Blog, bool>> filter = x =>
                    (string.IsNullOrEmpty(requestSearchBlogModel.Title) || x.Title.Contains(requestSearchBlogModel.Title)) &&
                    (x.ManagerId == requestSearchBlogModel.ManagerId || requestSearchBlogModel.ManagerId == null);
                Func<IQueryable<Blog>, IOrderedQueryable<Blog>> orderBy = null;

                if (!string.IsNullOrEmpty(sortBy))
                {
                    if (sortType == SortBlogTypeEnum.Ascending.ToString())
                    {
                        orderBy = query => query.OrderBy(p => EF.Property<object>(p, sortBy));
                    }
                    else if (sortType == SortBlogTypeEnum.Descending.ToString())
                    {
                        orderBy = query => query.OrderByDescending(p => EF.Property<object>(p, sortBy));
                    }
                }
                var reponseBlog = _unitOfWork.BlogRepository.Get(
                    filter,
                    orderBy,
                    /*includeProperties: "",*/
                    pageIndex: requestSearchBlogModel.pageIndex,
                    pageSize: requestSearchBlogModel.pageSize, m => m.Manager
                    ).Select(x => x.toBlogDTO());
                return Ok(reponseBlog);
            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong appears in showing all blogs");
            }

        }

        [HttpGet("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConst.Admin)]
        public IActionResult GetBlogById(int id)
        {
            try
            {
                var Blog = _unitOfWork.BlogRepository.GetByID(id, m => m.Manager);
                if (Blog == null)
                {
                    return NotFound("Blog is not existed");
                }
                return Ok(Blog.toBlogDTO());

            }
            catch (Exception ex)
            {
                return BadRequest("Something wrong appears in getting blog id");
            }



        }

        [HttpPost]
        public IActionResult CreateBlog(RequestCreateBlogModel requestCreateBlogModel)
        {
            try
            {
                var user = _unitOfWork.UserRepository.GetByID(requestCreateBlogModel.ManagerId, m => m.Role);
                if (user.Role.Name != RoleConst.Manager)
                {
                    return BadRequest("Manager Id is not valid");
                }
                if (_unitOfWork.BlogRepository.Get(filter: x => x.Title.Equals(requestCreateBlogModel.Title)).FirstOrDefault() != null)
                {
                    return BadRequest("This Blog exists");
                }
                var Blog = requestCreateBlogModel.toBlogEntity();
                _unitOfWork.BlogRepository.Insert(Blog);
                _unitOfWork.Save();
                return Ok("Create successfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Create failed");
            }
        }

        [HttpPut]
        public IActionResult UpdateBlog(int id, RequestCreateBlogModel requestCreateBlogModel)
        {
            try
            {
                var existedBlog = _unitOfWork.BlogRepository.GetByID(id);
                if (existedBlog == null)
                {
                    return NotFound("Blog is not existed");
                }
                existedBlog.Description = requestCreateBlogModel.Description;
                existedBlog.ManagerId = requestCreateBlogModel.ManagerId;
                existedBlog.Title = requestCreateBlogModel.Title;
                existedBlog.Image = requestCreateBlogModel.Image;
                _unitOfWork.BlogRepository.Update(existedBlog);
                _unitOfWork.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Update failed");
            }

        }

        [HttpDelete]
        public IActionResult DeleteBlog(int id)
        {
            try
            {
                var existedBlog = _unitOfWork.BlogRepository.GetByID(id);
                if (existedBlog == null)
                {
                    return NotFound("Blog is not existed");
                }
                _unitOfWork.BlogRepository.Delete(existedBlog);
                _unitOfWork.Save();
                return Ok("Delete blog successfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Delete failed");
            }
        }
    }
}
