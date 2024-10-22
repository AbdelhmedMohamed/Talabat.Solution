using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Repositories.Contract
{
    public interface IBasketRepository
    {
        Task<customerBasket?> GetBasketAsync(string basketId);
        Task<customerBasket?> UpdateBasketAsync(customerBasket basket); //Add & Update

        Task<bool> DeleteBasketAsync(string basketid);



    }
}
