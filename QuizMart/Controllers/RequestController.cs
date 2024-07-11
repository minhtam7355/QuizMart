using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizMart.Models.DomainModels;
using QuizMart.Services;
using QuizMart.Services.IServices;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuizMart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Moderator")]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        #region Get All Requests
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
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
        [HttpGet("{requestId}")]
        public async Task<IActionResult> GetById(Guid requestId)
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
        [HttpGet("pending/add-deck")]
        public async Task<IActionResult> GetPendingAddDeck()
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
        [HttpGet("pending/edit-deck")]
        public async Task<IActionResult> GetPendingEditDeck()
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

        #region Approve Add Deck Request
        [HttpPost("approve/add-deck/{requestId}")]
        public async Task<IActionResult> ApproveAddDeck(Guid requestId)
        {
            try
            {
                var modIdString = User.FindFirstValue(ClaimTypes.Sid); // Ensure this matches your claim type

                if (Guid.TryParse(modIdString, out Guid modId))
                {
                    var success = await _requestService.ApproveAddDeckRequestAsync(requestId, modId);
                    if (success)
                    {
                        return Ok("Add deck request approved successfully.");
                    }
                    else
                    {
                        return NotFound("Add deck request not found or could not be approved.");
                    }
                }
                else
                {
                    return BadRequest("Invalid moderator ID format.");
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion

        #region Deny Add Deck Request
        [HttpPost("deny/add-deck/{requestId}")]
        public async Task<IActionResult> DenyAddDeck(Guid requestId)
        {
            try
            {
                var modIdString = User.FindFirstValue(ClaimTypes.Sid); // Ensure this matches your claim type

                if (Guid.TryParse(modIdString, out Guid modId))
                {
                    var success = await _requestService.DenyAddDeckRequestAsync(requestId, modId);
                    if (success)
                    {
                        return Ok("Add deck request denied successfully.");
                    }
                    else
                    {
                        return NotFound("Add deck request not found or could not be denied.");
                    }
                }
                else
                {
                    return BadRequest("Invalid moderator ID format.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion

        #region Approve Edit Deck Request
        [HttpPost("approve/edit-deck/{requestId}")]
        public async Task<IActionResult> ApproveEditDeck(Guid requestId)
        {
            try
            {
                var modIdString = User.FindFirstValue(ClaimTypes.Sid); // Ensure this matches your claim type

                if (Guid.TryParse(modIdString, out Guid modId))
                {
                    var success = await _requestService.ApproveEditDeckRequestAsync(requestId, modId);
                    if (success)
                    {
                        return Ok("Edit deck request approved successfully.");
                    }
                    else
                    {
                        return NotFound("Edit deck request not found or could not be approved.");
                    }
                }
                else
                {
                    return BadRequest("Invalid moderator ID format.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion

        #region Deny Edit Deck Request
        [HttpPost("deny/edit-deck/{requestId}")]
        public async Task<IActionResult> DenyEditDeck(Guid requestId)
        {
            try
            {
                var modIdString = User.FindFirstValue(ClaimTypes.Sid); // Ensure this matches your claim type

                if (Guid.TryParse(modIdString, out Guid modId))
                {
                    var success = await _requestService.DenyEditDeckRequestAsync(requestId, modId);
                    if (success)
                    {
                        return Ok("Edit deck request denied successfully.");
                    }
                    else
                    {
                        return NotFound("Edit deck request not found or could not be denied.");
                    }
                }
                else
                {
                    return BadRequest("Invalid moderator ID format.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        #endregion
    }
}
