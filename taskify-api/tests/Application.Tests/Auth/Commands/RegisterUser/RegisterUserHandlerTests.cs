using Application.Auth.Commands.RegisterUser;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Moq;

namespace Application.Tests.Auth.Commands.RegisterUser;

public class RegisterUserHandlerTests
{
   [Fact]
   public async Task Should_Throw_When_Email_Already_Exists()
   {
      var mockUserRepo = new Mock<IUserRepository>();
      var mockPassword = new Mock<IPasswordHasher>();
      var handler = new RegisterUserHandler(mockUserRepo.Object, mockPassword.Object);

      var existingUser = new UserEntity { Email = "existing@example.com" };
      mockUserRepo.Setup(r => r.GetByEmailAsync("existing@example.com")).ReturnsAsync(existingUser);

      var command = new RegisterUserCommand("John", "existing@example.com", "password123");

      await Assert.ThrowsAsync<AlreadyRegisteredException>(() =>
         handler.Handle(command, CancellationToken.None));
   }
   
   [Fact]
   public async Task Should_Register_User_When_Email_Is_Not_Registered()
   {
      var mockUserRepo = new Mock<IUserRepository>();
      var mockPassword = new Mock<IPasswordHasher>();

      mockUserRepo.Setup(r => r.GetByEmailAsync("new@example.com")).ReturnsAsync((UserEntity?)null);
      mockPassword.Setup(p => p.HashPassword("password123")).Returns("hashedPassword");

      var handler = new RegisterUserHandler(mockUserRepo.Object, mockPassword.Object);

      var command = new RegisterUserCommand("Jane", "new@example.com", "password123");

      var result = await handler.Handle(command, CancellationToken.None);

      mockUserRepo.Verify(r => r.AddAsync(It.Is<UserEntity>(u =>
         u.Email == "new@example.com" &&
         u.Name == "Jane" &&
         u.Password == "hashedPassword"
      )), Times.Once);

      Assert.Equal("User registered successfully", result);
   }
    

}