using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizMart.Services;
using System;
using System.Threading.Tasks;

namespace QuizMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        #region Get All Requests
        [HttpGet("Get-All-Requests")]
        public async Task<IActionResult> GetAllRequests()
        {
            try
            {
                var requests = await _requestService.GetAllRequestsAsync();
                return Ok(requests);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion

        #region Get Request by ID
        [HttpGet("Get-Request-By-Id/{requestId}")]
        public async Task<IActionResult> GetRequestById(Guid requestId)
        {
            try
            {
                var request = await _requestService.GetRequestByIdAsync(requestId);
                if (request == null)
                {
                    return NotFound("Request not found.");
                }
                return Ok(request);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion

        #region Get All Pending Add Deck Requests
        [HttpGet("Get-All-Pending-Add-Deck-Requests")]
        public async Task<IActionResult> GetAllPendingAddDeckRequests()
        {
            try
            {
                var requests = await _requestService.GetAllPendingAddDeckRequestsAsync();
                return Ok(requests);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion

        #region Get All Pending Edit Deck Requests
        [HttpGet("Get-All-Pending-Edit-Deck-Requests")]
        public async Task<IActionResult> GetAllPendingEditDeckRequests()
        {
            try
            {
                var requests = await _requestService.GetAllPendingEditDeckRequestsAsync();
                return Ok(requests);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion
    }
}
