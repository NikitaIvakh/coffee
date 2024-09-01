using Microsoft.AspNetCore.Http;

namespace Coffee.Application.Coffees.Handlers.UploadPhoto;

public record UploadCoffeePhotoRequest(Guid Id, IFormFile File, bool IsMain);