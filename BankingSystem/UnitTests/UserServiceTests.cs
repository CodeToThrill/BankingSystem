using BankingSystem.Interfaces;
using BankingSystem.Models;
using BankingSystem.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BankingSystem.UnitTests
{
    [TestClass]
    public class UserServiceTests
    {
    //    [TestMethod]
    //    public void CreateUser_ShouldCreateUser()
    //    {
    //        // Arrange
    //        var userServiceMock = new Mock<UserService>();
    //        userServiceMock.Setup(service => service.CreateUser("John Smith", "john.smith@example.com")).Returns(new User
    //        {Id = 1, 
    //            Name = "John Smith",
    //            Email = "john.smith@example.com"
    //        });


    //        var userService = new UserService(userServiceMock.Object);
    //        // Act
    //        var result = userService.CreateUser("John Smith", "john.smith@example.com");

    //        // Assert
    //        Assert.AreEqual(1, result.Id);
    //        userServiceMock.Verify(userService => userService.CreateUser("John Smith", "john.smith@example.com"), Times.Once);
    //    }

    }
}
