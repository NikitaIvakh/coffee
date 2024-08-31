using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Coffees.Handlers.Delete;
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

    [Fact]
    public async Task Handle_Should_SuccessRemoveCoffee()
    {
        // Arrange
        var coffee = CoffeeEntity.Create("testName", "testDescription", 23.90m, CoffeeType.Columbia);
        _coffeeRepositoryMock.Setup(key => key.GetCoffeeEntityAsync(It.IsAny<Guid>())).ReturnsAsync(coffee);

        var deleteCoffeeDto = new DeleteCoffeeDto(coffee.Id);
        var command = new DeleteCoffeeCommand(deleteCoffeeDto);
        var handler = new DeleteCoffeeCommandHandler(_coffeeRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_Should_RepositoryCallOnce()
    {
        // Arrange
        var coffee = CoffeeEntity.Create("testName", "testDescription", 23.90m, CoffeeType.Columbia);
        _coffeeRepositoryMock.Setup(key => key.GetCoffeeEntityAsync(It.IsAny<Guid>())).ReturnsAsync(coffee);

        var deleteCoffeeDto = new DeleteCoffeeDto(coffee.Id);
        var command = new DeleteCoffeeCommand(deleteCoffeeDto);
        var handler = new DeleteCoffeeCommandHandler(_coffeeRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        await handler.Handle(command, default);
        
        // Assert
        _coffeeRepositoryMock.Verify(key => key.DeleteAsync(It.IsAny<CoffeeEntity>()), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_UnitOfWorkCallOnce()
    {
        // Arrange
        var coffee = CoffeeEntity.Create("testName", "testDescription", 23.90m, CoffeeType.Columbia);
        _coffeeRepositoryMock.Setup(key => key.GetCoffeeEntityAsync(It.IsAny<Guid>())).ReturnsAsync(coffee);

        var deleteCoffeeDto = new DeleteCoffeeDto(coffee.Id);
        var command = new DeleteCoffeeCommand(deleteCoffeeDto);
        var handler = new DeleteCoffeeCommandHandler(_coffeeRepositoryMock.Object, _unitOfWorkMock.Object);

        // Act
        await handler.Handle(command, default);
        
        // Assert
        _unitOfWorkMock.Verify(key => key.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}