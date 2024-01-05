using CompanyDetailsWebAPI.Models;
using CompanyDetailsWebAPI.Repository;
using CompanyDetailsWebAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CompanyDetailsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRegController : ControllerBase
    {
        private readonly IUserRegRepository _userRegRepository;
        private readonly ILogger<UserRegController> _logger;
        public UserRegController(IUserRegRepository userRegRepository, ILogger<UserRegController> logger)
        {
            this._userRegRepository = userRegRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpPost("AddNewUser")]
        public async Task<IActionResult> AddNewUser(UserRegModel userRegModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userRegRepository.AddNewUser(userRegModel);

                    if (result > 0)
                    {
                        return Ok(new { Message = "User added successfully", UserId = result });
                    }
                    else if (result == -1)
                    {
                        return BadRequest(new { Message = "User with the provided PAN or mobile number already exists." });
                    }
                    else
                    {
                        return BadRequest(new { Message = "Failed to add user" });
                    }
                }
                else
                {
                    return BadRequest(new { Message = "Invalid model state" });
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error" });
            }
        }
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UserRegModel userRegModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _userRegRepository.UpdateUser(userRegModel);

                    if (result > 0)
                    {
                        return Ok(new { Message = "user updated successfully" });
                    }
                    else if (result == -2)
                    {
                        return BadRequest(new { Message = "PAN already exists for another user" });
                    }
                    else if (result == -3)
                    {
                        return BadRequest(new { Message = "Mobile number already exists for another user" });
                    }
                    else if (result == 0)
                    {
                        return NotFound(new { Message = "User not found" });
                    }
                    else if (result == -1)
                    {
                        return BadRequest(new { Message = "Failed to update User" });
                    }
                }
                else
                {
                    return BadRequest(new { Message = "Invalid model state" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error" });
            }

            return BadRequest(new { Message = "Update failed" });
        }



        [HttpDelete("DeleteUser")]

        public async Task<IActionResult> DeleteUser(UserRegModel userRegModel)
        {
            try
            {
                var result = await _userRegRepository.DeleteUser(userRegModel.Id);

                if (result > 0)
                {
                    return Ok(new { Message = "User deleted successfully" });
                }
                else
                {
                    return NotFound(new { Message = "User not found" });
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error" });
            }


        }

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers(int pageNo, int pageSize, string? textSearch)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();

            try
            {
                var pageUsers = await _userRegRepository.GetAllUsers(pageNo, pageSize, textSearch);

                List<UserRegModel> usersModels = (List<UserRegModel>)pageUsers.ResponseData1;
                // PaginationModel pagination = (PaginationModel)pageemployee.ResponseData2;
                if (usersModels.Count == 0)
                {
                    var returnMsg = string.Format("UserController-GetAll:  Record is not available.");
                    _logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                else
                {
                    var rtrMsg = string.Format("All getAll records are fetched successfully.");
                    _logger.LogDebug(rtrMsg);
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = rtrMsg;

                    responseDetails.ResponseData = new
                    {
                        EmployeeData = pageUsers.ResponseData1,
                        PaginationData = pageUsers.ResponseData2
                    };
                    return Ok(responseDetails);
                }
            }
            catch (Exception ex)
            {
                // Log error
                _logger.LogError(ex, "An error occurred in GetAllEmployee action.");
                var returnMsg = string.Format(ex.Message);
                _logger.LogInformation(returnMsg);
                responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseDetails.StatusMessage = returnMsg;
                return Ok(responseDetails);
            }
        }



        [HttpGet("GetAllUsersClaimList")]
        public async Task<IActionResult> GetAllUsersClaimList(int pageNo, int pageSize, string? textSearch, int SerarchByLeadStatusId, int SerarchByLeadSourceId)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();

            try
            {
                var pageUsers = await _userRegRepository.GetAllUsersClaimList(pageNo, pageSize, textSearch, SerarchByLeadStatusId, SerarchByLeadSourceId);

                List<UserClaimListModel> usersClaimModels = (List<UserClaimListModel>)pageUsers.ResponseData1;
                // PaginationModel pagination = (PaginationModel)pageemployee.ResponseData2;
                if (usersClaimModels.Count == 0)
                {
                    var returnMsg = string.Format("UserController-GetAll:  Record is not available.");
                    _logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                else
                {
                    var rtrMsg = string.Format("All getAll records are fetched successfully.");
                    _logger.LogDebug(rtrMsg);
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = rtrMsg;

                    responseDetails.ResponseData = new
                    {
                        EmployeeData = pageUsers.ResponseData1,
                        PaginationData = pageUsers.ResponseData2
                    };
                    return Ok(responseDetails);
                }
            }
            catch (Exception ex)
            {
                // Log error
                _logger.LogError(ex, "An error occurred in GetAllEmployee action.");
                var returnMsg = string.Format(ex.Message);
                _logger.LogInformation(returnMsg);
                responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseDetails.StatusMessage = returnMsg;
                return Ok(responseDetails);
            }
        }
    }
}
