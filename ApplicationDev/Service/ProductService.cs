﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationDev.Data;
using ApplicationDev.Models;
using ApplicationDev.Service.IService;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationDev.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public async Task<List<Product>> GetAll()
        {
            var obj = _context.Products.ToList();
            return obj;
        }

        public async Task<Product> Create(Product product)
        {
           
            if (product.Id == 0)
            {
                
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            return product;
        }

        public Task<List<Product>> ProductInStore(int? id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Product> Update(Product product, int id)
        {
            if (product.Id == id)
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            return product;
        }

        public async Task<Product> Delete(int id)
        {
            
            var obj = await _context.Products.FirstOrDefaultAsync();
            if (obj.Id == id)
            {
                _context.Remove(id);
                await _context.SaveChangesAsync();
            }
            return obj;
        }

        public Task<Product> Detail(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}