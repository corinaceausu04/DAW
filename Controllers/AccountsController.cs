using DAW.Data.DTO;
using DAW.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAW.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<User> _userManager;

        public AccountsController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser(UserForRegistrationDto userForRegistration)
        {
            User user = new User
            {
                FirstName = userForRegistration.FirstName,
                LastName = userForRegistration.LastName
            };

            var res = await _userManager.CreateAsync(user, userForRegistration.Password);

            if(!res.Succeeded)
            {
                var errors = res.Errors.Select(e => e.Description);

                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }
            return StatusCode(201);
        }
    }
}
