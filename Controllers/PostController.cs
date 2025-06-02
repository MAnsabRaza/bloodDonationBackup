using bloodDonationAppBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace bloodDonationAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public PostController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        [HttpPost, Route("createPost")]
        public async Task<IActionResult> CreatePost(Post post)
        {
            try
            {
                if (post == null)
                {
                    return BadRequest(new { status = 400, message = "Invalid post data" });
                }

                Post post1 = new Post
                {
                    postTitle = post.postTitle,
                    bloodGroup = post.bloodGroup,
                    amountBlood = post.amountBlood,
                    date = post.date,
                    hospitalName = post.hospitalName,
                    message = post.message,
                    mobileNumber = post.mobileNumber,
                    country = post.country,
                    city = post.city,
                   
                };

                _appDbContext.post.Add(post1);
                await _appDbContext.SaveChangesAsync();

                return Ok(new { status = 200, message = "Post added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while saving the post.",
                    error = ex.Message,
                    success = false
                });
            }
        }
        [HttpGet, Route("getAllPost")]
        public async Task<IActionResult> getAllPost()
        {
            try
            {
                var post = await _appDbContext.post.ToListAsync();
                return Ok(new
                {
                    data = post,
                    count = post.Count,
                    success = true
                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while geting the post.",
                    error = ex.Message,
                    success = false
                });
            }
        }
        [HttpGet, Route("getPostById/{id}")]
        public async Task<IActionResult> getPostById(int id)
        {
            try
            {
                var post = await _appDbContext.post.FindAsync(id);
                if (post == null)
                {
                    return BadRequest(new { status = 400, message = "Invalid post data" });
                }
                return Ok(new { status = 200, message = "User fetched successfully", post });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while getting the post by id.",
                    error = ex.Message,
                    success = false
                });
            }
        }
        [HttpDelete("deletePost/{id}")]
        public async Task<IActionResult> deletePost(int id)
        {
            try{
                var post = await _appDbContext.post.FindAsync(id);
                if (post == null)
                {
                    return NotFound(new { status = 404, message = "Post not found" });
                }
                _appDbContext.post.Remove(post);
                await _appDbContext.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("updatePost")]
        public async Task<IActionResult> UpdatePost([FromBody] Post post)
        {
            try
            {
                var existingPost= await _appDbContext.post.FindAsync(post.Id);
                if (existingPost == null)
                    return Ok(new { status = 202, message = "Not Found" });

                existingPost.postTitle = post.postTitle;
                existingPost.bloodGroup = post.bloodGroup;
                existingPost.amountBlood = post.amountBlood;
                existingPost.date = post.date;
                existingPost.hospitalName = post.hospitalName;
                existingPost.message=post.message;
                existingPost.mobileNumber = post.mobileNumber;
                existingPost.city = post.city;
                existingPost.country = post.country;

                _appDbContext.Entry(existingPost).State = EntityState.Modified;
                await _appDbContext.SaveChangesAsync();

                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
