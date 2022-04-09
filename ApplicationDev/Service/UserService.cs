using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationDev.Data;
using ApplicationDev.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ApplicationDev.Service
{
    public class UserService : IUserService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<List<IdentityRole>> GetAll()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return roles;
        }

        public async Task<string> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            return roleName;
        }
    }
}