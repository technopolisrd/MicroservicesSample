using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.OrderAPI.Messages;

public class CartDetailDTO
{
    public int CartDetailId { get; set; }

    public int CartHeaderId { get; set; }

    public int ProductId { get; set; }
    public virtual ProductDTO Product { get; set; }

    public int Count { get; set; }

}
