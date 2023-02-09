using Salon.Infrastructure;
using Salon.Models.ViewModels;
using Salon.Models;
using Microsoft.AspNetCore.Mvc;
using Salon.Migrations;
using OrderModel = Salon.Models.OrderModel;
using Procedure = Salon.Models.Procedure;
using Microsoft.AspNetCore.Identity;

namespace Salon.Controllers
{
    public class CartController : Controller
    {
        private readonly DataContext _context;
        [BindProperty]

        public CartViewModel CartVM { get; set; }

        public CartController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

             CartVM = new()
            {
                Order = new OrderModel(),
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };

            return View(CartVM);
        }

        public async Task<IActionResult> Add(long id)
        {
            Procedure procedure = await _context.Procedures.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(c => c.ProcedureId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(procedure));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            TempData["Success"] = "Запис створено!";

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Decrease(long id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(c => c.ProcedureId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProcedureId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "Запис видалено!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(long id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(p => p.ProcedureId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "Запис видалено!";

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostOrder()
        {
            if (HttpContext.Session.GetJson<List<CartItem>>("Cart") != null)
            {
                List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

                CartVM.CartItems = cart;
               
                
                CartVM.Order.OrderDate = DateTime.Now;
                CartVM.Order.Status = "...";
                CartVM.Order.ProcedureCount = CartVM.CartItems.Count;
                _context.OrderModels.Add(CartVM.Order);
                _context.SaveChanges();

                foreach (var item in CartVM.CartItems)
                {
                    OrderDetails orderDetails = new OrderDetails
                    {
                       ProcedureId = item.ProcedureId,
                        OrderModelId = CartVM.Order.Id,
                        ProcedureName = item.ProcedureName,
                        Price = item.Price,
                        Quantity = item.Quantity,
                    };

                    _context.OrderDetails.Add(orderDetails);
                    _context.SaveChanges();
                }
            }

            HttpContext.Session.Remove("Cart");

            TempData["Success"] = "Запис створено!";

            return RedirectToAction("OrderConfirmation", new { id = CartVM.Order.Id });
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }
    }
}
