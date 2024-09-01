using System.Data;
using Coffee.Domain.Common;
using FluentValidation;

namespace Coffee.Application.Coffees.Handlers.UploadPhoto;

public class UploadCoffeePhotoValidator : AbstractValidator<UploadCoffeePhotoRequest>
{
    public UploadCoffeePhotoValidator()
    {
        var type = string.Empty;
        long length = 0;

        RuleFor(key => key.File).Must(key =>
        {
            type = key.ContentType;
            return CheckTypes(key.ContentType);
        });

        RuleFor(key => key.File).Must(key =>
        {
            length = key.Length;
            return CheckLength(key.Length);
        });
    }

    private static bool CheckTypes(string contentType)
    {
        string[] allowedContentType = { "images/jpg", "images/jpeg", "images/" };
        return allowedContentType.Contains(contentType);
    }

    private static bool CheckLength(long length)
    {
        return length < Constraints.PhotoMaxLength;
    }
}