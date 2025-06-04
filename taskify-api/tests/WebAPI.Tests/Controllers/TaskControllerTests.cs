using Application.TaskList.Commands.CreateTask;
using Application.TaskList.Commands.DeleteTask;
using Application.TaskList.Commands.UpdateTask;
using Application.TaskList.DTO;
using Application.TaskList.Queries.GetAllTasks;
using Application.TaskList.Queries.GetTaskById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers;

public class TaskControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly TaskController _controller;

    public TaskControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new TaskController(_mediatorMock.Object);
    }

    [Fact]
    public async Task GetAll_ShouldReturnOkWithData()
    {
        // Arrange
        var tasks = new List<TaskDto> { new TaskDto { Id = 1, Title = "Test" } };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllTaksQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(tasks);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(tasks, okResult.Value);
    }

    [Fact]
    public async Task Create_ShouldReturn201Created()
    {
        // Arrange
        var command = new CreateTaskCommand { Title = "Test" };
        var createdTask = new TaskDto { Id = 1, Title = "Test" };

        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(createdTask);

        // Act
        var result = await _controller.Create(command);

        // Assert
        var createdResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
        Assert.Equal(createdTask, createdResult.Value);
    }

    [Fact]
    public async Task Update_IdMismatch_ReturnsBadRequest()
    {
        // Arrange
        var command = new UpdateTaskCommand { Id = 5 };
        int urlId = 6;

        // Act
        var result = await _controller.Update(urlId, command);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("ID mismatch", badRequest.Value);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent()
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteTaskCommand>(), It.IsAny<CancellationToken>()))
                     .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task GetById_ShouldReturnOkWithData()
    {
        // Arrange
        var task = new TaskDto { Id = 1, Title = "Sample" };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetTaskByIdQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(task);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(task, okResult.Value);
    }
}
