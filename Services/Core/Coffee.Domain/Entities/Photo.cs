using Coffee.Domain.Common;
using Coffee.Domain.Primitives;
using Coffee.Domain.Shared;

namespace Coffee.Domain.Entities;

public abstract class Photo(string patch, bool isMatch) : Entity
{
    
    protected const string Jpeg = ".jpeg";
    protected const string Jpg = ".jpg";
    protected const string Png = ".png";

    public string Patch { get; protected set; } = patch;

    public bool IsMatch { get; protected set; } = isMatch;

    public static ResultT<CoffeePhoto> CreateAndActive(string path, string contentType, long length, bool isMatch)
    {
        if (contentType != Jpg && contentType != Jpeg && contentType != Png)
            return Result.Failure<CoffeePhoto>(DomainErrors.CoffeePhoto.FileTypeInvalid(nameof(contentType)));

        if (length > Constraints.PhotoMaxLength)
            return Result.Failure<CoffeePhoto>(DomainErrors.CoffeePhoto.FileInvalidLength(nameof(length)));

        var coffeePhoto = new CoffeePhoto(path, isMatch);
        return Result.Success(coffeePhoto);
    }
}