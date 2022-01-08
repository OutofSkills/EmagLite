using DataAccess.Repositories.Intefaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.DataAccess.Repositories.Intefaces
{
    public interface IOrdersRepository: IRepository<Order>
    {
        void MakeOrder(Order order);
    }
}
