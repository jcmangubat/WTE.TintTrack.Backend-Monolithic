using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using WTE.TintTrack.Api.Controllers;
using WTE.TintTrack.Api.Models;
using WTE.TintTrack.Core.Application.DTOs;
using WTE.TintTrack.Core.Application.Interfaces;
using WTE.TintTrack.Core.Domain.Entities;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace WTE.TintTrack.API.UnitTest
{
    public class LoginControllerTests
    {
        private readonly Mock<ILogger<LoginController>> _loggerMock;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
        private readonly Mock<IOptions<JwtSettings>> _jwtSettingsMock;
        private readonly Mock<ITenantService> _tenantServiceMock; // Add this
        private readonly LoginController _controller;

        public LoginControllerTests()
        {
            _loggerMock = new Mock<ILogger<LoginController>>();
            _userManagerMock = MockUserManager();
            _signInManagerMock = MockSignInManager();

            // Setting up the JwtSettings
            var jwtSettings = new JwtSettings
            {
                Key = "FZ7R@0n!q4XPm8n3l^A0YvZ9F6J1kO!9x&%2y*",
                Issuer = "https://localhost:7080"
            };

            _jwtSettingsMock = new Mock<IOptions<JwtSettings>>();
            _jwtSettingsMock.Setup(m => m.Value).Returns(jwtSettings);

            // Mocking the ITenantService
            _tenantServiceMock = new Mock<ITenantService>();

            // Instantiating the LoginController
            _controller = new LoginController(
                _loggerMock.Object,
                _userManagerMock.Object,
                _signInManagerMock.Object,
                _jwtSettingsMock.Object,
                _tenantServiceMock.Object); // Pass the tenant service
        }

        [Fact]
        public async Task Token_ReturnsOkWithToken_WhenCredentialsAreValidAndTenantExists()
        {
            // Arrange
            var model = new LoginModel
            {
                Username = "testuser",
                Password = "password123",
                TenantCode = "tenant123" // Assuming TenantCode is part of LoginModel
            };

            var user = new ApplicationUser
            {
                UserName = model.Username
            };

            // Mocking the SignInManager
            _signInManagerMock.Setup(s => s.PasswordSignInAsync(model.Username, model.Password, false, false))
                .ReturnsAsync(SignInResult.Success);

            // Mocking the UserManager
            _userManagerMock.Setup(u => u.FindByNameAsync(model.Username))
                .ReturnsAsync(user);

            // Mocking the TenantService
            _tenantServiceMock.Setup(t => t.GetTenantByCodeAsync(model.TenantCode))
                // Simulate tenant exists
                .ReturnsAsync(new TenantDTO
                {
                    ContactNumber = "0123456789",
                    Description = "Tenant 1",
                    Email = "tenant1@test.com",
                    Name = "Tenant 1",
                    TenantCode = model.TenantCode,
                    TenantStatus = Common.Domain.Constants.Enums.TenantStatus.Active
                });

            // Act
            var result = await _controller.Token(model);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tokenResponse = okResult.Value as TokenResponse;
            tokenResponse.Should().NotBeNull();
            tokenResponse.Token.Should().NotBeNullOrWhiteSpace();

            // Optional: Validate the JWT structure
            var handler = new JwtSecurityTokenHandler();
            handler.CanReadToken(tokenResponse.Token).Should().BeTrue();
        }

        [Fact]
        public async Task Token_ReturnsUnauthorized_WhenCredentialsAreInvalid()
        {
            // Arrange
            var model = new LoginModel
            {
                Username = "invaliduser",
                Password = "wrongpassword",
                TenantCode = "tenant123"
            };

            _signInManagerMock.Setup(s => s.PasswordSignInAsync(model.Username, model.Password, false, false))
                .ReturnsAsync(SignInResult.Failed);

            // Act
            var result = await _controller.Token(model);

            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task Token_ReturnsUnauthorized_WhenUserNotFound()
        {
            // Arrange
            var model = new LoginModel
            {
                Username = "nonexistentuser",
                Password = "password123",
                TenantCode = "tenant123"
            };

            _signInManagerMock.Setup(s => s.PasswordSignInAsync(model.Username, model.Password, false, false))
                .ReturnsAsync(SignInResult.Success);

            _userManagerMock.Setup(u => u.FindByNameAsync(model.Username))
                .ReturnsAsync((ApplicationUser)null);

            // Act
            var result = await _controller.Token(model);

            // Assert
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [Fact]
        public async Task Token_ReturnsUnauthorized_WhenTenantNotFound()
        {
            // Arrange
            var model = new LoginModel
            {
                Username = "testuser",
                Password = "password123",
                TenantCode = "nonexistenttenant"
            };

            var user = new ApplicationUser
            {
                UserName = model.Username
            };

            // Mocking the SignInManager
            _signInManagerMock.Setup(s => s.PasswordSignInAsync(model.Username, model.Password, false, false))
                .ReturnsAsync(SignInResult.Success);

            // Mocking the UserManager
            _userManagerMock.Setup(u => u.FindByNameAsync(model.Username))
                .ReturnsAsync(user);

            // Mocking the TenantService
            _tenantServiceMock.Setup(t => t.GetTenantByCodeAsync(model.TenantCode))
                .ReturnsAsync((TenantDTO)null); // Make sure this matches the return type

            // Act
            var result = await _controller.Token(model);

            // Assert
            result.Should().BeOfType<UnauthorizedObjectResult>(); // Updated for clearer error handling
        }

        [Fact]
        public async Task Token_IncludesTenantClaim_WhenLoginIsSuccessful()
        {
            // Arrange
            var model = new LoginModel
            {
                Username = "testuser",
                Password = "password123",
                TenantCode = "tenant123"
            };

            var user = new ApplicationUser
            {
                UserName = model.Username
            };

            var tenant = new TenantDTO
            {
                ContactNumber = "0123456789",
                Description = "Tenant 1",
                Email = "tenant1@test.com",
                Name = "Tenant 1",
                TenantCode = model.TenantCode,
                TenantStatus = Common.Domain.Constants.Enums.TenantStatus.Active
            };

            // Mocking the SignInManager
            _signInManagerMock.Setup(s => s.PasswordSignInAsync(model.Username, model.Password, false, false))
                .ReturnsAsync(SignInResult.Success);

            // Mocking the UserManager
            _userManagerMock.Setup(u => u.FindByNameAsync(model.Username))
                .ReturnsAsync(user);

            // Mocking the TenantService
            _tenantServiceMock.Setup(t => t.GetTenantByCodeAsync(model.TenantCode))
                .ReturnsAsync(tenant); // Return a valid tenant

            // Act
            var result = await _controller.Token(model);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var tokenResponse = okResult.Value as TokenResponse;
            tokenResponse.Should().NotBeNull();
            tokenResponse.Token.Should().NotBeNullOrWhiteSpace();

            // Optional: Validate the JWT structure and check for the tenant claim
            var handler = new JwtSecurityTokenHandler();
            handler.CanReadToken(tokenResponse.Token).Should().BeTrue();
            var jwtToken = handler.ReadJwtToken(tokenResponse.Token);

            // Check if the tenant claim is present
            var tenantClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "tenant");
            tenantClaim.Should().NotBeNull("Tenant claim should be included in the JWT token.");
            tenantClaim.Value.Should().Be(model.TenantCode, "Tenant claim value should match the tenant the user is logging into.");
        }

        private static Mock<UserManager<ApplicationUser>> MockUserManager()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
        }

        private static Mock<SignInManager<ApplicationUser>> MockSignInManager()
        {
            var userManagerMock = MockUserManager();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            return new Mock<SignInManager<ApplicationUser>>(userManagerMock.Object, contextAccessor.Object, claimsFactory.Object, null, null, null, null);
        }
    }
}
