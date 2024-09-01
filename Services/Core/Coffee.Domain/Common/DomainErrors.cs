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

        public static readonly Func<string, Error> PhotoCountLimit = (property) =>
            new Error("photo.count.limit", $"{property} - photo count limit");
    }

    public static class CoffeePhoto
    {
        public static readonly Func<string, Error> FileTypeInvalid = (property) =>
            new Error("file.type.invalid", $"{property} is invalid");

        public static readonly Func<string, Error> FileInvalidLength = (property) =>
            new Error("file.invalid.length", $"{property} - length is invalid");

        public static readonly Func<string, Error> PhotoNotFound = (property) =>
            new Error("photo.not.found", $"{property} - NOT FOUND");

        public static readonly Func<string, Error> SaveFailure = (property) =>
            new Error("save.failure", $"{property}: save failure");
        
        public static readonly Func<string, Error> PhotosLoadingFailure = (property) =>
            new Error("save.failure", $"{property}: photo loading failure");
        
        public static readonly Func<string, Error> RemovePhotoFailure = (property) =>
            new Error("save.failure", $"{property}: remove photo failure");
        
        public static readonly Func<string, Error> RemovePhotosFailure = (property) =>
            new Error("save.failure", $"{property}: remove photos failure");
    }
}