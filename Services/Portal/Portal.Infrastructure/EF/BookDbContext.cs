using Microsoft.EntityFrameworkCore;
using Portal.Infrastructure.EntityTypeConfigurations;

namespace Portal.Infrastructure.EF
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    var tableName = entityType.GetTableName();
            //    if (tableName.StartsWith("AspNet"))
            //    {
            //        entityType.SetTableName(tableName.Substring(6));
            //    }
            //}

            modelBuilder.ApplyConfiguration(new BookEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BookCategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LanguageEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PublisherEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LogEntityTypeConfiguration());
        }
    }
}