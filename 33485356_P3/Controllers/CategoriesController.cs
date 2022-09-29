using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Repositories;

namespace DeviceManagement_WebApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ConnectedOfficeContext _context;
        private readonly iRepository<Device, int> deviceRepository;
        private readonly iRepository<Category, int> categoryRepository;
        private readonly iRepository<Zone, int> zoneRepository;

        public CategoriesController(iRepository<Device, int> deviceRepository,
         iRepository<Category, int> categoryRepository,
         iRepository<Zone, int> zoneRepository)
        {
            this.deviceRepository = deviceRepository;
            this.categoryRepository = categoryRepository;
            this.zoneRepository = zoneRepository;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            var category = await categoryRepository.GetAll();
            return View(category);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await categoryRepository.GetAll();
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            category.CategoryId = Guid.NewGuid();
            await categoryRepository.Insert(category);
            await categoryRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("CategoryId,CategoryName,CategoryDescription,DateCreated")] Category category)
        {
            if (id != category.CategoryId)
            {
                return NotFound();
            }
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.CategoryId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await categoryRepository.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await categoryRepository.Delete(id);
            await categoryRepository.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Category.Any(e => e.CategoryId == id);
        }
    }
}
