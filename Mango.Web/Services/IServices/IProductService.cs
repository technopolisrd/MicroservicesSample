﻿using Mango.Web.Models;

namespace Mango.Web.Services.IServices;

public interface IProductService : IBaseService
{
    Task<T> GetAllProductsAsync<T>(string token);
    Task<T> GetProductsByIdAsync<T>(int id, string token);
    Task<T> CreateProductAsync<T>(ProductDTO productDTO, string token);
    Task<T> UpdateProductAsync<T>(ProductDTO productDTO, string token);
    Task<T> DeleteProductAsync<T>(int id, string token);
}
