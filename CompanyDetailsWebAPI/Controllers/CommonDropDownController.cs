using CompanyDetailsWebAPI.Repository;
using CompanyDetailsWebAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CompanyDetailsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommonDropDownController : ControllerBase
    {
        private readonly ICommonDropDownRepository _repository;

        public CommonDropDownController(ICommonDropDownRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("GetAllStates")]
        public async Task<IActionResult> GetAllStates()
        {
            try
            {
                var states = await _repository.GetAllState();
                return Ok(states);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllDistricts/{stateId}")]
        public async Task<IActionResult> GetAllDistricts(int stateId)
        {
            try
            {
                var districts = await _repository.GetAllDistrict(stateId);
                return Ok(districts);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllTalukas/{stateId}/{districtId}")]
        public async Task<IActionResult> GetAllTalukas(int stateId, int districtId)
        {
            try
            {
                var talukas = await _repository.GetAllTaluka(stateId, districtId);
                return Ok(talukas);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }





        [HttpGet("GetAllLeadStatus")]
        public async Task<IActionResult> GetAllLeadStatus()
        {
            try
            {
                var status = await _repository.GetAllLeadStatus();
                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("GetAllLeadSource")]
        public async Task<IActionResult> GetAllLeadSource()
        {
            try
            {
                var source = await _repository.GetAllLeadSource();
                return Ok(source);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [HttpGet("GetAllOccupations")]
        public async Task<IActionResult> GetAllOccupations()
        {
            try
            {
                var occupation = await _repository.GetAllOccupations();
                return Ok(occupation);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("GetAllOrganization")]
        public async Task<IActionResult> GetAllOrganization()
        {
            try
            {
                var states = await _repository.GetAllOrganization();
                return Ok(states);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetAllUnits")]
        public async Task<IActionResult> GetAllUnits()
        {
            try
            {
                var states = await _repository.GetAllUnits();
                return Ok(states);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
