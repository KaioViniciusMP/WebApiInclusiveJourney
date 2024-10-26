using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using WebApiInclusiveJourney.Application.IServices;
using WebApiInclusiveJourney.Application.Services;

namespace WebApiInclusiveJourney.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentsService _commentsService;
        public CommentsController(ICommentsService service)
        {
            _commentsService = service;
        }

        [HttpGet("/comments")]
        public IActionResult GetComments()
        {
            var response = _commentsService.GetComments();
            if (response.Count < 1)
                return NoContent();
            else
                return Ok(response);
        }
    }
}
