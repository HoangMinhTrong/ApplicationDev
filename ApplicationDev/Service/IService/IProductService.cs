using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationDev.Models;

namespace ApplicationDev.Service.IService
{
    public interface IProductService
    {
        public Task<List<Product>> GetAll();
        public Task<Product> Create(Product product);
        public Task<List<Product>> ProductInStore(string? id);
        public Task<Product> Update(Product product, string id);
        public Task<Product> Delete(string id);
        public Task<Product> Detail(string id);
    }
}