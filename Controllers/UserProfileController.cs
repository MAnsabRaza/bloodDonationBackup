using bloodDonationAppBackend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bloodDonationAppBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public UserProfileController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost,Route("addUserProfile")]
        public async Task<IActionResult> SaveUserProfile(userProfile profile)
        {
            try
            {
                if (profile == null)
                {
                    return BadRequest(new { status = 400, message = "Invalid user profile data" });
                }

                userProfile newUserProfile = new userProfile
                {
                    name = profile.name,
                    city = profile.city,
                    country = profile.country,
                    bloodGroup = profile.bloodGroup,
                    mobileNumber = profile.mobileNumber,
                    gender = profile.gender,
                    bloodDonate = profile.bloodDonate,
                    message = profile.message
                };

                _appDbContext.userProfile.Add(newUserProfile);
                await _appDbContext.SaveChangesAsync();
                return Ok(new { status = 200, message = "User added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while saving the user profile.",
                    error = ex.Message,
                    success = false
                });
            }
        }

        [HttpGet]
        [Route("allUserProfile")]
        public async Task<IActionResult> GetAllUserProfiles()
        {
            try
            {
                var profile = await _appDbContext.userProfile.ToListAsync();
                return Ok(new
                {
                    data = profile,
                    count = profile.Count,
                    success = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving user profiles.",
                    error = ex.Message,
                    success = false
                });
            }
        }

        [HttpGet, Route("getUserProfileById/{id}")]
        public async Task<IActionResult> GetUserProfileById(int id)
        {
            try
            {
                var userProfile = await _appDbContext.userProfile.FindAsync(id);
                if (userProfile == null)
                {
                    return NotFound(new { status = 404, message = "User not found" });
                }
                return Ok(new { status = 200, message = "User fetched successfully", userProfile });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("deleteUserProfile/{id}")]
        public async Task<IActionResult> DeleteUserProfile(int id)
        {
            try
            {
                var userProfile=await _appDbContext.userProfile.FindAsync(id);
                if (userProfile == null)
                {
                    return NotFound(new { status = 404, message = "User not found" });
                }
                _appDbContext.userProfile.Remove(userProfile);
                await _appDbContext.SaveChangesAsync();
                return Ok(new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("updateUserProfile")]
        public async Task<IActionResult> UpdateUserProfile([FromBody] userProfile userProfile)
        {
            try
            {
                var existingUserProfile = await _appDbContext.userProfile.FindAsync(userProfile.Id);
                if (existingUserProfile == null)
                    return Ok(new { status = 202, message = "Not Found" });

                existingUserProfile.name = userProfile.name;
                existingUserProfile.city=userProfile.city;
                existingUserProfile.country=userProfile.country;
                existingUserProfile.bloodGroup=userProfile.bloodGroup;
                existingUserProfile.mobileNumber=userProfile.mobileNumber;
                existingUserProfile.bloodDonate=userProfile.bloodDonate;
                existingUserProfile.gender=userProfile.gender;
                existingUserProfile.message=userProfile.message;

                _appDbContext.Entry(existingUserProfile).State = EntityState.Modified;
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