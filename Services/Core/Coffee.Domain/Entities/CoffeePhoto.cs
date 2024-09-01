using Coffee.Domain.Common;
using Coffee.Domain.Shared;

namespace Coffee.Domain.Entities;

public class CoffeePhoto(string patch, bool isMatch) : Photo(patch, isMatch)
{
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