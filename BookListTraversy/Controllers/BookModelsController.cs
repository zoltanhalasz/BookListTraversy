using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookListTraversy.Data;

namespace BookListTraversy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookModelsController : ControllerBase
    {
        private readonly BookContext _context;

        public BookModelsController(BookContext context)
        {
            _context = context;
        }

        // GET: api/BookModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookModel>>> GetBookTable()
        {
            return await _context.BookTable.ToListAsync();
        }

        // GET: api/BookModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BookModel>> GetBookModel(int id)
        {
            var bookModel = await _context.BookTable.FindAsync(id);

            if (bookModel == null)
            {
                return NotFound();
            }

            return bookModel;
        }

        // PUT: api/BookModels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookModel(int id, BookModel bookModel)
        {
            if (id != bookModel.id)
            {
                return BadRequest();
            }

            _context.Entry(bookModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BookModels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<BookModel>> PostBookModel(BookModel bookModel)
        {
            _context.BookTable.Add(bookModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBookModel", new { id = bookModel.id }, bookModel);
        }

        // DELETE: api/BookModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BookModel>> DeleteBookModel(int id)
        {
            var bookModel = await _context.BookTable.FindAsync(id);
            if (bookModel == null)
            {
                return NotFound();
            }

            _context.BookTable.Remove(bookModel);
            await _context.SaveChangesAsync();

            return bookModel;
        }

        private bool BookModelExists(int id)
        {
            return _context.BookTable.Any(e => e.id == id);
        }
    }
}
