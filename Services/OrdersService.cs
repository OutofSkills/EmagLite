using DataAccess.UnitOfWork;
using Models;
using RESTApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrdersService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Order> GetOrderAsync(int orderId)
        {
            return await unitOfWork.OrdersRepository.GetById(orderId);
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await unitOfWork.OrdersRepository.GetAll();
        }

        public void MakeOrder(Order order)
        {
            unitOfWork.OrdersRepository.MakeOrder(order);
            unitOfWork.SaveChanges();
        }

        public async Task RemoveOrder(int orderId)
        {
            var order = await unitOfWork.OrdersRepository.GetById(orderId);
            if(order != null)
                await unitOfWork.OrdersRepository.Delete(order);
        }
    }
}
