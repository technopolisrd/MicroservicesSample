using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services;

public class CartService : BaseService, ICartService
{

    private readonly IHttpClientFactory _clientFactory;

    public CartService(IHttpClientFactory clientFactory) : base(clientFactory)
    {
        this._clientFactory = clientFactory;
    }

    public async Task<T> AddToCartAsync<T>(CartDTO cartDTO, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = cartDTO,
            Url = SD.ShoppingCartAPIBase + "/api/cart/AddCart",
            AccessToken = token
        });
    }

    public async Task<T> ApplyCoupon<T>(CartDTO cartDTO, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = cartDTO,
            Url = SD.ShoppingCartAPIBase + "/api/cart/ApplyCoupon",
            AccessToken = token
        });
    }

    public async Task<T> Checkout<T>(CartHeaderDTO cartHeader, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = cartHeader,
            Url = SD.ShoppingCartAPIBase + "/api/cart/Checkout",
            AccessToken = token
        });
    }

    public async Task<T> GetCartByUserIdAsync<T>(string userID, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.GET,
            Url = SD.ShoppingCartAPIBase + "/api/cart/GetCart/" + userID,
            AccessToken = token
        });
    }

    public async Task<T> RemoveCoupon<T>(string userID, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = userID,
            Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCoupon",
            AccessToken = token
        });
    }

    public async Task<T> RemoveToCartAsync<T>(int cartId, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = cartId,
            Url = SD.ShoppingCartAPIBase + "/api/cart/RemoveCart",
            AccessToken = token
        });
    }

    public async Task<T> UpdateToCartAsync<T>(CartDTO cartDTO, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = cartDTO,
            Url = SD.ShoppingCartAPIBase + "/api/cart/UpdateCart",
            AccessToken = token
        });
    }
}
