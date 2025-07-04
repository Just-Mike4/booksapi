using BooksApi.Api.Data;
using BooksApi.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace BooksApi.Api.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Book>> GetAllBooksAsync(string? searchTerm=null)
        {
            var query = _context.Books.AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm));
            }
            return await query.ToListAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }
        public async Task<Book> AddBookAsync(BookCreateDto newBookDto)
        {
            var book = new Book
            {
                Title = newBookDto.Title,
                Author = newBookDto.Author,
                PublicationYear = newBookDto.PublicationYear
            };
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<bool> UpdateBookAsync(int id, BookUpdateDto updatedBookDto)
        {
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
            {
                return false;
            }

            existingBook.Title = updatedBookDto.Title;
            existingBook.Author = updatedBookDto.Author;
            existingBook.PublicationYear = updatedBookDto.PublicationYear;

            _context.Books.Update(existingBook);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteBookAsync(int id)
        {
            var bookToRemove = await _context.Books.FindAsync(id);
            if (bookToRemove == null)
            {
                return false;
            }

            _context.Books.Remove(bookToRemove);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}