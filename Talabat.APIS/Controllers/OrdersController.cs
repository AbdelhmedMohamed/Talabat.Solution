﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.Core.Order_Aggregrate;
using Talabat.Core.Services.Contract;

namespace Talabat.APIS.Controllers
{
    
    public class OrdersController : BaseAPIController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService , IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(Order),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status400BadRequest)]
        [HttpPost]

        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto )
        {
            var address = _mapper.Map<AddressDto,Address>(orderDto.shippingAddress);
            var order =  await _orderService.CreateOrderAsync(orderDto.BuyerEmail, orderDto.BasketId, orderDto.DelivaryMethodId,address);
            if (order is null) return BadRequest(new ApiResponse(400));
            return Ok(order);

        }



    }
}
