using Coffee.Domain.Enums;

namespace Coffee.Application.Helpers;

public static class CoffeeTypeHelper
{
    private static readonly Dictionary<CoffeeType, string> CoffeeTypeDescription = new()
    {
        { CoffeeType.Brazil, "Brazil" },
        { CoffeeType.Kenya, "Kenya" },
        { CoffeeType.Columbia, "Columbia" }
    };

    public static string GetDescription(CoffeeType coffeeType)
    {
        return CoffeeTypeDescription.TryGetValue(coffeeType, out var description)
            ? description
            : coffeeType.ToString();
    }
}