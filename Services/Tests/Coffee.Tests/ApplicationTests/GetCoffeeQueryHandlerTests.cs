using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Coffees.Queries.CoffeeList;
using Coffee.Application.Providers;
using Coffee.Domain.Entities;
using Coffee.Domain.Enums;
using FluentAssertions;
using Moq;

namespace Coffee.Tests.ApplicationTests;

public sealed class GetCoffeeQueryHandlerTests
{
    private readonly Mock<ICoffeeRepository> _coffeeRepository = new();
    private readonly Mock<ICacheProvider> _cacheProvider = new();

    [Fact]
    public async Task Query_Should_ReturnCoffeeList_Success()
    {
        // Arrange
        var coffee = CoffeeEntity.Create("test12232", "description", 23.78m, CoffeeType.Kenya).Value;
        var coffeeList = new List<CoffeeEntity>{ coffee, coffee, coffee };
        _coffeeRepository.Setup(key => key.GetAllAsync()).ReturnsAsync(coffeeList);

        var query = new GetCoffeeQuery();
        var handler = new GetCoffeeQueryHandler(_coffeeRepository.Object, _cacheProvider.Object);

        // Act
        var result = await handler.Handle(query, default);

        // Assert
        result.Should().NotBeNull();
    }
}