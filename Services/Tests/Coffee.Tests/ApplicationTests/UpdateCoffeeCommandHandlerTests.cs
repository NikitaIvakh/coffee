using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Coffees.Handlers.Update;
using Coffee.Domain.DTOs;
using Coffee.Domain.Entities;
using Coffee.Domain.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Coffee.Tests.ApplicationTests;

public sealed class UpdateCoffeeCommandHandlerTests
{
    private readonly Mock<ICoffeeRepository> _coffeeRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<HttpContextAccessor> _httpContextAccessor = new();

    [Fact]
    public async Task Handle_Should_SuccessfullyUpdated()
    {
        // Arrange
        var coffee = CoffeeEntity.Create("test12232", "description", 23.78m, CoffeeType.Kenya).Value;
        _coffeeRepositoryMock.Setup(key => key.GetCoffeeEntityAsync(It.IsAny<Guid>())).ReturnsAsync(coffee);
        
        var updateCoffee = new UpdateCoffeeDto(coffee.Id, "testName", "testDescription", 99, CoffeeType.Brazil, null, "text", "text");
        var command = new UpdateCoffeeCommand(updateCoffee);
        var handler = new UpdateCoffeeCommandHandler(_coffeeRepositoryMock.Object, _unitOfWorkMock.Object, _httpContextAccessor.Object);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.IsFailure.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_Should_SuccessfullyUpdated_RepositoryUpdateCall_Twice()
    {
        // Arrange
        var coffee = CoffeeEntity.Create("test12232", "description", 23.78m, CoffeeType.Kenya).Value;
        _coffeeRepositoryMock.Setup(key => key.GetCoffeeEntityAsync(It.IsAny<Guid>())).ReturnsAsync(coffee);
        
        var updateCoffee = new UpdateCoffeeDto(coffee.Id, "testName", "testDescription", 99, CoffeeType.Brazil, null, "text", "text");
        var command = new UpdateCoffeeCommand(updateCoffee);
        var handler = new UpdateCoffeeCommandHandler(_coffeeRepositoryMock.Object, _unitOfWorkMock.Object, _httpContextAccessor.Object);
        
        // Act
        await handler.Handle(command, default);

        // Assert
        _coffeeRepositoryMock.Verify(key => key.UpdateAsync(It.IsAny<CoffeeEntity>()), Times.Exactly(2));
    }

    [Fact]
    public async Task Handle_Should_SuccessfullyUpdated_UnitOfWorkCall_Twice()
    {
        // Arrange
        var coffee = CoffeeEntity.Create("test12232", "description", 23.78m, CoffeeType.Kenya).Value;
        _coffeeRepositoryMock.Setup(key => key.GetCoffeeEntityAsync(It.IsAny<Guid>())).ReturnsAsync(coffee);
        
        var updateCoffee = new UpdateCoffeeDto(coffee.Id, "testName", "testDescription", 99, CoffeeType.Brazil, null, "text", "text");
        var command = new UpdateCoffeeCommand(updateCoffee);
        var handler = new UpdateCoffeeCommandHandler(_coffeeRepositoryMock.Object, _unitOfWorkMock.Object, _httpContextAccessor.Object);
        
        // Act
        await handler.Handle(command, default);
        
        // Assert
        _unitOfWorkMock.Verify(key => key.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Exactly(2));
    }
}