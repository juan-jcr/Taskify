using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.TaskList.DTO;
using Application.TaskList.Queries.GetTaskById;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace Application.Tests.TaskList.queries.GetTaskById;

public class GetTaskByIdHandlerTests
{
   private readonly Mock<ITaskRepository> _taskRepositoryMock;
   private readonly Mock<IMapper> _mapperMock;
   private readonly GetTaskByIdHandler _handler;

   public GetTaskByIdHandlerTests()
   {
      _taskRepositoryMock = new Mock<ITaskRepository>();
      _mapperMock = new Mock<IMapper>();
      _handler = new GetTaskByIdHandler(
         _taskRepositoryMock.Object,
         _mapperMock.Object
      );
   }
   
   [Fact]
   public async Task Handle_ShouldReturnTaskDto_WhenTaskExists()
   {
      // Arrange
      var taskEntity = new TaskEntity { Id = 1, Title = "Test Task" };
      var expectedDto = new TaskDto { Id = 1, Title = "Test Task" };
      var query = new GetTaskByIdQuery(1);

      _taskRepositoryMock.Setup(r => r.GetByIdAsync(query.Id)).ReturnsAsync(taskEntity);
      _mapperMock.Setup(m => m.Map<TaskDto>(taskEntity)).Returns(expectedDto);

      // Act
      var result = await _handler.Handle(query, CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(expectedDto.Id, result.Id);
      Assert.Equal(expectedDto.Title, result.Title);
      _taskRepositoryMock.Verify(r => r.GetByIdAsync(query.Id), Times.Once);
      _mapperMock.Verify(m => m.Map<TaskDto>(taskEntity), Times.Once);
   }

   [Fact]
   public async Task Handle_ShouldThrowResourceNotFoundException_WhenTaskDoesNotExist()
   {
      // Arrange
      var query = new GetTaskByIdQuery(99);
      _taskRepositoryMock.Setup(r => r.GetByIdAsync(query.Id)).ReturnsAsync((TaskEntity?)null);

      // Act & Assert
      var ex = await Assert.ThrowsAsync<ResourceNotFoundException>(() =>
         _handler.Handle(query, CancellationToken.None)
      );

      Assert.Equal("Task not found", ex.Message);
      _taskRepositoryMock.Verify(r => r.GetByIdAsync(query.Id), Times.Once);
      _mapperMock.Verify(m => m.Map<TaskDto>(It.IsAny<TaskEntity>()), Times.Never);
   }
   
}