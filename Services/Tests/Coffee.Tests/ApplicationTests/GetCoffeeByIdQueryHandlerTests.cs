using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Coffees.Queries.CoffeeById;
using Coffee.Domain.Entities;
using Coffee.Domain.Enums;
using FluentAssertions;
using Moq;

namespace Coffee.Tests.ApplicationTests;

public sealed class GetCoffeeByIdQueryHandlerTests
{
    private readonly Mock<ICoffeeRepository> _coffeeRepository = new();

    [Fact]
    public async Task Handler_Should_GetCoffeeById_Success()
    {
        // Arrange
        var coffee = CoffeeEntity.Create("testName", "testDescription", 22.86m, CoffeeType.Kenya).Value;
        _coffeeRepository.Setup(key => key.GetCoffeeEntityAsync(It.IsAny<Guid>())).ReturnsAsync(coffee);

        var query = new GetCoffeeByIdQuery(coffee.Id);
        var handler = new GetCoffeeByIdQueryHandler(_coffeeRepository.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
    }
}