using Microsoft.AspNetCore.Mvc;
using Models;
using RESTApi.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            this.productCategoryService = productCategoryService;
        }

        // GET: api/<ProductCategoryController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetAsync()
        {
            var categories = await productCategoryService.GetCategoriesAsync();

            return Ok(categories);
        }

        // GET api/<ProductCategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            var category = await productCategoryService.FindCategoryAsync(id);
            return Ok(category);
        }

        // POST api/<ProductCategoryController>
        [HttpPost]
        public void Post([FromBody] Category category)
        {
            productCategoryService.AddCategory(category);
        }

        // PUT api/<ProductCategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Category category)
        {
            productCategoryService.EditCategory(category);
        }

        // DELETE api/<ProductCategoryController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await productCategoryService.RemoveCategoryAsync(id);
        }
    }
}
