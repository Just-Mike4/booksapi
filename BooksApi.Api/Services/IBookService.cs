using BooksApi.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksApi.Api.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> AddBookAsync(BookCreateDto newBookDto);
        Task<bool> UpdateBookAsync(int id, BookUpdateDto updatedBookDto);
        Task<bool> DeleteBookAsync(int id);
    }
}