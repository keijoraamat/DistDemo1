#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.App;
using Domain;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ListItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ListItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ListItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ListItems.Include(l => l.AppUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/ListItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listItem = await _context.ListItems
                .Include(l => l.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listItem == null)
            {
                return NotFound();
            }

            return View(listItem);
        }

        // GET: Admin/ListItems/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Admin/ListItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,IsDone,AppUserId,Id,UpdatedAt")] ListItem listItem)
        {
            if (ModelState.IsValid)
            {
                listItem.Id = Guid.NewGuid();
                _context.Add(listItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", listItem.AppUserId);
            return View(listItem);
        }

        // GET: Admin/ListItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listItem = await _context.ListItems.FindAsync(id);
            if (listItem == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", listItem.AppUserId);
            return View(listItem);
        }

        // POST: Admin/ListItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Description,IsDone,AppUserId,Id,UpdatedAt")] ListItem listItem)
        {
            if (id != listItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListItemExists(listItem.Id))
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
            ViewData["AppUserId"] = new SelectList(_context.Users, "Id", "Id", listItem.AppUserId);
            return View(listItem);
        }

        // GET: Admin/ListItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listItem = await _context.ListItems
                .Include(l => l.AppUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (listItem == null)
            {
                return NotFound();
            }

            return View(listItem);
        }

        // POST: Admin/ListItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var listItem = await _context.ListItems.FindAsync(id);
            _context.ListItems.Remove(listItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListItemExists(Guid id)
        {
            return _context.ListItems.Any(e => e.Id == id);
        }
    }
}
