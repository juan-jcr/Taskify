using Application.TaskList.Commands.UpdateTask;
using Application.TaskList.Commands.CreateTask;
using Application.TaskList.Commands.DeleteTask;
using Application.TaskList.DTO;
using Application.TaskList.Queries.GetAllTasks;
using Application.TaskList.Queries.GetTaskById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;


/// <summary>
/// Controlador para gestionar tareas.
/// </summary>
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
   
   
   /// <summary>
   /// Obtiene todas las tareas.
   /// </summary>
   /// <returns>Lista de tareas.</returns>
   /// <response code="200">Tareas obtenidas correctamente.</response>
   [HttpGet]
   [ProducesResponseType(typeof(IEnumerable<TaskDto>), StatusCodes.Status200OK)]
   public async Task<IActionResult> GetAll()
   {
      var result = await _mediator.Send(new GetAllTaksQuery());
      return Ok(result);
   }
   
   /// <summary>
   /// Crea una nueva tarea.
   /// </summary>
   /// <param name="command">Datos de la tarea a crear.</param>
   /// <returns>La tarea creada.</returns>
   /// <response code="201">Tarea creada correctamente.</response>
   /// <response code="409">Datos inválidos.</response>
   [HttpPost]
   [ProducesResponseType(typeof(TaskDto), StatusCodes.Status201Created)]
   [ProducesResponseType(StatusCodes.Status409Conflict)]
   
   public async Task<IActionResult> Create([FromBody] CreateTaskCommand command)
   {
      var task = await _mediator.Send(command);
      return StatusCode(StatusCodes.Status201Created, task);
   }
   
   /// <summary>
   /// Actualiza una tarea existente.
   /// </summary>
   /// <param name="id">ID de la tarea a actualizar.</param>
   /// <param name="command">Datos actualizados de la tarea.</param>
   /// <returns>La tarea actualizada.</returns>
   /// <response code="200">Tarea actualizada correctamente.</response>
   /// <response code="409">ID no coincide o datos inválidos.</response>
   [HttpPut("{id}")]
   [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status409Conflict)]
   public async Task<IActionResult> Update(int id, [FromBody] UpdateTaskCommand command)
   {
      if (id != command.Id) return BadRequest("ID mismatch");

      var task = await _mediator.Send(command);
      return Ok(task);
   }
   
   /// <summary>
   /// Elimina una tarea por ID.
   /// </summary>
   /// <param name="id">ID de la tarea a eliminar.</param>
   /// <response code="204">Tarea eliminada correctamente.</response>
   /// <response code="404">Tarea no encontrada.</response>
   [HttpDelete("{id}")]
   [ProducesResponseType(StatusCodes.Status204NoContent)]
   [ProducesResponseType(StatusCodes.Status404NotFound)]
   public async Task<IActionResult> Delete(int id)
   {
      await _mediator.Send(new DeleteTaskCommand(id));
      return NoContent();
   }
   
   /// <summary>
   /// Obtiene una tarea por ID.
   /// </summary>
   /// <param name="id">ID de la tarea.</param>
   /// <returns>La tarea solicitada.</returns>
   /// <response code="200">Tarea encontrada.</response>
   /// <response code="404">Tarea no encontrada.</response>
   [HttpGet("{id}")]
   [ProducesResponseType(typeof(TaskDto), StatusCodes.Status200OK)]
   [ProducesResponseType(StatusCodes.Status404NotFound)]
   public async Task<IActionResult> GetById(int id)
   {
      var tarea = await _mediator.Send(new GetTaskByIdQuery(id));
      return Ok(tarea);
   }
}

