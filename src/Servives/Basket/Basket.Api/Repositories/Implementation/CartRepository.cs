using Cart.Api.Entites;
using Cart.Api.Repositories.Contract;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cart.Api.Repositories.Implementation
{
    public class CartRepository : ICartRepository
    {
        private readonly IDistributedCache _redisCache;

        public CartRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<ShoppingCart> GetCart(string userName)
        {
            var cart = await _redisCache.GetStringAsync(userName);

            if (String.IsNullOrEmpty(cart))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(cart);
        }

        public async Task<ShoppingCart> UpdateCart(ShoppingCart cart)
        {
            await _redisCache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));

            return await GetCart(cart.UserName);
        }

        public async Task DeleteCart(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}
