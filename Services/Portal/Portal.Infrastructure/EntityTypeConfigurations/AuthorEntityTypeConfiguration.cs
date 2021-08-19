using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Core;

namespace Portal.Infrastructure.EntityTypeConfigurations
{
    public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable(nameof(Author));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasMany(x => x.Books).WithOne(x => x.Author).HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}