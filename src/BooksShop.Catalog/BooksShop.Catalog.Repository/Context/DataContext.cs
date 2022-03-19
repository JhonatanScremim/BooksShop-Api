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

            modelBuilder.Entity<AuthorBook>()
                .HasOne(bc => bc.Book)
                .WithMany(b => b.AuthorBooks)
                .HasForeignKey(bc => bc.BookId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<AuthorBook>()
                .HasOne(bc => bc.Author)
                .WithMany(c => c.AuthorBooks)
                .HasForeignKey(bc => bc.AuthorId);
        }
    }
}