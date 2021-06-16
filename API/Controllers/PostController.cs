using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;
using Data;

namespace Blogger.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private DataContext _postDbContext;
        public PostsController(DataContext postDbContext)
        {
            _postDbContext = postDbContext;
        }

        //GET:  /api/<PostsController>
        [HttpGet]
        [Produces("application/json")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _postDbContext.Posts.ToListAsync());
        }

        //GET api/<PostsController>/<Id>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var post = await _postDbContext.Posts.FindAsync(id);
            return Ok(post);
        }

        //POST api/<PostsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post post)
        {
            await _postDbContext.Posts.AddAsync(post);
            await _postDbContext.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);

        }

        //PUT api/<PostsController>/<Id>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Post postObj)
        {
            var post = await _postDbContext.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound("Couldn't find a post with that id.");
            }
            else
            {
                post.Title = postObj.Title;
                post.Author = postObj.Author;
                post.Text = postObj.Text;
                await _postDbContext.SaveChangesAsync();
                return Ok("Record updated successfully.");
            }
        }

        //DELETE api/<PostsController>/<Id>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _postDbContext.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound("Couldn't find a post with that id.");
            }
            else
            {
                _postDbContext.Posts.Remove(post);
                await _postDbContext.SaveChangesAsync();
                return Ok("Record was successfully deleted.");

            }
        }
    }
}