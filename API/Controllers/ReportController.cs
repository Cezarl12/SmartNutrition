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
    public class ReportController(IReportService reportService, UserManager<ApplicationUser> userManager) : ControllerBase
    {
        [Authorize]
        [HttpGet("summary")]
        public async Task<ActionResult<AverageSummaryDto>> GetAverageSummary([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);

            if (user == null) return NotFound("User not found.");

            if (startDate > endDate)
                return BadRequest("Start date cannot be after end date.");

            var summary = await reportService.GetAverageSummaryAsync(user, startDate, endDate);
            return Ok(summary);
        }


        [HttpGet("daily")]
        public async Task<ActionResult<List<DailyDataDto>>> GetDailyData([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await userManager.FindByIdAsync(userId);

            if (startDate > endDate)
                return BadRequest("Start date cannot be after end date.");

            var dailyData = await reportService.GetDailyDataAsync(user, startDate, endDate);
            return Ok(dailyData);

        }
    }
}