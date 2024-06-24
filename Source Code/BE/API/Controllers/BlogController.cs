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

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConst.Customer )]
        public IActionResult SearchBlog([FromQuery] RequestSearchBlogModel requestSearchBlogModel) 
        {
            var sortBy = requestSearchBlogModel.SortContent!=null ? requestSearchBlogModel.SortContent?.sortBlogBy.ToString() : null;
            var sortType = requestSearchBlogModel.SortContent!=null ? requestSearchBlogModel.SortContent?.sortBlogType.ToString() : null;
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
                pageSize: requestSearchBlogModel.pageSize, m=>m.Manager
                ).Select(x=>x.toBlogDTO());
            return Ok(reponseBlog);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = RoleConst.Admin)]
        public IActionResult GetBlogById(int id)
        {
            var Blog = _unitOfWork.BlogRepository.GetByID(id, m => m.Manager);
            if (Blog == null)
            {
                return NotFound("Blog is not existed");
            }

            return Ok(Blog.toBlogDTO());
        }

        [HttpPost]
        public IActionResult CreateBlog(RequestCreateBlogModel requestCreateBlogModel)
        {
            var user = _unitOfWork.UserRepository.GetByID(requestCreateBlogModel.ManagerId, m=>m.Role);
            if(user.Role.Name !=  RoleConst.Manager) 
            {
                return BadRequest("Manager Id is not valid");
            }
            var Blog = requestCreateBlogModel.toBlogEntity();
            _unitOfWork.BlogRepository.Insert(Blog);
            _unitOfWork.Save();
            return Ok("Create successfully");
        }

        [HttpPut]
        public IActionResult UpdateBlog(int id, RequestCreateBlogModel requestCreateBlogModel)
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

        [HttpDelete]
        public IActionResult DeleteBlog(int id)
        {
            var existedBlog = _unitOfWork.BlogRepository.GetByID(id);
            if (existedBlog==null)
            {
                return NotFound("Blog is not existed");
            }
            _unitOfWork.BlogRepository.Delete(existedBlog);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
