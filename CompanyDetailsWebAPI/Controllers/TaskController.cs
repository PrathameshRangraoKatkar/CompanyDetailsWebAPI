using CompanyDetailsWebAPI.Models;
using CompanyDetailsWebAPI.Repository;
using CompanyDetailsWebAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CompanyDetailsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddTask([FromBody] TaskModel taskModel)
        {
            try
            {
                var result = await _taskRepository.Add(taskModel);
                return Ok(new { Message = "Task added successfully", UserId = result }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskModel taskModel)
        {
            try
            {
                taskModel.Id = id; 
                var result = await _taskRepository.Update(taskModel);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok(new { Message = "Task Update successfully", UserId = result }); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            try
            {
                var result = await _taskRepository.Delete(id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok(new { Message = "Task Delete  successfully", UserId = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
