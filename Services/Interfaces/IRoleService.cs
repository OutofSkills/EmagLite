﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.Services.Interfaces
{
    public interface IRoleService
    {
        Task CreateRoleAsync(Role role);
        Task RemoveRoleAsync(int id);
        IEnumerable<Role> GetRoles();
    }
}
