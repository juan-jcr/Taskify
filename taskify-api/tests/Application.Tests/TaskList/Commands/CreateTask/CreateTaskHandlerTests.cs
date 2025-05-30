using Application.Common.Interfaces;
using Application.TaskList.Commands.CreateTask;
using Application.TaskList.DTO;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace Application.Tests.TaskList.Commands.CreateTask;

public class CreateTaskHandlerTests 
{
   private readonly Mock<ITaskRepository> _taskRepositoryMock;
   private readonly Mock<IMapper> _mapperMock;
   private readonly Mock<ICurrentUserService> _currentUserServiceMock;
   private readonly CreateTaskHandler _handler;

   public CreateTaskHandlerTests()
   {
      _taskRepositoryMock = new Mock<ITaskRepository>();
      _mapperMock = new Mock<IMapper>();
      _currentUserServiceMock = new Mock<ICurrentUserService>();
      _handler = new CreateTaskHandler(
         _taskRepositoryMock.Object,
         _mapperMock.Object,
         _currentUserServiceMock.Object
      );
   }
   
   
   [Fact]
   public async Task Handle_ShouldCreateTaskAndReturnTaskDto()
   {
      // Arrange
      var command = new CreateTaskCommand
      {
         Title = "Test Task",
         Description = "Test Description",
         DateOfCreation = new DateTime().Date,
      };

      var taskEntity = new TaskEntity {  Id = 1, Title = "Task 1", Description = "Desc 1", DateOfCreation = new DateTime().Date, Completed = false };
      var taskDto = new TaskDto { Id = 1, Title = "Task 1", Description = "Desc 1", DateOfCreation = new DateTime().Date, Completed = false };

      _mapperMock.Setup(m => m.Map<TaskEntity>(command)).Returns(taskEntity);
      _currentUserServiceMock.Setup(c => c.UserId).Returns("42");
      _taskRepositoryMock.Setup(r => r.AddAsync(taskEntity)).Returns(Task.CompletedTask);
      _mapperMock.Setup(m => m.Map<TaskDto>(taskEntity)).Returns(taskDto);

      // Act
      var result = await _handler.Handle(command, CancellationToken.None);

      // Assert
      Assert.Equal(taskDto.Id, result.Id);
      Assert.Equal(taskDto.Title, result.Title);

      _taskRepositoryMock.Verify(r => r.AddAsync(It.IsAny<TaskEntity>()), Times.Once);
   }
}