using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationDev.Models;

namespace ApplicationDev.Service.IService
{
    public interface IStoreService
    {
        public Task<Store> Create(Store store);
        public Task<List<Store>> GetAll();
    }
}