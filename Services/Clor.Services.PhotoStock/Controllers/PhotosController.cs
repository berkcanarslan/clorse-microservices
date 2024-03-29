﻿using Clor.Services.PhotoStock.Dtos;
using Clor.Shared.ControllerBases;
using Microsoft.AspNetCore.Mvc;
using Clor.Shared.Dtos;

namespace Clor.Services.PhotoStock.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PhotosController : CustomBaseController
{
    [HttpPost]
    public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
    {
        if (photo is { Length: > 0 })
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
            await using var stream = new FileStream(path, FileMode.Create);
            await photo.CopyToAsync(stream, cancellationToken);

            var returnPath = "photos/" + photo.FileName;

            PhotoDto photoDto = new() { Url = returnPath };

            return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
        }

        return CreateActionResultInstance(Response<PhotoDto>.Fail("Photo is empty", 400));
    }

    [HttpDelete("{photoUrl}")]
    public IActionResult PhotoDelete(string photoUrl, CancellationToken cancellationToken)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
        if (!System.IO.File.Exists(path))
            return CreateActionResultInstance(Response<NoContent>.Fail("Photo not found", 404));
        System.IO.File.Delete(path);
        return CreateActionResultInstance(Response<NoContent>.Success(204));

    }

}