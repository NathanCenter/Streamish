using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Streamish.Models;
using Streamish.Repositories;
using System;

namespace Streamish.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {

        private readonly IUserProfileRepository _userProfileRepository;
        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        [HttpGet("GetAllUserProfile")]
        public IActionResult GetAllUserProfile()
        {
            var userProfile = _userProfileRepository.GetAll();
            return Ok(userProfile);
        }

        [HttpPost]
        public IActionResult Post(UserProfile userprofile)
        {
            _userProfileRepository.Add(userprofile);
            return CreatedAtAction("Get", new { id = userprofile.Id }, userprofile);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UserProfile userprofile)
        {
            if (id != userprofile.Id)
            {
                return BadRequest();
            }

            _userProfileRepository.Update(userprofile);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userProfileRepository.Delete(id);
            return NoContent();
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var video = _userProfileRepository.GetByIdandVideo(id);
            if (video == null)
            {
                return NotFound();
            }
            return Ok(video);
        }
    }
}
