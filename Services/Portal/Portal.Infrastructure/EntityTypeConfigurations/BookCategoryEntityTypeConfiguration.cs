using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Core;

namespace Portal.Infrastructure.EntityTypeConfigurations
{
    public class BookCategoryEntityTypeConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.ToTable(nameof(BookCategory));
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Book).WithMany(x => x.BookCategories).HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Category).WithMany(x => x.BookCategories).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}