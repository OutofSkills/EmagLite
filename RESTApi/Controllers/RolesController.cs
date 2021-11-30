using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using RESTApi.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RESTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService rolesService;

        public RolesController(IRoleService rolesService)
        {
            this.rolesService = rolesService;
        }

        // GET: api/<RolesController>
        [HttpGet]
        public ActionResult<IEnumerable<Role>> GetAsync()
        {
            var roles = rolesService.GetRoles();
            return Ok(roles);
        }

        //// GET api/<RolesController>/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Product>> GetAsync(int id)
        //{
            
        //}

        // POST api/<RolesController>
        [HttpPost]
        public async Task PostAsync([FromBody] Role role)
        {
            await rolesService.CreateRoleAsync(role);
        }

        //// PUT api/<RolesController>/5
        //[HttpPut("{id}")]
        //public async Task PutAsync(int id, [FromBody] Product product)
        //{
            
        //}

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task DeleteAsync(int id)
        {
            await rolesService.RemoveRoleAsync(id);
        }
    }
}
