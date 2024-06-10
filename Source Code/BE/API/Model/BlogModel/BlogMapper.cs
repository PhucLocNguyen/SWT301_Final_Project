using API.Model.UserModel;
using Repositories;

namespace API.Model.BlogModel
{
    public static class BlogMapper
    {
        public static Blog toBlogEntity(this RequestCreateBlogModel requestCreateBlogModel) 
        {
            return new Blog()
            {
                Description = requestCreateBlogModel.Description,
                ManagerId = requestCreateBlogModel.ManagerId,
                Title = requestCreateBlogModel.Title,
                Image = requestCreateBlogModel.Image,

            };
        }

        public static BlogDTO toBlogDTO(this Blog blog)
        {
            return new BlogDTO()
            {
                BlogId = blog.BlogId,
                Description = blog.Description,
                Manager = UserMapper.toUserDTO(blog.Manager),
                Title = blog.Title,
                Image = blog.Image,

            };
        }
    }
}
