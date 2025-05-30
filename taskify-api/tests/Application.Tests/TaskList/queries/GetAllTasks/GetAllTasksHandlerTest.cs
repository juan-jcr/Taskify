using Application.Common.Interfaces;
using Application.TaskList.DTO;
using Application.TaskList.Queries.GetAllTasks;
using AutoMapper;
using Domain.Entities;
using Moq;

namespace Application.Tests.TaskList.queries.GetAllTasks;

public class GetAllTasksHandlerTest
{
   private readonly Mock<ITaskRepository> _taskRepositoryMock;
   private readonly Mock<IMapper> _mapperMock;
   private readonly GetAllTasksHandler _handler;

   public GetAllTasksHandlerTest()
   {
      _taskRepositoryMock = new Mock<ITaskRepository>();
      _mapperMock = new Mock<IMapper>();
      _handler = new GetAllTasksHandler(_taskRepositoryMock.Object, _mapperMock.Object);
   }
   
   [Fact]
   public async Task Handle_ShouldReturnMappedTaskDtos()
   {
      // Arrange
      var tasks = new List<TaskEntity>
      {
         new TaskEntity { Id = 1, Title = "Task 1", Description = "Desc 1", DateOfCreation = new DateTime().Date, Completed = false},
         new TaskEntity { Id = 2, Title = "Task 2", Description = "Desc 2" , DateOfCreation = new DateTime().Date, Completed = false}
      };

      var taskDtos = new List<TaskDto>
      {
         new TaskDto { Id = 1, Title = "Task 1", Description = "Desc 1", DateOfCreation = new DateTime().Date, Completed = false},
         new TaskDto { Id = 2, Title = "Task 2", Description = "Desc 2" , DateOfCreation = new DateTime().Date, Completed = false }
      };

      _taskRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(tasks);
      _mapperMock.Setup(m => m.Map<IEnumerable<TaskDto>>(tasks)).Returns(taskDtos);

      var query = new GetAllTaksQuery(); 

      // Act
      var result = await _handler.Handle(query, CancellationToken.None);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(2, result is List<TaskDto> list ? list.Count : 0);
      Assert.Collection(result,
         item => Assert.Equal("Task 1", item.Title),
         item => Assert.Equal("Task 2", item.Title)
      );

      _taskRepositoryMock.Verify(r => r.GetAllAsync(), Times.Once);
   }
   
}