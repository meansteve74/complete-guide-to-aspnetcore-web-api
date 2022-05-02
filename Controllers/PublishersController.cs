using System;
using Microsoft.AspNetCore.Mvc;
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

        public PublishersController(PublishersService publishersService)
        {
            _publishersService = publishersService;
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