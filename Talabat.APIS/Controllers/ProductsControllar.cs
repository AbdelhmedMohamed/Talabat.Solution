using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.APIS.Controllers
{
   
    public class ProductsControllar : BaseAPIController
    {
        private readonly IGenericRepository<Product> _productRepo;

        public ProductsControllar(IGenericRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }


        [HttpGet]

        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepo.GetAllAsync();

            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product =await _productRepo.GetAsync(id);
            if(product == null)
            {
                return NotFound(new {Massage ="Not Found" , StatusCode = 404 }); //404
            }
            return Ok(product);
        }


    }
}
