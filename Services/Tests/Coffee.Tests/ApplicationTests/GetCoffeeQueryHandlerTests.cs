using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Coffees.Queries.CoffeeList;
using Coffee.Domain.Entities;
using Coffee.Domain.Enums;
using FluentAssertions;
using Moq;

namespace Coffee.Tests.ApplicationTests;

public sealed class GetCoffeeQueryHandlerTests
{
    private readonly Mock<ICoffeeRepository> _coffeeRepository = new();

    [Fact]
    public async Task Query_Should_ReturnCoffeeList_Success()
    {
        // Arrange
        var coffee = CoffeeEntity.Create("testName", "testDescription", 22.87m, CoffeeType.Brazil);
        var coffeeList = new List<CoffeeEntity>{ coffee, coffee, coffee };
        _coffeeRepository.Setup(key => key.GetAllAsync()).ReturnsAsync(coffeeList);

        var query = new GetCoffeeQuery();
        var handler = new GetCoffeeQueryHandler(_coffeeRepository.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
    }
}