using System.Diagnostics;
using System.Linq;
using ApplicationDev.Data;
using Microsoft.AspNetCore.Mvc;
using ApplicationDev.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ApplicationDev.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    var obj = _context.Products.ToList();
        //    return View(obj);
        //}

        public async Task<IActionResult> Index(int id = 0, string searchString = "")
        {
            ViewData["CurrentFilter"] = searchString;
            var products = from p in _context.Products
                           select p;

            products = products.Where(s => s.Title.Contains(searchString) || s.Author.Contains(searchString));
            //
            int numOfFilteredStudent = products.Count();
            ViewBag.numberOfPages = (int)Math.Ceiling((double)numOfFilteredStudent / 24);
            ViewBag.CurrentPage = id;
            List<Product> studentsList = await products.Skip(id * 24)
                .Take(24).ToListAsync();

            return View(studentsList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}