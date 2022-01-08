using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int StatusId { get;set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(StatusId))]
        public virtual OrderStatus Status { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
        public virtual ICollection<OrderProduct> Products { get; set; }

        public Order()
        {
            if(Products is null)
                Products = new List<OrderProduct>();
            Status = new OrderStatus();
        }
    }
}
