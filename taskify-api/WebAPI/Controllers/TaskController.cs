using Application.DTOs.TaskDTO;
using Application.Exceptions;
using Application.Services.TaskService;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _service;

        public TaskController(ITaskService taskService)
        {
            _service = taskService;
        }
        

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllTasksAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskDto taskDto)
        {
            var task = await _service.CreateAsync(taskDto);
            return Ok(task);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> FindById(int id)
        {
            try
            {
                var task = await _service.GetByIdAsync(id);
                return Ok(task);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDto>> UpdateAsync(int id, UpdateTaskDto taskDto)
        {
            try
            {
                var task = await _service.UpdateAsync(id, taskDto);
                return Ok(task);
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (ResourceNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
