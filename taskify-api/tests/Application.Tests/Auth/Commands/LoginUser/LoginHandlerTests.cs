using Application.Auth.Commands.LoginUser;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using Moq;

namespace Application.Tests.Auth.Commands.LoginUser;

public class LoginHandlerTests
{
   
   [Fact]
   public async Task Should_Throw_When_User_Not_Found()
   {
      // Arrange
      var userRepository = new Mock<IUserRepository>();
      var passwordHasher = new Mock<IPasswordHasher>();
      var utils = new Mock<IUtils>();

      userRepository.Setup(r => r.GetByEmailAsync("missing@example.com"))
         .ReturnsAsync((UserEntity?)null);

      var handler = new LoginHandler(userRepository.Object, passwordHasher.Object, utils.Object);

      var command = new LoginCommand("missing@example.com", "password");

      // Act & Assert
      await Assert.ThrowsAsync<InvalidCredentialsException>(() => handler.Handle(command, CancellationToken.None));
   }

   [Fact]
   public async Task Should_Throw_When_Password_Invalid()
   {
      var user = new UserEntity { Email = "user@example.com", Password = "hashedPassword" };

      var userRepository = new Mock<IUserRepository>();
      userRepository.Setup(r => r.GetByEmailAsync("user@example.com")).ReturnsAsync(user);

      var passwordHasher = new Mock<IPasswordHasher>();
      passwordHasher.Setup(h => h.Verify("wrongPassword", "hashedPassword")).Returns(false);

      var utils = new Mock<IUtils>();

      var handler = new LoginHandler(userRepository.Object, passwordHasher.Object, utils.Object);

      var command = new LoginCommand("user@example.com", "wrongPassword");

      await Assert.ThrowsAsync<InvalidCredentialsException>(() => handler.Handle(command, CancellationToken.None));
   }
   
   [Fact]
   public async Task Should_Return_Token_When_Credentials_Are_Valid()
   {
      var user = new UserEntity { Email = "user@example.com", Password = "hashedPassword" };

      var userRepository = new Mock<IUserRepository>();
      userRepository.Setup(r => r.GetByEmailAsync("user@example.com")).ReturnsAsync(user);

      var passwordHasher = new Mock<IPasswordHasher>();
      passwordHasher.Setup(h => h.Verify("correctPassword", "hashedPassword")).Returns(true);

      var utils = new Mock<IUtils>();
      utils.Setup(u => u.GenerateToken(user)).Returns("mocked-token");

      var handler = new LoginHandler(userRepository.Object, passwordHasher.Object, utils.Object);

      var command = new LoginCommand("user@example.com", "correctPassword");

      var token = await handler.Handle(command, CancellationToken.None);

      Assert.Equal("mocked-token", token);
   }


}