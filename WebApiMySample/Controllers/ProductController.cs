using EFCoreCodeFirstSample.Models.DataManager;
using Microsoft.AspNetCore.Mvc;
using WebApiMySample.Models;
using WebApiMySample.Models.Repository;

namespace ASPNetWithAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IDataRepository<Product> _dataRepository;

        public ProductController(IDataRepository<Product> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Product> products = _dataRepository.GetAll();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Product product = _dataRepository.Get(id);
            if (product == null)
            {
                return NotFound("The Product record couldn't be found.");
            }
            return Ok(product);
        }

        [HttpPut]
        public IActionResult Put(int id, [FromBody] Product prd)
        {

            if (prd == null)
            {
                return BadRequest("Product is null.");
            }
            Product productToUpdate = _dataRepository.Get(id);
            if (productToUpdate == null)
            {
                return NotFound("The Product record couldn't be found.");
            }
            _dataRepository.Update(productToUpdate, prd);
            return NoContent();
        }

        [HttpPost]
        public IActionResult Post([FromBody] Product prd)
        {
            if (prd == null)
            {
                return BadRequest("Product is null.");
            }
            _dataRepository.Add(prd);
            return CreatedAtRoute(
                  "Get",
                  new { Id = prd.ProductId },
                  prd);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Product prd = _dataRepository.Get(id);
            if (prd == null)
            {
                return NotFound("The Product record couldn't be found.");
            }
            _dataRepository.Delete(prd);
            return NoContent();
        }
    }
}
