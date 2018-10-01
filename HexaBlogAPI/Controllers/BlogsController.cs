using HexaBlogAPI.Models;
using HexaBlogAPI.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
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
        // [ProducesResponseType( typeof(IEnumerable<Blog>), 200)]
        [HttpGet("",Name ="ListBlogs")]
        public IEnumerable<Blog> GetBlogs()
        {
            return this.repository.GetAll();
        }

        //GET /api/blogs{id}
        // [ProducesResponseType( typeof(IEnumerable<Blog>), 200)]
        [HttpGet("{id:int?}", Name = "GetBlog")]
        public async Task<ActionResult<Blog>> GetById(int? id)
        {
            if(id== null)
            {
                return BadRequest("Id is required");
            }
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