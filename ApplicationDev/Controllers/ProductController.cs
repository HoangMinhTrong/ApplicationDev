using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationDev.Data;
using ApplicationDev.Models;
using ApplicationDev.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ApplicationDev.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IProductService _productService;
        private readonly ApplicationDbContext _context;

        public ProductController(IProductService productService, IWebHostEnvironment hostEnvironment, ApplicationDbContext context)
        {
            _productService = productService;
            _hostEnvironment = hostEnvironment;
            _context = context;
        }
        // GET
        public async Task<IActionResult> Index()
        {
            var obj = await _productService.GetAll();
            return View(obj);
        }

        public IActionResult Create()
        {
            Product product = new Product()
            {
                ProductCategoryList = _context.ProductCategories.ToList().Select(x=> new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }),
                StoreList = _context.Stores.ToList().Select(x=> new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.Name
                }),
            };
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            //Save Image To wwwRoot
            var wwwRootPath = _hostEnvironment.WebRootPath;
            var filename = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
            var extension = Path.GetExtension(product.ImageFile.FileName);
            product.ImageUrl = filename = filename + DateTime.Now.ToString("yymmssff") + extension;
            var path = Path.Combine(wwwRootPath + "/Image/", filename);
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await product.ImageFile.CopyToAsync(fileStream);
            }
            
            var obj = _context.Products;
            await _productService.Create(product);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ProductInStore(int? id)
        {
            var productId = id.Value;
            var product = await _context.Stores.FindAsync(productId);
            ViewBag.ProductId = product.Id;
            ViewBag.ProductName = product.Name;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ProductInStore(int id)
        {
            var obj = _context.Products
                .Include(x => x.Store)
                .Where(x => x.StoreId == id);
            return View(obj);
        }

        
            // }
            // [HttpPost]
            // public async Task<IActionResult> CreateProductInStore(ProductInStore productInStore)
            // {
            //     if (productInStore.Id == 0)
            //     {
            //             await _context.ProductInStores.AddAsync(productInStore);
            //             await _context.SaveChangesAsync();
            //             RedirectToAction(nameof(StoreController.Index));
            //     }
            //     return View(productInStore);
            // }
    

    }
}