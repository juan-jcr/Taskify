using Application.DTOs.TaskDTO;
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

    }
}
