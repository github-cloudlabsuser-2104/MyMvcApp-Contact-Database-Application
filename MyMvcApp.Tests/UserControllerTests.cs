using Xunit;
using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Controllers;
using MyMvcApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyMvcApp.Tests
{
    public class UserControllerTests
    {
        private UserController _controller;

        public UserControllerTests()
        {
            // Inicializa el controlador y la lista de usuarios
            _controller = new UserController();
            UserController.userlist = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com" },
                new User { Id = 2, Name = "Jane Doe", Email = "jane@example.com" }
            };
        }

        [Fact]
        public void Index_ReturnsViewResult_WithListOfUsers()
        {
            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<User>>(viewResult.ViewData.Model);
            Assert.Equal(2, model.Count);
        }

        [Fact]
        public void Details_ReturnsViewResult_WithUser()
        {
            // Act
            var result = _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<User>(viewResult.ViewData.Model);
            Assert.Equal("John Doe", model.Name);
        }

        [Fact]
        public void Details_ReturnsNotFound_WhenUserNotFound()
        {
            // Act
            var result = _controller.Details(3);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Create_ReturnsViewResult()
        {
            // Act
            var result = _controller.Create();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Create_Post_RedirectsToIndex_WhenModelIsValid()
        {
            // Arrange
            var newUser = new User { Id = 3, Name = "Sam Smith", Email = "sam@example.com" };

            // Act
            var result = _controller.Create(newUser);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal(3, UserController.userlist.Count);
        }

        [Fact]
        public void Edit_ReturnsViewResult_WithUser()
        {
            // Act
            var result = _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<User>(viewResult.ViewData.Model);
            Assert.Equal("John Doe", model.Name);
        }

        [Fact]
        public void Edit_ReturnsNotFound_WhenUserNotFound()
        {
            // Act
            var result = _controller.Edit(3);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_Post_RedirectsToIndex_WhenModelIsValid()
        {
            // Arrange
            var updatedUser = new User { Id = 1, Name = "John Smith", Email = "johnsmith@example.com" };

            // Act
            var result = _controller.Edit(1, updatedUser);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal("John Smith", UserController.userlist.First(u => u.Id == 1).Name);
        }

        [Fact]
        public void Delete_ReturnsViewResult_WithUser()
        {
            // Act
            var result = _controller.Delete(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<User>(viewResult.ViewData.Model);
            Assert.Equal("John Doe", model.Name);
        }

        [Fact]
        public void Delete_ReturnsNotFound_WhenUserNotFound()
        {
            // Act
            var result = _controller.Delete(3);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Delete_Post_RedirectsToIndex_WhenUserIsDeleted()
        {
            // Act
            var result = _controller.Delete(1, null);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Single(UserController.userlist);
        }
    }
}