using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _dbcontext;

        public BuggyController(StoreContext dbcontext)
        {
           _dbcontext = dbcontext;
        }

        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var Product = _dbcontext.Products.Find(100);
            if (Product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(Product);
        }

        [HttpGet("servererror")]
        public ActionResult GetServerErrore()
        {
            var Product = _dbcontext.Products.Find(100);
            var ProductDto = Product.ToString();

            return Ok(ProductDto);
        }

        [HttpGet("badrequest")]

        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("unauthorized")]
        public ActionResult GetUnAuthorized()
        {
            return Unauthorized(new ApiResponse(401));
        }


        [HttpGet("badrequest/{id}")]

        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }



    }
}
