using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asm5.Models;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace asm5.Controllers
{
    public class BookController : Controller
    {
        private readonly BookDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(BookDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Book or Book/Index?categoryId=X
        public async Task<IActionResult> Index(int? categoryId)
        {
            // Load Categories and their Books to compute count
            var categories = await _context.Categories.Include(c => c.Books).ToListAsync();
            ViewBag.Categories = categories;
            ViewBag.SelectedCategoryId = categoryId;

            // Load Books (filtered by categoryId if provided)
            var booksQuery = _context.Books.Include(b => b.Category).AsQueryable();
            if (categoryId.HasValue && categoryId.Value > 0)
            {
                booksQuery = booksQuery.Where(b => b.CategoryId == categoryId.Value);
            }

            var books = await booksQuery.ToListAsync();
            return View(books);
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.BookId == id);
            
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookId,Title,Author,Price,Description,CategoryId")] Book book, IFormFile? imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                string fileName = Path.GetFileName(imageFile.FileName);
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Content", "ImageBooks");
                
                // Ensure directory exists
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                
                book.Image = fileName;
                ModelState.Remove("Image");
            }
            else
            {
                ModelState.AddModelError("Image", "Vui lòng chọn một tệp hình ảnh cho sách.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", book.CategoryId);
            return View(book);
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", book.CategoryId);
            return View(book);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookId,Title,Author,Price,Description,CategoryId")] Book book, IFormFile? imageFile)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            // Retrieve the existing book to get current image name
            var existingBook = await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.BookId == id);
            if (existingBook == null)
            {
                return NotFound();
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                string fileName = Path.GetFileName(imageFile.FileName);
                string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Content", "ImageBooks");
                
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                
                book.Image = fileName;
                ModelState.Remove("Image");
            }
            else
            {
                // Re-use existing image path
                book.Image = existingBook.Image;
                ModelState.Remove("Image");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookId))
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
            ViewBag.CategoryId = new SelectList(_context.Categories, "CategoryId", "CategoryName", book.CategoryId);
            return View(book);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
