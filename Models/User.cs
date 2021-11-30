using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User: IdentityUser<int>
    {
        [Key]
        public override int Id { get; set; }
        public override string UserName { get; set; }
        [Required]
        public override string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Avatar { get; set; }

        public virtual Role Role { get; set; }
    }
}
