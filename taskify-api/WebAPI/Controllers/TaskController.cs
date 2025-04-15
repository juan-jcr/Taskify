using Application.DTOs.TaskDTO;
using Application.Interfaces;
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
            var task = await _service.GetByIdAsync(id);
            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDto>> UpdateAsync(int id, UpdateTaskDto taskDto)
        {
            var task = await _service.UpdateAsync(id, taskDto);
            return Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
            
        }
    }
}
