using Mango.Services.ShoppingCartAPI.Models.DTO;
using Mango.Services.ShoppingCartAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers;

[ApiController]
[Route("api/cart")]
public class CartController : Controller
{
    private readonly ICartRepository _cartRepository;
    protected ResponseDTO _response;

    public CartController(ICartRepository cartRepository, ResponseDTO _response)
    {
        _cartRepository = cartRepository;
        this._response = new ResponseDTO();
    }

    [HttpGet("GetCart/{userId}")]
    public async Task<object> GetCart(string userId)
    {
        try
        {
            CartDTO cartDto = await _cartRepository.GetCartByUserId(userId);
            _response.Result = cartDto;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }
        return _response;
    }

    [HttpPost("AddCart")]
    public async Task<object> AddCart(CartDTO cartDTO)
    {
        try
        {
            CartDTO cartDto = await _cartRepository.CreateUpdateCart(cartDTO);
            _response.Result = cartDto;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }
        return _response;
    }

    [HttpPost("UpdateCart")]
    public async Task<object> UpdateCart(CartDTO cartDTO)
    {
        try
        {
            CartDTO cartDto = await _cartRepository.CreateUpdateCart(cartDTO);
            _response.Result = cartDto;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }
        return _response;
    }

    [HttpPost("RemoveCart")]
    public async Task<object> RemoveCart([FromBody]int cartId)
    {
        try
        {
            bool IsSuccess = await _cartRepository.RemoveFromCart(cartId);
            _response.Result = IsSuccess;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }
        return _response;
    }
}
