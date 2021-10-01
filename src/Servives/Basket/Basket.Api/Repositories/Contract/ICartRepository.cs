using Cart.Api.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.Api.Repositories.Contract
{
    public interface ICartRepository
    {
        Task<ShoppingCart> GetCart(string userName);
        Task<ShoppingCart> UpdateCart(ShoppingCart basket);
        Task DeleteCart(string userName);
    }
}
