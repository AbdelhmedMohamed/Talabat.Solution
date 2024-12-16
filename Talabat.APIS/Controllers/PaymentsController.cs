using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Services.Contract;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Talabat.APIS.Controllers
{
    
    public class PaymentsController : BaseAPIController
    {
        private readonly IPaymentServise _paymentServise;
        private readonly IMapper _mapper;

        public PaymentsController(IPaymentServise paymentServise , IMapper mapper)
        {
            _paymentServise = paymentServise;
            _mapper = mapper;
        }

        //Create Or Update PaymentIntent
        
        [ProducesResponseType(typeof(CustomerBasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]

        public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var Basket = await _paymentServise.CreateOrUpdatePaymentIntent(basketId);
            if (Basket == null) return BadRequest(new ApiResponse(400));

            var mappedBasket = _mapper.Map<customerBasket, CustomerBasketDto>(Basket);
            return Ok(mappedBasket);

        }

    }
}
