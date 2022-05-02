﻿using Microsoft.AspNetCore.Mvc;

namespace my_books.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.2")]
    [ApiVersion("1.9")]
    [Route("api/[controller]")]
    //[Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("get-test-data")]
        public IActionResult Get()
        {
            return Ok("This is TestController v1.0");
        }

        [HttpGet("get-test-data"), MapToApiVersion("1.2")]
        public IActionResult Get12()
        {
            return Ok("This is TestController v1.2");
        }

        [HttpGet("get-test-data"), MapToApiVersion("1.9")]
        public IActionResult Get19()
        {
            return Ok("This is TestController v1.9");
        }
    }
}