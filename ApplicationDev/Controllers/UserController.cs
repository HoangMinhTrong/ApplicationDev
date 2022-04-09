using System.Threading.Tasks;
using ApplicationDev.Service;
using ApplicationDev.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationDev.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET
        public async Task<IActionResult> Index()
        {
            var obj = await _userService.GetAll();
            return View(obj);
        }
        // CREATE
        [HttpPost]
        public async Task<IActionResult> AddRole(string roleName)
        {
            await _userService.AddRole(roleName);
            return RedirectToAction(nameof(Index));
        }
    }
}