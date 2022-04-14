using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationDev.Data;
using ApplicationDev.Models;
using ApplicationDev.Service.IService;
using Microsoft.EntityFrameworkCore;

namespace ApplicationDev.Service
{
    public class StoreService : IStoreService
    {
        private readonly ApplicationDbContext _context;

        public StoreService(ApplicationDbContext context)
        {
            _context = context;
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
            var obj = await _context.Stores.ToListAsync();
            return obj;
        }
    }
}