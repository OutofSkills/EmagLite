using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.Services.Helpers
{
    public interface ITokenBuilder
    {
        Task<string> BuildTokenAsync(User user);
        bool ValidateToken(string token);
    }
}
