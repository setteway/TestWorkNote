using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteApp.Models;

namespace NoteApp.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await db.Notes.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Note note)
        {
            db.Notes.Add(note);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                Note note = await db.Notes.FirstOrDefaultAsync(p => p.Id == id);
                if (note != null)
                    return View(note);
            }
            return NotFound();
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Note note = await db.Notes.FirstOrDefaultAsync(p => p.Id == id);
                if (note != null)
                    return View(note);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Note note)
        {
            db.Notes.Update(note);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Note note = await db.Notes.FirstOrDefaultAsync(p => p.Id == id);
                if (note != null)
                    return View(note);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Note note = new Note { Id = id.Value };
                db.Entry(note).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }




        public ActionResult RegisterViewModel(string name)
        {
            IQueryable<Note> users = db.Notes;
            if (!String.IsNullOrEmpty(name))
            {
                users = users.Where(p => p.Name.Contains(name));
            }

            List<Note> companies = db.Notes.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            companies.Insert(0, new Note { Name = "Все", Id = 0 });

            Search viewModel = new Search
            {
                Notes = users.ToList(),
                Name = name
            };
            return View(viewModel);
        }










        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
