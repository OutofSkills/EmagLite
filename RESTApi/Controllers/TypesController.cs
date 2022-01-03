using Microsoft.AspNetCore.Mvc;
using Models;
using RESTApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly ITypesService typesService;

        public TypesController(ITypesService typesService)
        {
            this.typesService = typesService;
        }

        // GET: api/<TypesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetAsync()
        {
            var types = await typesService.GetTypesAsync();
            return Ok(types);
        }

        // GET api/<TypesController>/Type
        [HttpGet("{name}")]
        public ActionResult<ProductType> GetAsync(string name)
        {
            var type = typesService.FindTypeAsync(name);

            return Ok(type);
        }

        // POST api/<TypesController>
        [HttpPost]
        public void PostAsync([FromBody] ProductType type)
        {
            typesService.AddType(type);
        }

        // PUT api/<TypesController>/5
        [HttpPut("{id}")]
        public void PutAsync(int id, [FromBody] ProductType type)
        {
            typesService.EditType(type);
        }

        // DELETE api/<TypesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await typesService.RemoveTypeAsync(id);
        }
    }
}
