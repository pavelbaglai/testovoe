using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebApplication39.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Data;


namespace WebApplication39.Controllers
{
    
    public class CategoryController : Controller
    {
        private KatalogsContext db;
        public CategoryController(KatalogsContext context)
        {
            db = context;
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()

        {
            return View(await db.Categories.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            db.Categories.Add(category);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}

