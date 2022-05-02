using System;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly LogsService _logsService;

        public LogsController(LogsService logsService)
        {
            _logsService = logsService;
        }

        [HttpGet("get-all-logs")]
        public IActionResult GetAllLogs()
        {
            try
            {
                var allLogs = _logsService.GetAllLogs();
                return Ok(allLogs);
            }
            catch (Exception)
            {
                return BadRequest("Could not fetch logs from the database.");
            }
        }
    }
}