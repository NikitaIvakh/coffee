using Coffee.Domain.Shared;

namespace Coffee.Domain.Common;

public static class DomainErrors
{
    public static class CoffeeEntity
    {
        public static readonly Func<string, int, Error> InvalidLength = (property, length) => 
            new Error("invalid.length", $"The property {property} has an invalid length of {length}");
        
        public static readonly Func<string, Error> InvalidValue = (property) => 
            new Error("value.is.invalid", $"{property} is invalid");

        public static readonly Func<string, Error> CoffeeCanNotCreate = (property) =>
            new Error("created.error", $"{property}");

        public static readonly Func<string, Error> CoffeeCanNotDeleted = (property) =>
            new Error("deleted.error", $"{property} can not deleted");

        public static readonly Func<Guid, Error> CoffeeNotFound = (property) =>
            new Error("coffee.not.found", $"Not found: {property}");

        public static readonly Func<string, Error> CoffeeCanNotUpdate = (property) =>
            new Error("update.error", $"{property}");
    }
}