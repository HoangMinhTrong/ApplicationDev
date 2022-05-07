using System.Linq;
using System.Security.Claims;
using ApplicationDev.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationDev.Controllers
{
    public class OderDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OderDetailsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        // GET
        public IActionResult Manager()
                {
                    var obj = _context.OrderItems.Include(x=>x.ApplicationUser).ToList();
                    return View(obj);
                }
                
                public IActionResult Details(int? id)
                        {
                            ViewBag.Products = _context.Products.ToList();
                            return View();
                        }
                
                        [HttpGet]
                        public IActionResult Details(int id)
                        {
                            ViewBag.Products = _context.Products.ToList();
                            var objId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                            var obj = _context.OrderDetails.Where(x=>x.OrderItem.UserId == objId).Where(x=>x.OrderId == id).ToList();
                            return View(obj);
                        }
    }
}