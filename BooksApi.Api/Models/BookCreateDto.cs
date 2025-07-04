using System.ComponentModel.DataAnnotations;

namespace BooksApi.Api.Models
{
    public class BookCreateDto
    {
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(maximumLength: 200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Author is required")]
        [StringLength(100, ErrorMessage = "Author cannot exceed 100 characters")]
        public string Author { get; set; } = string.Empty;

        [Range(1000, 2025, ErrorMessage = "Publication year must be between 1000 and current year.")]
        public int PublicationYear { get; set; }
    }
}