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
       
      
    }
}
