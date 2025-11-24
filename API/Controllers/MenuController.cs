using Core.InterfacesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController(UserManager<ApplicationUser> userManager, IMenuGeneratorService generatorService) : ControllerBase
    {
        [Authorize]
        [HttpGet("generate")]
        public async Task<IActionResult> GenerateMenuAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                return NotFound("User not found.");

            if (user.Target == null || user.Target.DailyKcal <= 0)
                return BadRequest("User profile targets have not been calculated. Please update your profile.");

            var menu = await generatorService.GenerateDailyMenuAsync(user);

            return Ok(menu);
        }
    }
}
