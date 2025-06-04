using Application.TaskList.Commands.UpdateTask;
using Application.TaskList.Commands.CreateTask;
using Application.TaskList.Commands.DeleteTask;
using Application.TaskList.Queries.GetAllTasks;
using Application.TaskList.Queries.GetTaskById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/tasks")]
public class TaskController : ControllerBase
{   
   private readonly IMediator _mediator;

   public TaskController(IMediator mediator)
   {
      _mediator = mediator;
   }

   [HttpGet]
   public async Task<IActionResult> GetAll()
   {
      var result = await _mediator.Send(new GetAllTaksQuery());
      return Ok(result);
   }

   [HttpPost]
   public async Task<IActionResult> Create([FromBody] CreateTaskCommand command)
   {
      var task = await _mediator.Send(command);
      return StatusCode(StatusCodes.Status201Created, task);
   }

   [HttpPut("{id}")]
   public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskCommand command)
   {
      if (id != command.Id) return BadRequest("ID mismatch");

      var task = await _mediator.Send(command);
      return Ok(task);
   }

   [HttpDelete("{id}")]
   public async Task<IActionResult> Delete(int id)
   {
      await _mediator.Send(new DeleteTaskCommand(id));
      return NoContent();
   }

   [HttpGet("{id}")]
   public async Task<IActionResult> GetById(int id)
   {
      var tarea = await _mediator.Send(new GetTaskByIdQuery(id));
      return Ok(tarea);
   }
}

