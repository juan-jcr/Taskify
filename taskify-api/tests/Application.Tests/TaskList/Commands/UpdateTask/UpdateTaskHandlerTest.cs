using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.TaskList.Commands.UpdateTask;
using Application.TaskList.DTO;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace Application.Tests.TaskList.Commands.UpdateTask;

public class UpdateTaskHandlerTest
{
   private readonly Mock<ITaskRepository> _taskRepositoryMock;
   private readonly Mock<IMapper> _mapperMock;
   private readonly Mock<ICurrentUserService> _currentUserServiceMock;
   private readonly UpdateTaskHandler _handler;

   public UpdateTaskHandlerTest()
   {
      _taskRepositoryMock = new Mock<ITaskRepository>();
      _mapperMock = new Mock<IMapper>();
      _currentUserServiceMock = new Mock<ICurrentUserService>();
      _handler = new UpdateTaskHandler(
         _taskRepositoryMock.Object,
         _mapperMock.Object,
         _currentUserServiceMock.Object
      );
   }
   
   [Fact]
   public async Task Handle_ShouldUpdateTask_WhenTaskExists()
   {
      // Arrange
      var command = new UpdateTaskCommand
      {
         Id = 1,
         Title = "Test Task",
         Description = "Test Description",
      };

      var taskEntity = new TaskEntity
      {
         Id = 1,
         Title = "Old Title",
         Description = "Old Description",
         UserId = 0
      };

      var updatedDto = new TaskDto { Id = 1, Title = "Updated Title" };

      _taskRepositoryMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(taskEntity);
      _mapperMock.Setup(m => m.Map(command, taskEntity));
      _currentUserServiceMock.Setup(s => s.UserId).Returns("123");
      _taskRepositoryMock.Setup(r => r.UpdateAsync(taskEntity)).Returns(Task.CompletedTask);
      _mapperMock.Setup(m => m.Map<TaskDto>(taskEntity)).Returns(updatedDto);

      // Act
      var result = await _handler.Handle(command, CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(updatedDto.Id, result.Id);
      Assert.Equal(updatedDto.Title, result.Title);
      Assert.Equal(123, taskEntity.UserId);

      _taskRepositoryMock.Verify(r => r.GetByIdAsync(command.Id), Times.Once);
      _mapperMock.Verify(m => m.Map(command, taskEntity), Times.Once);
      _taskRepositoryMock.Verify(r => r.UpdateAsync(taskEntity), Times.Once);
   }
   
   [Fact]
   public async Task Handle_ShouldThrowException_WhenTaskDoesNotExist()
   {
      // Arrange
      var command = new UpdateTaskCommand { Id = 99 };
      _taskRepositoryMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync((TaskEntity?)null);

      // Act & Assert
      var ex = await Assert.ThrowsAsync<ResourceNotFoundException>(() =>
         _handler.Handle(command, CancellationToken.None)
      );

      Assert.Equal("Task with id 99 not found.", ex.Message);
      _taskRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<TaskEntity>()), Times.Never);
   }
   
}