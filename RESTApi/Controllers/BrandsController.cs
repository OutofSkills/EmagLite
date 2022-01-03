using Microsoft.AspNetCore.Mvc;
using Models;
using RESTApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandsService brandsService;

        public BrandsController(IBrandsService brandsService)
        {
            this.brandsService = brandsService;
        }

        // GET: api/<TypesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetAsync()
        {
            var brands = await brandsService.GetBrandsAsync();
            return Ok(brands);
        }

        // GET api/<TypesController>/Brand
        [HttpGet("{name}")]
        public ActionResult<ProductBrand> GetAsync(string name)
        {
            var brand = brandsService.FindBrandAsync(name);

            return Ok(brand);
        }

        // POST api/<TypesController>
        [HttpPost]
        public void PostAsync([FromBody] ProductBrand brand)
        {
            brandsService.AddBrand(brand);
        }

        // PUT api/<TypesController>/5
        [HttpPut("{id}")]
        public void PutAsync(int id, [FromBody] ProductBrand brand)
        {
            brandsService.EditBrand(brand);
        }

        // DELETE api/<TypesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await brandsService.RemoveBrandAsync(id);
        }
    }
}
