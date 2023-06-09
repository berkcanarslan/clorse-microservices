﻿using Clor.Services.Catalog.Dtos;
using Clor.Shared.Dtos;

namespace Clor.Services.Catalog.Services;

public interface ICategoryService
{
    Task<Response<List<CategoryDto>>> GetAllAsync();
    Task<Response<CategoryDto>> CreateAsync(CategoryDto category);
    Task<Response<CategoryDto>> GetByIdAsync(string id);
}