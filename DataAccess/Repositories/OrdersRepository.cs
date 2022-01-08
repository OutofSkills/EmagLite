using DataAccess;
using DataAccess.Repositories;
using Models;
using RESTApi.DataAccess.Repositories.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.DataAccess.Repositories
{
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        public OrdersRepository(AppDbContext _context) : base(_context)
        {
        }

        public void MakeOrder(Order order)
        {
            var orderProducts = new List<OrderProduct>();
            foreach(var product in order.Products)
            {
                orderProducts.Add(product);
            }
            order.Products = null;

            _context.OrdersStatuses.Add(new() { Name = "Pending" });
            _context.SaveChanges();

            var status = _context.OrdersStatuses.Where(s => s.Name == "Pending").FirstOrDefault();
            order.Status= status;

            _context.Attach(order.User);
            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var product in orderProducts)
            {
                product.OrderId = order.Id;
                _context.Attach(order);
                _context.Attach(product.Product);
                _context.OrderProducts.Add(product);
                _context.SaveChanges();
            }
        }
    }
}
