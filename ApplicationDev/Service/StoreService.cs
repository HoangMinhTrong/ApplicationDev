using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationDev.Data;
using ApplicationDev.Models;
using ApplicationDev.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ApplicationDev.Service
{
    public class StoreService : IStoreService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public StoreService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Store> Create(Store store)
        {
            if (store.Id == 0)
            {
                await _context.Stores.AddAsync(store);
                await _context.SaveChangesAsync();
            }
            return store;
        }

        public async Task<List<Store>> GetAll()
        {
            var objId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var stores = _context.Stores.Where(x => x.UserId == objId).ToList();
            return stores;
           
        }
    }
}