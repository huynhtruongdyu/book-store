using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Core;

namespace Portal.Infrastructure.EntityTypeConfigurations
{
    public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable(nameof(Book));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.CoverUrl).IsRequired();

            builder.HasOne(x => x.Publisher).WithMany(x => x.Books).HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Author).WithMany(x => x.Books).HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Language).WithMany(x => x.Books).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.BookCategories).WithOne(x => x.Book).HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}