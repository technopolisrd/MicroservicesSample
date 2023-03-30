using Mango.MessageBus;
using Mango.Services.ShoppingCartAPI.Messages;
using Mango.Services.ShoppingCartAPI.Models.DTO;
using Mango.Services.ShoppingCartAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ShoppingCartAPI.Controllers;

[Route("api/cart")]
public class CartAPIController : ControllerBase
{
    private readonly ICartRepository _cartRepository;
    private readonly ICouponRepository _couponRepository;
    private readonly IMessageBus _messageBus;
    protected ResponseDTO _response;

    public CartAPIController(ICartRepository cartRepository, IMessageBus messageBus, ICouponRepository couponRepository)
    {
        _cartRepository = cartRepository;
        _couponRepository = couponRepository;
        _messageBus = messageBus;
        this._response = new ResponseDTO();
    }

    [HttpGet("GetCart/{userId}")]
    [Authorize]
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
    [Authorize]
    public async Task<object> AddCart([FromBody] CartDTO cartDTO)
    {
        try
        {
            CartDTO cartDt = await _cartRepository.CreateUpdateCart(cartDTO);
            _response.Result = cartDt;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }
        return _response;
    }

    [HttpPost("UpdateCart")]
    [Authorize]
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
    [Authorize]
    public async Task<object> RemoveCart(int cartId)
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

    [HttpPost("ApplyCoupon")]
    [Authorize]
    public async Task<object> ApplyCoupon([FromBody] CartDTO cartDTO)
    {
        try
        {
            bool IsSuccess = await _cartRepository.ApplyCoupon(cartDTO.CartHeader.UserId, cartDTO.CartHeader.CouponCode);
            _response.Result = IsSuccess;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }
        return _response;
    }

    [HttpPost("RemoveCoupon")]
    [Authorize]
    public async Task<object> RemoveCoupon(string userID)
    {
        try
        {
            bool IsSuccess = await _cartRepository.RemoveCoupon(userID);
            _response.Result = IsSuccess;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }
        return _response;
    }

    [HttpPost("Checkout")]
    public async Task<object> Checkout([FromBody] CheckoutHeaderDTO checkoutHeader)
    {
        try
        {
            CartDTO cartDTO = await _cartRepository.GetCartByUserId(checkoutHeader.UserId);
            if (cartDTO==null)
            {
                return BadRequest();
            }

            if (!string.IsNullOrEmpty(checkoutHeader.CouponCode))
            {
                 CouponDTO coupon = await _couponRepository.GetCoupon(checkoutHeader.CouponCode);
                if (checkoutHeader.DiscountTotal != coupon.DiscountAmount)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages = new List<string> { "Coupon Price has changed, please confirm" };
                    _response.DisplayMessage = "Coupon Price has changed, please confirm";
                    return _response;
                }
            }

            checkoutHeader.CartDetails = cartDTO.CartDetails;

            //logic to add message to process order
            //pending configure second parameter in app.setting file
            await _messageBus.PublishMessage(checkoutHeader, "roycheckoutmessagetopic");
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }
        return _response;
    }
}
