﻿using API.Model.UserModel;
using Repositories;
using Repositories.Entity;

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

        public static ReposneBlog toBlogDTO(this Blog blog)
        {
            return new ReposneBlog()
            {
                BlogId = blog.BlogId,
                Description = blog.Description,
                Manager = blog.Manager!=null? UserMapper.toUserDTO(blog.Manager):null,
                Title = blog.Title,
                Image = blog.Image,

            };
        }
    }
}
