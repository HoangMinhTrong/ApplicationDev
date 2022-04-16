using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using ApplicationDev.Data;
using ApplicationDev.Models;
using ApplicationDev.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SQLitePCL;

namespace ApplicationDev.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService, ApplicationDbContext context)
        {
            _storeService = storeService;
            _context = context;
        } 
        //GET
        public async Task<IActionResult> Index()
        {
            var obj =await _storeService.GetAll();
            return View(obj);
        }
        
        public IActionResult CreateStore()
        {
            Store store = new Store
            {
                UserList = _context.Users.ToList().Select(x=> new SelectListItem
                {
                    Value = x.Id,
                    Text = x.UserName
                })
            };
            
            return View(store);
        }
        //Create
        [HttpPost]
        public async Task<IActionResult> CreateStore(Store store)
        {
            await _storeService.Create(store);
            return RedirectToAction(nameof(Index));
        }
    }
}