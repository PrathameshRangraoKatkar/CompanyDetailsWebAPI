using CompanyDetailsWebAPI.Context;
using CompanyDetailsWebAPI.Models;
using CompanyDetailsWebAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CompanyDetailsWebAPI.Repository.Interface;

namespace CompanyDetailsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly OrganizationRepository _organizationRepository;
        private readonly ILogger<UserRegController> _logger;

        public OrganizationController(OrganizationRepository organizationRepository, ILogger<UserRegController> logger)
        {
            _organizationRepository = organizationRepository;
            _logger = logger;
        }


        [HttpPost("AddOrganization")]
        public async Task<IActionResult> AddOrganization([FromBody] OrganizationAddUpdateModel organizationAddUpdateModel)
        {
            try
            {
                int result = await _organizationRepository.AddOrganization(organizationAddUpdateModel);

                if (result > 0)
                {
                    return Ok(new { OrganizationId = result, Message = "Organization added successfully." });
                }
                else if (result == -1)
                {
                    return BadRequest(new { Message = "Organization with specified Id already exists." });
                }
                else
                {
                    return BadRequest(new { Message = "Failed to add organization." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }



        [HttpPut("EditOrganization")]
        public async Task<IActionResult> EditOrganization([FromBody] OrganizationAddUpdateModel organizationAddUpdateModel)
        {
            try
            {
                int updateResult = await _organizationRepository.EditOrganization(organizationAddUpdateModel);

                if (updateResult > 0)
                {
                    // Organization updated successfully, now fetch the updated list
                    var updatedList = await _organizationRepository.GetOrganization();

                    return Ok(new { UpdatedList = updatedList, Message = "Organization updated successfully." });
                }
                else if (updateResult == -1)
                {
                    return BadRequest(new { Message = "Invalid organization Id." });
                }
                else
                {
                    return BadRequest(new { Message = "Failed to update organization." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, new { Message = "Internal Server Error" });
            }
        }


        [HttpDelete("BlockOrganization")]

        public async Task<IActionResult> BlockOrganization(OrganizationModel organizationModel)
        {
            try
            {
                var result = await _organizationRepository.BlockOrganization(organizationModel.Id);

                if (result > 0)
                {
                    return Ok(new { Message = "Organization Blocked successfully" });
                }
                else
                {
                    return NotFound(new { Message = "Organization not found" });
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Internal Server Error" });
            }


        }


        [HttpGet("GetAllOrganization")]
        public async Task<IActionResult> GetAllOrganization(int pageNo, int pageSize, string? textSearch)
        {
            BaseResponseStatus responseDetails = new BaseResponseStatus();

            try
            {
                var pageUsers = await _organizationRepository.GetAllOrganization(pageNo, pageSize, textSearch);

                List<OrganizationModel> OrgModels = (List<OrganizationModel>)pageUsers.ResponseData1;
                // PaginationModel pagination = (PaginationModel)pageemployee.ResponseData2;
                if (OrgModels.Count == 0)
                {
                    var returnMsg = string.Format("OrganizationController-GetAll:  Record is not available.");
                    _logger.LogInformation(returnMsg);
                    responseDetails.StatusCode = StatusCodes.Status404NotFound.ToString();
                    responseDetails.StatusMessage = returnMsg;
                    return Ok(responseDetails);
                }
                else
                {
                    var rtrMsg = string.Format("All Organization records are fetched successfully.");
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
                _logger.LogError(ex, "An error occurred in GetAllOrganization action.");
                var returnMsg = string.Format(ex.Message);
                _logger.LogInformation(returnMsg);
                responseDetails.StatusCode = StatusCodes.Status409Conflict.ToString();
                responseDetails.StatusMessage = returnMsg;
                return Ok(responseDetails);
            }
        }



      

    }
}
