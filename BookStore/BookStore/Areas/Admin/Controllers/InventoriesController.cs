using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using Azure;
using X.PagedList;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Inventory")]
    public class InventoriesController : Controller
    {
        private readonly BookStoreContext _context;

        public InventoriesController(BookStoreContext context)
        {
            _context = context;
        }

        // GET: Admin/Inventories

        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var bookStoreContext = _context.Inventories.Include(i => i.Product);
            PagedList<Inventory> Inventorys = new PagedList<Inventory>(bookStoreContext, pageNumber, pageSize);
            return View(Inventorys);
        }

        // GET: Admin/Inventories/Details/5
        [Route("Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // GET: Admin/Inventories/Create
        [Route("create")]
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: Admin/Inventories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,StockQuantity,LastUpdate")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", inventory.ProductId);
            return View(inventory);
        }

        // GET: Admin/Inventories/Edit/5
        [Route("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", inventory.ProductId);
            return View(inventory);
        }

        // POST: Admin/Inventories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,StockQuantity,LastUpdate")] Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryExists(inventory.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", inventory.ProductId);
            return View(inventory);
        }

        // GET: Admin/Inventories/Delete/5

        [Route("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .Include(i => i.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventory == null)
            {
                return NotFound();
            }

            return View(inventory);
        }

        // POST: Admin/Inventories/Delete/5

        [Route("Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory != null)
            {
                _context.Inventories.Remove(inventory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryExists(int id)
        {
            return _context.Inventories.Any(e => e.Id == id);
        }
    }
}
