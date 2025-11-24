using Core.InterfacesRepositories;
using Core.InterfacesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DailyLogController(IDailyLogService dailyLogService, IDailyLogRepository repository) : ControllerBase
    {
        [Authorize]
        [HttpPost("save")]
        public async Task<IActionResult> SaveDailyLog(DailyLogDto logDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!logDto.Meals.Any())
                return BadRequest(new { message = "Cannot save an empty log." });

            var alreadyExists = await repository.UserHasLogForDateAsync(userId, logDto.Date);
            if (alreadyExists)
                return BadRequest(new { message = "You already have a daily log for this date." });

            var dailyLog = await dailyLogService.CreateDailyLog(logDto, userId);
            await repository.AddDailyLogAsync(dailyLog);

            var success = await repository.SaveChangesAsync();

            if (success)
                return Ok(dailyLog);
            else
                return StatusCode(500, "An error occurred while saving the daily log.");
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDailyLogById(int id)
        {
            var dailyLog = await repository.GetLogByIdAsync(id);
            if (dailyLog == null)
                return NotFound(new { message = $"Daily log with ID {id} not found." });
            return Ok(dailyLog);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetDailyLogsByUser([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dailyLogs = await repository.GetDailyLogsByUserIdAsync(userId, pageNumber, pageSize);
            return Ok(dailyLogs);
        }

        [Authorize]
        [HttpGet("range")]
        public async Task<IActionResult> GetLogsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dailyLogs = await repository.GetLogsByDateRangeAsync(userId, startDate, endDate, pageNumber, pageSize);
            return Ok(dailyLogs);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDailyLog(int id)
        {
            var dailyLog = await repository.GetLogByIdAsync(id);

            if (dailyLog == null)
                return NotFound(new { message = $"Daily log with ID {id} not found." });

            repository.Delete(dailyLog);
            var success = await repository.SaveChangesAsync();

            if (success)
                return NoContent();
            else
                return StatusCode(500, "An error occurred while deleting the daily log.");
        }


    }
}
