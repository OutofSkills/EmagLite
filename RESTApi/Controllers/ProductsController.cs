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
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
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
        public async Task PostAsync([FromBody] Product product)
        {
            await productService.AddProductAsync(product);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task PutAsync(int id, [FromBody] Product product)
        {
            await productService.EditProductAsync(id, product);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            productService.RemoveProductAsync(id);
        }
    }
}
