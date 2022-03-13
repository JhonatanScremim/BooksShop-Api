using BooksShop.Catalog.Domain;
using Microsoft.EntityFrameworkCore;

namespace BooksShop.Catalog.Repository.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Book>? Book {get; set;}
        public DbSet<Author>? Author {get; set;}
        public DbSet<AuthorBook>? AuthorBook {get; set;}
        public DbSet<Publisher>? Publisher {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<AuthorBook>()
            .HasKey(x => new {x.AuthorId, x.BookId});
        }
    }
}