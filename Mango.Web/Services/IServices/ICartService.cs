﻿using Mango.Web.Models;

namespace Mango.Web.Services.IServices;

public interface ICartService
{
    Task<T> GetCartByUserIdAsync<T>(string userID, string token = null);
    Task<T> AddToCartAsync<T>(CartDTO cartDTO, string token = null);
    Task<T> UpdateToCartAsync<T>(CartDTO cartDTO, string token = null);
    Task<T> RemoveToCartAsync<T>(int cartId, string token = null);
    Task<T> ApplyCoupon<T>(CartDTO cartDTO, string token = null);
    Task<T> RemoveCoupon<T>(string userID, string token = null);
    Task<T> Checkout<T>(CartHeaderDTO cartHeader, string token = null);
}
