using Core.InterfacesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.Entities;
using System.Security.Claims;
namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(SignInManager<ApplicationUser> signInManager, INutritionService nutritionService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult> Register(Register register)
        {
            var user = new ApplicationUser
            {
                UserName = register.Email,
                Email = register.Email,
                FirstName = register.FirstName,
                LastName = register.LastName
            };

            var result = await signInManager.UserManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }
            return Ok(new { message = "Registration successful" });
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(Login login, [FromQuery] bool useCookies)
        {
            if (useCookies)
            {
                var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent: true, lockoutOnFailure: false);

                if (!result.Succeeded)
                {
                    return Unauthorized(new { message = "Wrong Password" });
                }

                return Ok(new { message = "Login successful." });
            }
            else
            {
                return BadRequest(new { message = "Token-based login not implemented." });
            }
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok(new { message = "Logout successful" });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<UserInfo>> GetUserInfo()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail == null)
            {
                return Unauthorized(new { message = "User identity claim not found." });
            }

            var user = await signInManager.UserManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                return NotFound(new { message = "User record not found in database." });
            }

            var userInfo = new UserInfo
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Age = user.Age,
                Sex = user.Sex,
                Height = user.Height,
                Weight = user.Weight,
                ActivityLevel = user.ActivityLevel,
                Goal = user.Goal,
                Preferences = user.Preferences,
                NumberOfMeals = user.NumberOfMeals
            };
            if (user.Target != null)
            {
                userInfo.Target = user.Target;
            }

            return Ok(userInfo);

        }

        [Authorize]
        [HttpPut("update-profile")]
        public async Task<IActionResult> UpdateProfile(UserInfo userInfo)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);

            if (userEmail == null)
            {
                return Unauthorized(new { message = "User identity claim not found." });
            }

            var user = await signInManager.UserManager.FindByEmailAsync(userEmail);

            if (user == null)
            {
                return NotFound(new { message = "User record not found in database." });
            }

            user.FirstName = userInfo.FirstName;
            user.LastName = userInfo.LastName;
            user.Age = userInfo.Age;
            user.Sex = userInfo.Sex;
            user.Height = userInfo.Height;
            user.Weight = userInfo.Weight;
            user.ActivityLevel = userInfo.ActivityLevel;
            user.Goal = userInfo.Goal;
            user.Preferences = userInfo.Preferences;
            user.NumberOfMeals = userInfo.NumberOfMeals;
            user.Target = nutritionService.CalculateDailyTargets(user);

            var result = await signInManager.UserManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(user);


        }
    }

}
