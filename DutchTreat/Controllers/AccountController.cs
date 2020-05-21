using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DutchTreat.Data.Entities;
using DutchTreat.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace DutchTreat.Controllers
{
    public class AccountController: Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<StoreUser> _signInManager;
        private readonly UserManager<StoreUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(ILogger<AccountController> logger, SignInManager<StoreUser> signInManager, UserManager<StoreUser> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
        }

        public ActionResult<StoreUser> Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "App");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var result = await _signInManager
                .PasswordSignInAsync(loginViewModel.UserName, 
                    loginViewModel.Password, 
                    loginViewModel.RememberMe, 
                    false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Failed to login");
                return View();
            }
            
            if (Request.Query.Keys.Contains("ReturnUrl"))
            {
                return Redirect(Request.Query["ReturnUrl"].First());
            }
            else
            {
                return RedirectToAction("Shop", "App");
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> Logout( )
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "App");
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) BadRequest(ModelState);

            StoreUser user = await _userManager.FindByNameAsync(loginViewModel.UserName);
            if (user == null) return BadRequest("User Not found");
            
            SignInResult signinResult = await _signInManager.CheckPasswordSignInAsync(user, loginViewModel.Password, false);
            if (!signinResult.Succeeded) return BadRequest("Password is not correct");

           Claim[] claims = new[]
            {
                // Create a jwt token
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            JwtSecurityToken token = new JwtSecurityToken(
                _configuration["Tokens:Issuer"],
                _configuration["Tokens:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            var results = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
            
            return Created("",results);
        }
    }
}