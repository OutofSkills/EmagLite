using Microsoft.AspNetCore.Identity;
using Models;
using RESTApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTApi.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> roleManager;

        public RoleService(RoleManager<Role> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task CreateRoleAsync(Role role)
        {
            var result = await roleManager.CreateAsync(role);
            if (!result.Succeeded)
                throw new Exception(result.Errors.FirstOrDefault().ToString());
        }

        public async Task RemoveRoleAsync(int roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId.ToString());
            if (role is null)
                throw new Exception("No role with the given Id.");

            var result = await roleManager.DeleteAsync(role);
            if (!result.Succeeded)
                throw new Exception(result.Errors.FirstOrDefault().ToString());
        }

        public IEnumerable<Role> GetRoles()
        {
            return roleManager.Roles.ToList();
        }
    }
}
