using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Salon.Infrastructure;
using Salon.Models.ViewModels;

namespace Salon.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        private readonly DataContext _context;
        public OrdersController(DataContext context)
        {
            _context = context;
        }
        public void ChangeOrderStatus(int OrderModelId, string status)
        {
            var order = _context.OrderModels.FirstOrDefault(o => o.Id == OrderModelId);
            order.Status = status;
            _context.SaveChanges();
        }
        public IActionResult Index(string? status)
        {
            if (status == "approved")
            {
                return View(_context.OrderModels.Where(o => o.Status == "Прийнято").ToList());
            }
            else if (status == "pending")
            {
                return View(_context.OrderModels.Where(o => o.Status == "...").ToList());
            }
            return View(_context.OrderModels.ToList());
        }

        //[HttpGet]
        //public async Task<IActionResult> Index(DateTime Empsearch)
        //{
        //    ViewData["GetEmployeedetails"] = Empsearch;
        //    var empquery = from x in _context.OrderModels select x;

        //    if (Empsearch!=0)
        //    {
        //        empquery = empquery.Where(x => x.OrderDate.Contains(Empsearch));
        //    }
        //    return View(await empquery.AsNoTracking().ToListAsync());
        //}

  

        public IActionResult Details(int id)
        {
            OrderViewModel orderVM = new OrderViewModel()
            {
                OrderModel = _context.OrderModels.FirstOrDefault(o => o.Id == id),
                OrderDetails = _context.OrderDetails.Where(o => o.OrderModelId == id)
            };

            return View(orderVM);
        }

        public IActionResult Approve(int id)
        {
            var orderFromDb = _context.OrderModels.FirstOrDefault(o => o.Id == id);
            if (orderFromDb == null)
            {
                return NotFound();
            }
            ChangeOrderStatus(id, "Прийнято");
            return RedirectToAction("Index");
        }

        public IActionResult Reject(int id)
        {
            var orderFromDb = _context.OrderModels.FirstOrDefault(o => o.Id == id);
            if (orderFromDb == null)
            {
                return NotFound();
            }
            ChangeOrderStatus(id, "Відхилено");
            return RedirectToAction("Index");
        }

    }
}
