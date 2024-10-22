using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;

namespace Talabat.APIS.Controllers
{
    
    public class BasketController : BaseAPIController
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository, IMapper  mapper)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<customerBasket>> GetBasket (string id)
        {
            var basket = await _basketRepository.GetBasketAsync(id);
            return Ok(basket ?? new customerBasket(id)) ;

        }


        [HttpPost]

        public async Task<ActionResult<customerBasket>> UpdateBasket( CustomerBasketDto basket)
        {
            //Mapping
           var mappedBasket = _mapper.Map<CustomerBasketDto, customerBasket>(basket);

            var createdOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(mappedBasket);
            if (createdOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));
            return Ok(createdOrUpdatedBasket);

        }
 

        [HttpDelete]

        public async Task DeleteBasket (string id)
        {
            await _basketRepository.DeleteBasketAsync(id);  
        }




    }
}
 