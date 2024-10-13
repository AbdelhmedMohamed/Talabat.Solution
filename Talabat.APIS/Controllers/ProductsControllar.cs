using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications.ProductSpecification;

namespace Talabat.APIS.Controllers
{
   
    public class ProductsControllar : BaseAPIController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IMapper _mapper;

        public ProductsControllar(IGenericRepository<Product> productRepo , IMapper mapper )
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var spac = new ProductWithBrandAndCategorySpecification();

            var products = await _productRepo.GetAllWhithSpacAsync(spac);

            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products)); //200
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spac = new ProductWithBrandAndCategorySpecification(id);

            var product = await _productRepo.GetWhithSpacAsync(spac);

            if(product == null)
            {
                return NotFound(new {Massage ="Not Found" , StatusCode = 404 }); //404
            }
            return Ok(_mapper.Map<Product , ProductDto >(product));
        }


    }
}
