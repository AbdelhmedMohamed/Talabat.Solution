using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Entities;
using Talabat.Core.Order_Aggregrate;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Product = Talabat.Core.Entities.Product;

namespace Talabat.Service
{
    public class PaymentServise : IPaymentServise
    {
        private readonly IConfiguration _configuration;
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentServise(IConfiguration configuration ,IBasketRepository basketRepository,IUnitOfWork unitOfWork )
        {
            _configuration = configuration;
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<customerBasket?> CreateOrUpdatePaymentIntent(string basketId)
        {
            //secret key
            StripeConfiguration.ApiKey = _configuration["StripeKeys:Secretkey"];
            //Get Basket
            var Basket = await _basketRepository.GetBasketAsync(basketId);
            if(Basket == null) return null;

            var shipingPrice = 0M; //Decimal
            if (Basket.DeliveryMethodId.HasValue)
            {
                var DeliveryMethod =await _unitOfWork.Repository<DeliveryMethod>().GetAsync(Basket.DeliveryMethodId.Value);
                shipingPrice = DeliveryMethod.Cost;
            }

            //total = subtotal + MD cost
            if(Basket.Items.Count > 0)
            {
                foreach (var item in Basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);
                    if(item.Price != product.Price)
                        item.Price = product.Price;
                }

            }

            var subTotal = Basket.Items.Sum(item => item.Price * item.Quantity );

            //Create payment intent 

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if(String.IsNullOrEmpty(Basket.PaymentIntentId)) //Creare
            {
                var Options = new PaymentIntentCreateOptions()
                {
                    Currency = "usd",
                    Amount = (long)subTotal * 100 + (long)shipingPrice * 100,
                    PaymentMethodTypes = new List<string>() { "card" }
                };
                paymentIntent = await service.CreateAsync(Options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else // Update
            {
                var Options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)subTotal * 100 + (long)shipingPrice * 100,
                };
                paymentIntent = await service.UpdateAsync(Basket.PaymentIntentId, Options);
                Basket.PaymentIntentId = paymentIntent.Id;
                Basket.ClientSecret = paymentIntent.ClientSecret;

            }
            await _basketRepository.UpdateBasketAsync(Basket);
             
              return Basket;


        }
    }
}
