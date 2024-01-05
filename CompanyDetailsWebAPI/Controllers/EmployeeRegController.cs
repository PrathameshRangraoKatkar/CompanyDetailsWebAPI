using CompanyDetailsWebAPI.Models;
using CompanyDetailsWebAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CompanyDetailsWebAPI.Repository;
using Microsoft.Extensions.Logging;



namespace CompanyDetailsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeRegController : ControllerBase
    {
        private readonly IEmployeeRegRepository employeeRegRepository;
        private readonly ILogger<EmployeeRegController> _logger;
        public EmployeeRegController(IEmployeeRegRepository employeeRegRepository, ILogger<EmployeeRegController> logger)
        {
            this.employeeRegRepository = employeeRegRepository;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpPost("AddnewEmployee")]
        public async Task<IActionResult> AddnewEmployee(EmployeeAddupdateModel employeeRegModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await employeeRegRepository.AddnewEmployee(employeeRegModel);

                    if (result > 0)
                    {
                        return Ok(new { Message = "Employee added successfully", EmployeeId = result });
                    }
                    else if (result == -1)
                    {
                        return BadRequest(new { Message = "Employee with the provided email or mobile number already exists." });
                    }
                    else
                    {
                        return BadRequest(new { Message = "Failed to add employee" });
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



        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeAddupdateModel employeeRegModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await employeeRegRepository.UpdateEmployee(employeeRegModel);

                    if (result > 0)
                    {
                        return Ok(new { Message = "Employee updated successfully" });
                    }
                    else if (result == -2)
                    {
                        return BadRequest(new { Message = "Email already exists for another employee" });
                    }
                    else if (result == -3)
                    {
                        return BadRequest(new { Message = "Mobile number already exists for another employee" });
                    }
                    else if (result == 0)
                    {
                        return NotFound(new { Message = "Employee not found" });
                    }
                    else if (result == -1)
                    {
                        return BadRequest(new { Message = "Failed to update employee" });
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





        [HttpDelete("DeleteEmployee")]

        public async Task<IActionResult> DeleteEmployee(EmployeeRegModel employeeRegModel)
        {
            try
            {
                var result = await employeeRegRepository.DeleteEmployee(employeeRegModel.Id);

                if (result > 0)
                {
                    return Ok(new { Message = "Employee deleted successfully" });
                }
                else
                {
                    return NotFound(new { Message = "Employee not found" });
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error" });
            }


        }
        [HttpGet("GetAllEmployee")]
        public async Task<IActionResult> GetAllEmployee(int pageNo, int pageSize, DateTime? fromDate, DateTime? toDate, string? textSearch)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();

            try
            {
                var pageemployee = await employeeRegRepository.GetAllEmployee(pageNo, pageSize, fromDate, toDate, textSearch);

                List<EmployeeRegModel> employeeModels = (List<EmployeeRegModel>)pageemployee.ResponseData1;
                // PaginationModel pagination = (PaginationModel)pageemployee.ResponseData2;
                if (employeeModels.Count == 0)
                {
                    var returnMsg = string.Format("EmployeeController-GetAll: Employee Record is not available.");
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
                        EmployeeData = pageemployee.ResponseData1,
                        PaginationData = pageemployee.ResponseData2
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



        [HttpGet("GetEmployeeById")]

        public async Task<IActionResult> GetEmployeeById(int Id)
        {
            try
            {
                var employee = await employeeRegRepository.GetEmployeeById(Id);


                if (employee != null)
                {
                    return Ok(employee);
                }

                else
                {
                    return NotFound(new { Message = "Employee Not found " });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error" });

            }
        }



        [HttpPost("login")]
        public async Task<ActionResult> EmployeeLogin([FromBody] LoginModel loginModel)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();
            try
            {
                if (loginModel.MobileNumber == "")
                {
                    var returnStr = string.Format("Kindly enter valid UserName.");
                    responseDetails.StatusCode = StatusCodes.Status400BadRequest.ToString();
                    responseDetails.StatusMessage = returnStr;
                    return Ok(responseDetails);
                }
                if (loginModel.Password == "")
                {
                    var returnStr = string.Format("Kindly enter valid Password.");
                    responseDetails.StatusCode = StatusCodes.Status400BadRequest.ToString();
                    responseDetails.StatusMessage = returnStr;
                    return Ok(responseDetails);
                }


                var Userlst = await employeeRegRepository.Login(loginModel);


                if (Userlst == null)
                {
                    var returnMsg = string.Format("User is not available.");
                    _logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                else if (Userlst.MSG == "Invalid UserName.")
                {
                    var returnMsg = string.Format("User Name is Incorrect");
                    _logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                else if (Userlst.MSG == "Invalid Password.")
                {
                    var returnMsg = string.Format("Password is Incorrect.");
                    _logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                else
                {
                    var rtrMsg = string.Format("Login Successful");
                    _logger.LogDebug(rtrMsg);
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = rtrMsg;
                    responseDetails.ResponseData = Userlst;
                    //responseDetails.ResponseData1 = jwtResult;
                    return Ok(responseDetails);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                var returnMsg = string.Format(ex.Message);
                _logger.LogInformation(returnMsg);
                responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseDetails.StatusMessage = returnMsg;
                return Ok(responseDetails);
            }
        }





        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangepasswordModel obj)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();

            try
            {
                if (obj.Id == 0)
                {
                    var returnMsg = string.Format("Please Enter UserId");
                    _logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                if (obj.NewPassword == "" || obj.NewPassword == null)
                {
                    var returnMsg = string.Format("Please Enter New Password");
                    _logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                if (obj.OldPassword == "" || obj.OldPassword == null)
                {
                    var returnMsg = string.Format("Please Enter Old Password");
                    _logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }

                var employeeDetails = await employeeRegRepository.ChangePassword(obj);

                if (employeeDetails == null)
                {
                    _logger.LogDebug("Password change Failed. Please check Credentials");
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = string.Format("Password change Failed. Please check Credentials");
                }
                else if (employeeDetails.Id > 0)
                {
                    _logger.LogDebug("Password Changed Successfully");
                    responseDetails.StatusCode = StatusCodes.Status200OK.ToString();
                    responseDetails.StatusMessage = string.Format("Password Changed Successfully");
                    responseDetails.ResponseData = employeeDetails;
                }
                else if (employeeDetails == null)
                {
                    _logger.LogDebug("Please ensure that the new password and the confirmed password are the same.");
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = string.Format("Please ensure that the new password and the confirmed password are the same.");
                }
                else if (employeeDetails == null)
                {
                    _logger.LogDebug("Please Enter Valid Current Password");
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = string.Format("Please Enter Valid Current Password");
                }
                else if (employeeDetails == null)
                {
                    _logger.LogDebug("User Does Not exist");
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = string.Format("User Does Not exist");
                }
                else if (employeeDetails == null)
                {
                    _logger.LogDebug("Current password and new password are the same");
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = string.Format("Current password and new password are the same");
                }

                return Ok(responseDetails);
            }
            catch (Exception ex)
            {
                var msgStr = string.Format("Error in ChangePassword with UserId:{0} and Error :{1}", obj.Id, ex.Message.ToString());
                _logger.LogInformation(msgStr);

                responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseDetails.StatusMessage = msgStr;
                return Ok(responseDetails);
            }
        }


    }
}
