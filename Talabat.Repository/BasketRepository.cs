using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Repository
{
    public class BasketRepository :IBasketRepository
    {
        private readonly StackExchange.Redis.IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketid)
        {
            return await _database.KeyDeleteAsync(basketid);    
        }

        public async Task<customerBasket?> GetBasketAsync(string basketId)
        {
           var basket = await _database.StringGetAsync(basketId);
            return basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<customerBasket>(basket);

        }

        public async Task<customerBasket?> UpdateBasketAsync(customerBasket basket) //Add or update
        {
          var createdOrUpdatedBasket =await _database.StringSetAsync(basket.Id , JsonSerializer.Serialize(basket) , TimeSpan.FromDays(60) );

            if (createdOrUpdatedBasket is false ) return null;
            return await GetBasketAsync(basket.Id); 
            
        }
    }
}
