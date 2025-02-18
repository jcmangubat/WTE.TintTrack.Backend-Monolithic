using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Security.Claims;
using WTE.TintTrack.Api.Controllers;
using WTE.TintTrack.Common.Domain.Constants;
using WTE.TintTrack.Core.Application.DTOs;
using WTE.TintTrack.Core.Application.Interfaces;

namespace WTE.TintTrack.API.UnitTest
{
    public class UserControllerTests
    {
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _loggerMock = new Mock<ILogger<UserController>>();
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_loggerMock.Object, _userServiceMock.Object);
        }

        [Fact]
        public async Task GetTenantsForCurrentUser_ReturnsTenants_WhenTenantsExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var tenants = new List<TenantDTO>
            {
                new() {
                    Id = Guid.NewGuid(),
                    TenantCode = "T001",
                    Name = "Tenant One",
                    Description = "Description of Tenant One",
                    Email = "tenant1@example.com",
                    ContactNumber = "1234567890",
                    TenantStatus = Enums.TenantStatus.Active
                }
            };

            _userServiceMock.Setup(s => s.GetTenantsForUserAsync(userId))
                .ReturnsAsync(tenants);

            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new("sub", userId.ToString())
            }));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claims }
            };

            // Act
            var result = await _controller.GetTenantsForCurrentUser();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeEquivalentTo(tenants);
        }

        [Fact]
        public async Task GetTenantsForCurrentUser_ReturnsNotFound_WhenNoTenantsExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userServiceMock.Setup(s => s.GetTenantsForUserAsync(userId))
                .ReturnsAsync(new List<TenantDTO>());

            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new("sub", userId.ToString())
            }));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claims }
            };

            // Act
            var result = await _controller.GetTenantsForCurrentUser();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>()
                .Which.Value.Should().Be("No tenants found for the user.");
        }

        [Fact]
        public async Task GetTenantsForCurrentUser_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userServiceMock.Setup(s => s.GetTenantsForUserAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Database error"));

            var claims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new("sub", userId.ToString())
            }));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claims }
            };

            // Act
            var result = await _controller.GetTenantsForCurrentUser();

            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be(500);
        }

        [Fact]
        public async Task GetUserById_ReturnsUser_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new ApplicationUserDto
            {
                Id = userId,
                UserName = "testuser",
                Email = "test@example.com",
                PhoneNumber = "1234567890",
                EmailConfirmed = true
            };

            _userServiceMock.Setup(s => s.GetUserByIdAsync(userId))
                .ReturnsAsync(user);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeEquivalentTo(user);
        }

        [Fact]
        public async Task GetUserById_ReturnsNotFound_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userServiceMock.Setup(s => s.GetUserByIdAsync(userId))
                .ReturnsAsync((ApplicationUserDto)null);

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetUserById_ReturnsInternalServerError_WhenExceptionThrown()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userServiceMock.Setup(s => s.GetUserByIdAsync(It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Database error"));

            // Act
            var result = await _controller.GetUserById(userId);

            // Assert
            result.Should().BeOfType<ObjectResult>()
                .Which.StatusCode.Should().Be(500);
        }
    }
}
