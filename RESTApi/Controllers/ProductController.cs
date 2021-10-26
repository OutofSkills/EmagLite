using Microsoft.AspNetCore.Mvc;
using Models;
using RESTApi.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAsync()
        {
            var products = await productService.GetProductsAsync();
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetAsync(int id)
        {
            var product = await productService.FindProductById(id);

            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] Product product)
        {
            productService.AddProduct(product);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product product)
        {
            productService.EditProduct(product);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            productService.RemoveProductAsync(id);
        }
    }
}
