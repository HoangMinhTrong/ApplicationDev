using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ApplicationDev.Service.IService
{
    public interface IUserService
    {
        public Task<List<IdentityRole>> GetAll();
        public Task<string> AddRole(string name);
    }
}