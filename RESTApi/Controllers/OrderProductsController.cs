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
    public class OrderProductsController : ControllerBase
    {
        private readonly IOrdersService ordersService;

        public OrderProductsController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        // GET: api/<OrdersProductsController>
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAsync(int id)
        {
            var orders = await ordersService.GetOrderProductsAsync(id);
            return Ok(orders);
        }
    }
}
