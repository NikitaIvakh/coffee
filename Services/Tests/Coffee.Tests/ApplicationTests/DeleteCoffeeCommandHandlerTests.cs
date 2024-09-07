using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Coffees.Handlers.Delete;
using Coffee.Application.Providers;
using Coffee.Domain.DTOs;
using Coffee.Domain.Entities;
using Coffee.Domain.Enums;
using FluentAssertions;
using Moq;

namespace Coffee.Tests.ApplicationTests;

public sealed class DeleteCoffeeCommandHandlerTests
{
    private readonly Mock<ICoffeeRepository> _coffeeRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<ICacheProvider> _cacheProvider = new();

    [Fact]
    public async Task Handle_Should_SuccessRemoveCoffee()
    {
        // Arrange
        var coffee = CoffeeEntity.Create("testName", "testDescription", 23.90m, CoffeeType.Columbia).Value;
        _coffeeRepositoryMock.Setup(key => key.GetCoffeeEntityAsync(It.IsAny<Guid>())).ReturnsAsync(coffee);
        
        var command = new DeleteCoffeeCommand(coffee.Id);
        var handler = new DeleteCoffeeCommandHandler(_coffeeRepositoryMock.Object, _unitOfWorkMock.Object, _cacheProvider.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_Should_RepositoryCallOnce()
    {
        // Arrange
        var coffee = CoffeeEntity.Create("testName", "testDescription", 23.90m, CoffeeType.Columbia).Value;
        _coffeeRepositoryMock.Setup(key => key.GetCoffeeEntityAsync(It.IsAny<Guid>())).ReturnsAsync(coffee);
        
        var command = new DeleteCoffeeCommand(coffee.Id);
        var handler = new DeleteCoffeeCommandHandler(_coffeeRepositoryMock.Object, _unitOfWorkMock.Object, _cacheProvider.Object);

        // Act
        await handler.Handle(command, default);
        
        // Assert
        _coffeeRepositoryMock.Verify(key => key.DeleteAsync(It.IsAny<CoffeeEntity>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_UnitOfWorkCallOnce()
    {
        // Arrange
        var coffee = CoffeeEntity.Create("testName", "testDescription", 23.90m, CoffeeType.Columbia).Value;
        _coffeeRepositoryMock.Setup(key => key.GetCoffeeEntityAsync(It.IsAny<Guid>())).ReturnsAsync(coffee);
        
        var command = new DeleteCoffeeCommand(coffee.Id);
        var handler = new DeleteCoffeeCommandHandler(_coffeeRepositoryMock.Object, _unitOfWorkMock.Object, _cacheProvider.Object);

        // Act
        await handler.Handle(command, default);
        
        // Assert
        _unitOfWorkMock.Verify(key => key.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}