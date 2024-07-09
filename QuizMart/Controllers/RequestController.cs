using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizMart.Services.IServices;
using System.Security.Claims;

namespace QuizMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;
        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpGet("Get-all-Requests")]
        public async Task<IActionResult> GetAllRequests()
        {
            var requests = await _requestService.GetAllRequests();
            return Ok(requests);
        }

        [HttpPost("Approve-Request")]
        public async Task<IActionResult> ApproveRequest([FromBody] Guid requestId)
        {
            try
            {
                var moderatorId = User.FindFirstValue(ClaimTypes.Sid);
                await _requestService.ApproveRequest(requestId, Guid.Parse(moderatorId));
                return Ok("Request approved successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
