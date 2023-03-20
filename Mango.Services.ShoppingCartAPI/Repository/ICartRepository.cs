using Mango.Services.ShoppingCartAPI.Models.DTO;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ShoppingCartAPI.Repository;

public interface ICartRepository
{
    Task<CartDTO> GetCartByUserId(string userId);
    Task<CartDTO> CreateUpdateCart(CartDTO cartDTO);
    Task<bool> RemoveFromCart(int cartDetailsId);
    Task<bool> ClearCart(string userId);

}
