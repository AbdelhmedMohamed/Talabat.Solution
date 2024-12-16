using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.APIS.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications.ProductSpecification;

namespace Talabat.APIS.Controllers
{
   
    public class ProductsControllar : BaseAPIController
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _brandsRepo;
        private readonly IGenericRepository<ProductCategory> _categoryRepo;
        private readonly IMapper _mapper;

        public ProductsControllar(IGenericRepository<Product> productRepo ,
            IGenericRepository<ProductBrand> brandsRepo,
            IGenericRepository<ProductCategory> categoryRepo , 
            IMapper mapper )
        {
            _productRepo = productRepo;
            _brandsRepo = brandsRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [CachedAttribute(300)]
        [HttpGet]

        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {
            var spac = new ProductWithBrandAndCategorySpecification(specParams);



            var products = await _productRepo.GetAllWhithSpacAsync(spac);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);
            var countSpec = new ProductWithFilterationForCountSpec(specParams);
            var count = await _productRepo.GetCountAsync(countSpec);


            return Ok(new Pagination<ProductDto>(specParams.PageSize , specParams.PageIndex, count , data)); //200

        }

        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spac = new ProductWithBrandAndCategorySpecification(id);

            var product = await _productRepo.GetWhithSpacAsync(spac);

            if(product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(_mapper.Map<Product , ProductDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetBrand()
        {
            var brands = await _brandsRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetCategory()
        {
            var categories = await _categoryRepo.GetAllAsync();
            return Ok(categories);
        }



    }
}
