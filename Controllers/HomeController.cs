using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication39.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApplication39.Controllers
{
    public class HomeController : Controller
    {
        KatalogsContext db;
        public HomeController(KatalogsContext context)
        {
            db = context;
            // добавляем начальные данные
            if (db.Categories.Count() == 0)
            {
                Category Еда = new Category { Name = "Еда" };
                Category Вкусности = new Category { Name = "Вкусности" };
                 Category Вода = new Category { Name = "Вода" };


                Katalog user1 = new Katalog { Name = "Селедка", Category = Еда, Opisanie = "Селедка соленая", Shena = 10000, PrimO = "Акция", PrimS = "Пересоленая" };


                db.Categories.AddRange(Еда, Вкусности, Вода);
                db.Katalogs.AddRange(user1);
                db.SaveChanges();
            }
        }
        public IActionResult Start()
        {
            return View();
        }
        [Authorize(Roles = "moderator  , user")]
        public ActionResult Index(int? сategory, string name)
        {
            IQueryable<Katalog> katalogs = db.Katalogs.Include(p => p.Category);
            if (сategory != null && сategory != 0)
            {
                katalogs = katalogs.Where(p => p.CategoryId == сategory);
            }
            if (!String.IsNullOrEmpty(name))
            {
                katalogs = katalogs.Where(p => p.Name.Contains(name));
            }

            List<Category> categories = db.Categories.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            categories.Insert(0, new Category { Name = "Все", Id = 0 });

            KatalogListViewModel viewModel = new KatalogListViewModel
            {
                Katalogs = katalogs.ToList(),
                Categories = new SelectList(categories, "Id", "Name"),
                Name = name
            };

            return View(viewModel);
        }
    
        [Authorize(Roles = "moderator")]
        public IActionResult Create()
        {
            //List<Category> categories = db.Categories.ToList();
            // ViewBag.TourCate = new SelectList(categories, "Id", "Name");
            // SelectList category = new SelectList(db.Categories, "Id", "Name");
            // ViewBag.Category = category;
           ViewData["Category"] = new SelectList(db.Categories, "Id", "Name");

            return View();
            
        }
     
        [Authorize(Roles = "moderator")]
        [HttpPost]
        public async Task<IActionResult> Create(Katalog katalog)
        {
            db.Katalogs.Add(katalog);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "moderator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Katalog katalog = await db.Katalogs.FirstOrDefaultAsync(p => p.Id == id);
                if (katalog != null)
                    return View(katalog);
                ViewData["Category"] = new SelectList(db.Categories, "Id", "Name", katalog.CategoryId);
            }
            return NotFound();
        }

        [Authorize(Roles = "moderator")]
        [HttpPost]
        public async Task<IActionResult> Edit(Katalog katalog)
        {
            db.Katalogs.Update(katalog);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "moderator")]
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Katalog user = await db.Katalogs.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                    return View(user);
            }
            return NotFound();
        }
        [Authorize(Roles = "moderator")]
        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Katalog user = await db.Katalogs.FirstOrDefaultAsync(p => p.Id == id);
                if (user != null)
                {
                    db.Katalogs.Remove(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
