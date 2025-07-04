using BooksApi.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksApi.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Things Fall Apart", Author = "Chinua Achebe", PublicationYear = 1958 },
                new Book { Id = 2, Title = "Half of a Yellow Sun", Author = "Chimamanda Ngozi Adichie", PublicationYear = 2006 },
                new Book { Id = 3, Title = "The Palm-Wine Drinkard", Author = "Amos Tutuola", PublicationYear = 1952 }
            );
        }
    }
}