﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationDev.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationDev.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        // GET
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrderManager(int? id)
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> OrderManager(int id)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var obj = _context.OrderDetails.
                Include(x=>x.OrderItem)
                .ThenInclude(x=>x.ApplicationUser).ThenInclude(x=>x.Store).ThenInclude(x=>x.Products)
                .Where(x => x.StoreId == id).ToList();
            return View(obj);
        }

        public IActionResult OrderDetails(int? id)
        {
            ViewBag.Products = _context.Products.ToList();
            return View();
        }

        [HttpGet]
        public IActionResult OrderDetails(int id)
        {
            ViewBag.Oders = _context.OrderItems.ToList();
            var objId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.Products = _context.Products.Where(x=>x.Store.UserId == objId).ToList();
            var obj = _context.OrderDetails.Where(x=>x.Store.UserId == objId).Where(x=>x.OrderId == id).ToList();
            return View(obj);
        }
    }
}