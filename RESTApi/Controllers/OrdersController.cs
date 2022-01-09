using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using RESTApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            this.ordersService = ordersService;
        }

        // GET: api/<OrdersController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetAsync()
        {
            var orders = await ordersService.GetOrdersAsync();
            return Ok(orders);
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAsync(int id)
        {
            var orders = await ordersService.GetUserOrdersAsync(id);
            return Ok(orders);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public void PostAsync([FromBody] Order order)
        {
            ordersService.MakeOrder(order);
        }


        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await ordersService.RemoveOrder(id);
        }

    }
}
