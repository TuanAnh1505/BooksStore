using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Models;
using System.Diagnostics;
using BookStore.Dtos;
using Microsoft.Extensions.Hosting;
using X.PagedList;
using X.PagedList.Extensions;

namespace BookStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Products")]
    public class ProductsController : Controller
    {
        private readonly BookStoreContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductsController(BookStoreContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Admin/Products
        [Route("")]
        [Route("Index")]
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var products = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Inventories)
            .ToPagedList();
            PagedList<Product> product = new PagedList<Product>(products, pageNumber, pageSize);
            return View(product);
        }
        

        // GET: Admin/Products/Details/5
        [Route("Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Inventories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        [Route("Create")]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View(new CreateProductDto());
        }

        // POST: Admin/Products/Create
        [HttpPost]
        [Route("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    Describe = productDto.Describe,
                    CategoryId = productDto.CategoryId
                };

                if (productDto.Image != null)
                {
                    var filePath = Path.Combine("wwwroot/images", Path.GetFileName(productDto.Image.FileName));
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await productDto.Image.CopyToAsync(stream);
                    }
                    product.Image = filePath;
                }

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", productDto.CategoryId);
            return View(productDto);
        }


        // GET: Admin/Products/Edit/5
        [Route("Edit")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditProductDto productDto)
        {
            if (id != productDto.Id)
            {
                return NotFound();
            }

            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                // Chuyển DTO thành model Product trước khi trả về view
                var product = new Product
                {
                    Id = productDto.Id,
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    Describe = productDto.Describe,
                    CategoryId = productDto.CategoryId,
                    Image = existingProduct.Image // Giữ nguyên ảnh nếu không cập nhật
                };

                ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
                return View(product);
            }

            // Xử lý upload ảnh mới nếu có
            if (productDto.Image != null)
            {
                var wwwRootPath = _hostEnvironment.WebRootPath;
                var filePath = Path.Combine(wwwRootPath, "images", Path.GetFileName(productDto.Image.FileName));

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await productDto.Image.CopyToAsync(stream);
                }

                existingProduct.Image = $"{Path.GetFileName(productDto.Image.FileName)}";
            }

            // Cập nhật các trường khác
            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.Price = productDto.Price;
            existingProduct.Describe = productDto.Describe;
            existingProduct.CategoryId = productDto.CategoryId;

            try
            {
                _context.Update(existingProduct);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == existingProduct.Id))
                {
                    return NotFound();
                }
                else
                {
                    ModelState.AddModelError("", "The product was updated by another user. Please try again.");
                    return View(existingProduct);
                }
            }

            return RedirectToAction(nameof(Index));
            
        }

        // GET: Admin/Products/Delete/5
        [Route("Delete")]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Inventories)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/ProductAdmin/Delete/5
        [Route("Delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
