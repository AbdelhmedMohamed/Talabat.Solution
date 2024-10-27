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

namespace Talabat.Service
{
    public class OderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly IGenericRepository<Product> _productRepo;
        //private readonly IGenericRepository<DeliveryMethod> _deliveryRepo;
        //private readonly IGenericRepository<Order> _orderRepo;

        public OderService(IBasketRepository basketRepository , IUnitOfWork unitOfWork )
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;

            //_productRepo = productRepo;
            //_deliveryRepo = deliveryRepo;
            //_orderRepo = orderRepo;
        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int delivaryMethodId, Address shippingAddress)
        {
            //1

           var basket = await _basketRepository.GetBasketAsync(basketId);   

            //2

            var orderItems =  new List<OrderItem>();

            if(basket?.Items?.Count > 0)
            {
                foreach (var item in basket.Items)
                {
                    var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id);

                    var productItemOrdered = new ProductItemOrdered(item.Id, product.Name, product.PictureUrl);

                    var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);

                    orderItems.Add(orderItem);  



                }

            }

            //3

            var subTotal = orderItems.Sum(orderItem => orderItem.Price * orderItem.Quantity);

            //4

            var deliveryMethod =await _unitOfWork.Repository<DeliveryMethod>().GetAsync(delivaryMethodId);

            //5

            var order = new Order(buyerEmail,shippingAddress,deliveryMethod,orderItems,subTotal);

            await _unitOfWork.Repository<Order>().AddAsync(order);

            //6

            var result = await _unitOfWork.CompleteAsync();
            if(result <= 0) return null;

            return order;


        }

        public Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
