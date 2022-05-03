using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationDev.Data;
using ApplicationDev.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ApplicationDev.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
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
    }
}