using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using WTE.TintTrack.Application.Shared.Interfaces;
using WTE.TintTrack.Business.Application.DTOs;
using WTE.TintTrack.Business.Application.Services;
using WTE.TintTrack.Business.Domain.Entities;
using WTE.TintTrack.Business.Domain.Interfaces.Repositories;
using Xunit;

namespace WTE.TintTrack.Business.Tests.Services;

public class CustomerServiceTests
{
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ILogger<CustomerService>> _loggerMock;
    private readonly Mock<IMessageProviderService> _messageProviderMock;
    private readonly Mock<ICustomerRepository> _repositoryMock;
    private readonly CustomerService _service;

    public CustomerServiceTests()
    {
        _mapperMock = new Mock<IMapper>();
        _loggerMock = new Mock<ILogger<CustomerService>>();
        _messageProviderMock = new Mock<IMessageProviderService>();
        _repositoryMock = new Mock<ICustomerRepository>();
        
        _service = new CustomerService(
            _mapperMock.Object,
            _loggerMock.Object,
            _messageProviderMock.Object,
            _repositoryMock.Object);
    }

    [Fact]
    public async Task GetByCodeAsync_ReturnsCustomer_WhenExists()
    {
        // Arrange
        var code = "C001";
        var customerId = Guid.NewGuid();
        var customer = new Customer 
        { 
            Id = customerId, 
            Code = code, 
            Name = "Test Customer",
            MainPhone = "123-456-7890"
        };
        var customerDto = new CustomerDto 
        { 
            Id = customerId, 
            Code = code, 
            Name = "Test Customer",
            MainPhone = "123-456-7890"
        };

        _repositoryMock.Setup(r => r.GetSingleAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Customer, bool>>>()))
            .ReturnsAsync(customer);
        _mapperMock.Setup(m => m.Map<CustomerDto>(customer))
            .Returns(customerDto);

        // Act
        var result = await _service.GetByCodeAsync(code);

        // Assert
        result.Should().NotBeNull();
        result!.Code.Should().Be(code);
        result.Name.Should().Be("Test Customer");
        _repositoryMock.Verify(r => r.GetSingleAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Customer, bool>>>()), Times.Once);
    }

    [Fact]
    public async Task GetByCodeAsync_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var code = "C999";
        _repositoryMock.Setup(r => r.GetSingleAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Customer, bool>>>()))
            .ReturnsAsync((Customer?)null);

        // Act
        var result = await _service.GetByCodeAsync(code);

        // Assert
        result.Should().BeNull();
        _repositoryMock.Verify(r => r.GetSingleAsync(It.IsAny<System.Linq.Expressions.Expression<Func<Customer, bool>>>()), Times.Once);
    }
}

