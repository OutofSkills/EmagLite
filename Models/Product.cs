using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public byte[] Image { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public decimal DeliveryPrice { get; set; }
        [Required]
        public int GuaranteeDays { get; set; }
        public int RatingId { get; set; }
        public int CategoryId { get; set; }
        public int TypeId { get; set; }
        public int BrandId { get; set; }

        [ForeignKey(nameof(RatingId))]
        public ProductRating Rating{ get; set; }

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        [ForeignKey(nameof(TypeId))]
        public virtual ProductType Type { get; set; }

        [ForeignKey(nameof(BrandId))]
        public virtual ProductBrand Brand { get; set; }

        public Product()
        {
            Rating = new() { RatedTimes = 0, Value = 0 };
        }

    }
}
