using api.DTO;
using api.Models;
using api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly UserManager<User> _userManager;
        public readonly IUserService _userService;

        public AuthController(UserManager<User> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO userDetails)
        {
            var user = await _userService.FindByUserName(userDetails.Username);
            if (user != null)
            {
                return BadRequest(new { Message = "User with that username already exists." });
            }
            var identityUser = new User() { UserName = userDetails.Username };
            var result = await _userManager.CreateAsync(identityUser, userDetails.Password);

            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new { Message = "User registration failed" });
            }

            return await Login(userDetails);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserRegisterDTO userLogin)
        {
            var identityUser = await _userService.FindByUserName(userLogin.Username);
            if (identityUser == null)
            {
                return BadRequest(new { Message = "Login failed, user not found." });
            }
            var result = _userManager.PasswordHasher.VerifyHashedPassword(identityUser, identityUser.PasswordHash, userLogin.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                return new BadRequestObjectResult(new { Message = "Login failed. Invalid password" });
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, identityUser.UserName),
                new Claim(ClaimTypes.NameIdentifier, identityUser.Id.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Ok(new { Message = "You are logged in."});
        }
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout() 
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            Response.Cookies.Delete(".AspNetCore.Cookie");
            return Ok(new { Message = "You are logged out" });
        }
    }
}
