using DAW.Data.DTO;
using DAW.Data.Entities;
using DAW.Data.Manager;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace DAW.Controllers
{
    public class AccountsController : Controller
    {
        private UserManager<User> _userManager;
        private readonly JWTHandler _jwtHandler;

        public AccountsController(UserManager<User> userManager, JWTHandler jwtHandler)
        {
            _userManager = userManager;
            _jwtHandler = jwtHandler;
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody]UserForRegistrationDto userForRegistration)
        {
            User user = new User
            {
                FirstName = userForRegistration.FirstName,
                LastName = userForRegistration.LastName,
                Email = userForRegistration.Email,
                Role = userForRegistration.Role,
                UserName = userForRegistration.Email
            };

            var res = await _userManager.CreateAsync(user, userForRegistration.Password);

            if(!res.Succeeded)
            {
                var errors = res.Errors.Select(e => e.Description);

                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }
            return StatusCode(201);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLogin)
        {
            var user = await _userManager.FindByEmailAsync(userForLogin.Email);
            if (!await _userManager.CheckPasswordAsync(user, userForLogin.Password))
                return Unauthorized(new LoginResponseDtocs { Errors = "invalid account" });

            var signingCredentials = _jwtHandler.GetSigningCredentials();
            var claims = _jwtHandler.GetClaims(user);
            var tokenOptions = _jwtHandler.GenerateTokenOptions(signingCredentials, claims);
            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return Ok(new LoginResponseDtocs { IsLoginOk = true, Token = token });

        }

    }
}
