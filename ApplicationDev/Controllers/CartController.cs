using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationDev.Data;
using ApplicationDev.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ApplicationDev.Controllers
{
    public class CartController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, INotyfService notyf)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _notyf = notyf;
        }
        // GET
        public IActionResult Index() {
            return View (GetCartItems());
        }
        // Save json key of cart
        public const string CARTKEY = "cart";

        // Get Cart Session 
        public List<CartItem> GetCartItems () {

            var session = HttpContext.Session;
            string jsonCart = session.GetString (CARTKEY);
            if (jsonCart != null) {
                return JsonConvert.DeserializeObject<List<CartItem>> (jsonCart);
            }
            return new List<CartItem> ();
        }
        // Delete cart session
        void ClearCart () {
            var session = HttpContext.Session;
            session.Remove (CARTKEY);
        }

        // Save list cart to session
        void SaveCartSession (List<CartItem> ls) {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject (ls);
            session.SetString (CARTKEY, jsoncart);
        }

        public IActionResult AddToCart (int productId) {

            var product = _context.Products
                .FirstOrDefault (p => p.Id == productId);
            if (product == null)
                return NotFound ("Not Found Product");

            
            var cart = GetCartItems ();
            var cartItem = cart.Find (p => p.Product.Id == productId);
            if (cartItem != null) 
            {
                cartItem.Quantity++;
            } else 
            {
                cart.Add (new CartItem () { Quantity = 1, Product = product });
            }
            SaveCartSession (cart);
            return RedirectToAction(nameof(HomeController.Index));
        }
        [HttpPost]
        public IActionResult UpdateCart (int id, int quantity) {
            var cart = GetCartItems ();
            if (cart != null)
            {
                if (quantity > 0)
                {
                    for (int i = 0; i < cart.Count; i++)
                    {
                        if (cart[i].Product.Id == id)
                        {
                            cart[i].Quantity = quantity;
                        }
                    }
                    
                    HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(cart));
                }
                SaveCartSession(cart);
                return Ok(quantity);
            }
            return BadRequest();

        }
        
        public IActionResult RemoveCart ( int productId) {
            var cart = GetCartItems ();
            var cartItem = cart.Find (p => p.Product.Id == productId);
            if (cartItem != null) {
                cart.Remove(cartItem);
            }

            SaveCartSession (cart);
            return RedirectToAction (nameof (Index));
        }

        public IActionResult Checkout()
        {
            ViewBag.User = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.Cart = GetCartItems();
            return View();
        }
        [HttpPost]
        public IActionResult Checkout(OrderItem orderItem)
        {
            var cart = GetCartItems ();
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userId != null)
            {
                OrderItem oder = new OrderItem();
                orderItem.OrderDate = oder.OrderDate = DateTime.Now;
                orderItem.UserId = oder.UserId = userId;
                orderItem.Paid = oder.Paid;
                _context.Add(oder);
                _context.SaveChanges();

                foreach (var item in cart)
                {
                    OrderDetail orderDetail = new OrderDetail();
                    orderDetail.OrderId = oder.Id;
                    orderDetail.ProductId = item.Product.Id;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.Total = item.Quantity * item.Product.Price;
                    orderDetail.CreateAt = DateTime.Now;
                    _context.Add(orderDetail);
                }
                _context.SaveChanges();
                ClearCart();
                _notyf.Success("Success Order");
                return RedirectToAction("Index", "Home");
            }
            return View(orderItem);
        }
    }
}