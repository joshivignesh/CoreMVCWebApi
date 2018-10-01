using HexaBlogAPI.Models;
using HexaBlogAPI.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HexaBlogAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private IRepository<Blog> repository;

        public BlogsController(IRepository<Blog> repo)
        {
            this.repository = repo;
        }

        [EnableCors("MyPolicy")]

        //GET /api/blogs
        [ProducesResponseType(typeof(IEnumerable<Blog>), 200)]
        [HttpGet("", Name = "ListBlogs")]
        public IEnumerable<Blog> GetBlogs()
        {
           // throw new ArgumentException("Some Error");
            return this.repository.GetAll();
        }

        ////[HttpGet("", Name = "ListBlogs")]
        ////public IEnumerable<Blog> GetBlogs() => throw new ArgumentException("Some Error");// return this.repository.GetAll();

        //GET /api/blogs{id}
        // [ProducesResponseType( typeof(IEnumerable<Blog>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]

        [HttpGet("{id:int?}", Name = "GetBlog")]
        public async Task<ActionResult<Blog>> GetById(int? id)
        {
            //////if(id== null)
            //////{
            //////    return BadRequest("Id is required");
            //////}
            var item = await this.repository.GetById(id.Value);

            if(item == null)
            {
                return NotFound();
            }
            else
            {
                return item;
            }
        }


        //POST/api/blogs

        /// <summary>
        /// Adds a new blog to blog collection
        /// </summary>
        /// <remarks>
        /// Sample Data
        /// {
        /// "Title" : "Test Title Name",
        /// "Content" : "Test Content",
        /// "AddedBy" : "Vignesh",
        /// "AddedDate" : "2018-01-01"
        /// }
        /// </remarks>
        /// <param name="blog"></param>
        /// <returns>Newly inserted blog object</returns>
        /// <response code="200"></response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)] 
        [HttpPost("", Name ="AddBlog")]
        public async Task<ActionResult<Blog>>  AddBlog(Blog blog)
        {
            if (blog == null)
            {
                return BadRequest();
            }
            var result = await this.repository.AddAsync(blog);
            return result;
        }
        //PUT/api/blogs/{id}
        [HttpPut("{id:int?}", Name ="UpdateBlog")]
        public async Task<ActionResult<Blog>> UpdateBlog(int? id, Blog blog)
        {
            if (id == null) return BadRequest();
            if (id.Value != blog.Id) return NotFound();
            var item = await this.repository.Update(id.Value, blog);
            return item;
        }

        //DELETE /api/blogs/{id}
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Blog>> DeleteBlog(int? id)
        {
            if (id == null) return BadRequest();
            var result = await this.repository.Delete(id.Value);
            if(result==null)
            {
                return NotFound();
            }
            return result;
        }


    }
}