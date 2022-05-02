using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using my_books.ActionResults;
using my_books.Data.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly PublishersService _publishersService;
        private readonly ILogger<PublishersController> _logger;

        public PublishersController(PublishersService publishersService, ILogger<PublishersController> logger)
        {
            _publishersService = publishersService;
            _logger = logger;
        }

        [HttpGet("get-all-publishers")]
        public IActionResult GetAllPublishers(string sortBy, string searchString, int pageNumber)
        {
            _logger.LogInformation($"This is a log entry from {nameof(GetAllPublishers)}");

            //throw new Exception($"This is an exception from {nameof(GetAllPublishers)}");

            try
            {
                var result = _publishersService.GetAllPublishers(sortBy, searchString, pageNumber);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest("Sorry, we could not load the publishers");
            }
        }

        [HttpPost("add-publisher")]
        public IActionResult AddPublisher([FromBody] PublisherVM publisherVm)
        {
            var response = _publishersService.AddPublisher(publisherVm);
            return Created(nameof(AddPublisher), response);
        }

        [HttpGet("get-publisher-by-id")]
        public CustomActionResult GetPublisherById(int id)
        {
            var response = _publishersService.GetPublisherById(id);

            if (response != null)
            {
                var responseObj = new CustomActionResultVM { Publisher = response };
                return new CustomActionResult(responseObj);
            }
            else
            {
                var responseObj = new CustomActionResultVM { Exception = new Exception("This is coming from the publishers controller") };
                return new CustomActionResult(responseObj);
            }
        }

        [HttpGet("get-publisher-books-with-authors/{id}")]
        public IActionResult GetPublisherData(int id)
        {
            var response = _publishersService.GetPublisherData(id);
            return Ok(response);
        }

        [HttpDelete("delete-publisher-by-id/{id}")]
        public IActionResult DeletePublisherById(int id)
        {
            _publishersService.DeletePublisherById(id);
            return Ok();
        }
    }
}