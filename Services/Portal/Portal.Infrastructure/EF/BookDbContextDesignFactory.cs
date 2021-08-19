using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Portal.Infrastructure.EF
{
    public class BookDbContextDesignFactory : IDesignTimeDbContextFactory<BookDbContext>
    {
        public BookDbContext CreateDbContext(string[] args)
        {
            //var connectionString = "Server=127.0.0.1;Port=5432;Database=htd-dev;User Id=admin;Password=root;";
            //var connectionString = "Host:localhost;Port:5432;Database:ta-studo-dev;Username:admin;Password:root";
            var connectionString = @"Data Source=DESKTOP-SSRQPRI\WINCC;Initial Catalog=book-store-db;Integrated Security=True;Pooling=False";
            var optionsBuilder = new DbContextOptionsBuilder<BookDbContext>()
                .UseSqlServer(connectionString);

            return new BookDbContext(optionsBuilder.Options);
        }
    }
}