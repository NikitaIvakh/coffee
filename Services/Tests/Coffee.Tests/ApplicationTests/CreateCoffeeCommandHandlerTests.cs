using Moq;
using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Coffees.Handlers.Create;
using Coffee.Application.Providers;
using Coffee.Domain.DTOs;
using Coffee.Domain.Entities;
using Coffee.Domain.Enums;
using Coffee.Domain.Shared;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace Coffee.Tests.ApplicationTests;

public sealed class CreateCoffeeCommandHandlerTests
{
    private readonly Mock<ICoffeeRepository> _couponRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<HttpContextAccessor> _httpContextAccessor = new();
    private readonly Mock<ICacheProvider> _cacheProvider = new();

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult()
    {
        // Arrange
        var coffee = new CreateCoffeeDto("test123", "test123", 20.5m, CoffeeType.Brazil, null); 
        
        var command = new CreateCoffeeCommand(coffee);
        var handler = new CreateCoffeeCommandHandler(_couponRepositoryMock.Object, _unitOfWorkMock.Object, _httpContextAccessor.Object, _cacheProvider.Object);

       // Act
       var result = await handler.Handle(command, default);

       // Assert
       result.IsSuccess.Should().BeTrue();
       result.IsFailure.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_Should_CallCoffeeRepositoryCreate_Once()
    {
        // Arrange
        var coffee = new CreateCoffeeDto("test123", "test123", 20.5m, CoffeeType.Brazil, null);
        var command = new CreateCoffeeCommand(coffee);
        var handler = new CreateCoffeeCommandHandler(_couponRepositoryMock.Object, _unitOfWorkMock.Object, _httpContextAccessor.Object, _cacheProvider.Object);
        
        // Act
        await handler.Handle(command, default);

        // Assert
        _couponRepositoryMock.Verify(key => key.CreateAsync(It.IsAny<CoffeeEntity>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWorkCreate_Once()
    {
        // Arrange
        var coffee = new CreateCoffeeDto("test123", "test123", 20.5m, CoffeeType.Brazil, null); 
        var command = new CreateCoffeeCommand(coffee);
        var handler = new CreateCoffeeCommandHandler(_couponRepositoryMock.Object, _unitOfWorkMock.Object, _httpContextAccessor.Object, _cacheProvider.Object);
        
        // Act
        await handler.Handle(command, default);

        // Assert
        _unitOfWorkMock.Verify(key => key.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
    }
}