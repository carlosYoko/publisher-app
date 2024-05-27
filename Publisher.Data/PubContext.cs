using Microsoft.EntityFrameworkCore;
using Publisher.Domain;

namespace Publisher.Data
{
    public class PubContext : DbContext
    {
        private StreamWriter _writer = new StreamWriter("EFCoreLog.txt", append: true);
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=PubDatabase;Trusted_Connection=True;")
                            .LogTo(_writer.WriteLine,
                                    new[] { DbLoggerCategory.Database.Command.Name }, Microsoft.Extensions.Logging.LogLevel.Information)
                            .EnableSensitiveDataLogging();

        }

        public override void Dispose()
        {
            _writer.Dispose();
            base.Dispose();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                            new Author { AuthorId = 1, FirstName = "Carlos", LastName = "Gimenez" }
                        );
            var authors = new Author[] {
                new Author { AuthorId = 2, FirstName = "Alan", LastName = "Turing" },
                new Author { AuthorId = 3, FirstName = "Richard", LastName = "Stallman" },
                new Author { AuthorId = 4, FirstName = "Robert C.", LastName = "Martin" },
                new Author { AuthorId = 5, FirstName = "Linus", LastName = "Torvalds" },
                new Author { AuthorId = 6, FirstName = "Ada", LastName = "Lovelace" },
            };

            modelBuilder.Entity<Author>().HasData(authors);

            var someBooks = new Book[]
            {
                new Book { BookId = 1, AuthorId = 1, Title = "In God's Ear" },
                new Book { BookId = 2, AuthorId = 3, Title = "A Tale For the Time Being" },
                new Book { BookId = 3, AuthorId = 3, Title = "The Left Hand od Darkness" },
            };

            modelBuilder.Entity<Book>().HasData(someBooks);

            //modelBuilder.Entity<Author>()
            //            .HasMany<Book>()
            //             .WithOne();

            // Mapeo en caso de que no se use la convencion de EF Core

            //modelBuilder.Entity<Author>()
            //            .HasMany( a => a.Books )
            //            .WithOne(b => b.Author)
            //            .HasForeignKey( b => b.AuthorFK );
        }
    }
}
