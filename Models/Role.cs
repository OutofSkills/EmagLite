using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Role: IdentityRole<string>
    {
        [Key]
        public override string Id { get; set; }
        [Required]
        public override string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}
