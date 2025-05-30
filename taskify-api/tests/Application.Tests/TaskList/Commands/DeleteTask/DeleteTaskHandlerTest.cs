using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.TaskList.Commands.DeleteTask;
using Domain.Entities;
using Moq;

namespace Application.Tests.TaskList.Commands.DeleteTask;

public class DeleteTaskHandlerTest
{
   private readonly Mock<ITaskRepository> _taskRepositoryMock;
   private readonly DeleteTaskHandler _handler;

   public DeleteTaskHandlerTest()
   {
      _taskRepositoryMock = new Mock<ITaskRepository>();
      _handler = new DeleteTaskHandler(_taskRepositoryMock.Object);
   }
   
   [Fact]
   public async Task Handle_ShouldDeleteTask_WhenTaskExists()
   {
      // Arrange
      var taskEntity = new TaskEntity { Id = 1, Title = "Task 1", Description = "Desc 1", DateOfCreation = new DateTime().Date, Completed = false };
      var command = new DeleteTaskCommand(1);

      _taskRepositoryMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync(taskEntity);
      _taskRepositoryMock.Setup(r => r.DeleteAsync(taskEntity)).Returns(Task.CompletedTask);

      // Act
      await _handler.Handle(command, CancellationToken.None);

      // Assert
      _taskRepositoryMock.Verify(r => r.GetByIdAsync(command.Id), Times.Once);
      _taskRepositoryMock.Verify(r => r.DeleteAsync(taskEntity), Times.Once);
   }
   
   [Fact]
   public async Task Handle_ShouldThrowException_WhenTaskDoesNotExist()
   {
      // Arrange
      var command = new DeleteTaskCommand(99);

      _taskRepositoryMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync((TaskEntity?)null);

      // Act & Assert
      var exception = await Assert.ThrowsAsync<ResourceNotFoundException>(() =>
         _handler.Handle(command, CancellationToken.None)
      );

      Assert.Equal("Task Not found", exception.Message);
      _taskRepositoryMock.Verify(r => r.GetByIdAsync(command.Id), Times.Once);
      _taskRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<TaskEntity>()), Times.Never);
   }

   
}