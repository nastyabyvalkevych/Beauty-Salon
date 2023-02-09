using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Salon.Infrastructure;
using Salon.Models;

namespace Salon.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProceduresController : Controller
    {
        private readonly DataContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProceduresController(DataContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(int p = 1)
        {
            int pageSize = 6;
            ViewBag.PageNumber = p;
            ViewBag.PageRange = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling((decimal)_context.Procedures.Count() / pageSize);

            return View(await _context.Procedures.OrderByDescending(p => p.Id)
                                                                            .Include(p => p.Category)
                                                                            .Skip((p - 1) * pageSize)
                                                                            .Take(pageSize)
                                                                            .ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Procedure procedure)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", procedure.CategoryId);

            if (ModelState.IsValid)
            {
                procedure.Slug = procedure.Name.ToLower().Replace(" ", "-");

                var slug = await _context.Procedures.FirstOrDefaultAsync(p => p.Slug == procedure.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Процедура вже існує");
                    return View(procedure);
                }

                if (procedure.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/procedures");
                    string imageName = Guid.NewGuid().ToString() + "_" + procedure.ImageUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await procedure.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                    procedure.Image = imageName;
                }

                _context.Add(procedure);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Процедура успішно створена!";

                return RedirectToAction("Index");
            }

            return View(procedure);
        }


        public async Task<IActionResult> Edit(long id)
        {
            Procedure procedure = await _context.Procedures.FindAsync(id);

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", procedure.CategoryId);

            return View(procedure);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Procedure procedure)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", procedure.CategoryId);

            if (ModelState.IsValid)
            {
                procedure.Slug = procedure.Name.ToLower().Replace(" ", "-");

                var slug = await _context.Procedures.FirstOrDefaultAsync(p => p.Slug == procedure.Slug);
                if (slug != null)
                {
                    ModelState.AddModelError("", "Процедура вже існує.");
                    return View(procedure);
                }

                if (procedure.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/procedures");
                    string imageName = Guid.NewGuid().ToString() + "_" + procedure.ImageUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fs = new FileStream(filePath, FileMode.Create);
                    await procedure.ImageUpload.CopyToAsync(fs);
                    fs.Close();

                    procedure.Image = imageName;
                }

                _context.Update(procedure);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Процедура успішно змінена!";
            }

            return View(procedure);
        }



        public async Task<IActionResult> Delete(long id)
        {
            Procedure procedure = await _context.Procedures.FindAsync(id);

            if (!string.Equals(procedure.Image, "noimage.png"))
            {
                string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "media/procedures");
                string oldImagePath = Path.Combine(uploadsDir, procedure.Image);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _context.Procedures.Remove(procedure);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Процедура успішно видалена!";

            return RedirectToAction("Index");
        }
    }
}
