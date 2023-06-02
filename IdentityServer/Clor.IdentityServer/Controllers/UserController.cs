﻿using System.Linq;
using System.Threading.Tasks;
using Clor.IdentityServer.Dtos;
using Clor.IdentityServer.Models;
using Clor.Shared.Dtos;
using static IdentityServer4.IdentityServerConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Clor.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignupDto signupDto)
        {
            var user = new ApplicationUser
            {
                UserName = signupDto.UserName,
                Email = signupDto.Email,
                City = signupDto.City
            };

            var result = await _userManager.CreateAsync(user, signupDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            }

            return NoContent();
        }
    }
}