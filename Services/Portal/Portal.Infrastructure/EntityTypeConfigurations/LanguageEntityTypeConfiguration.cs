using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Core;

namespace Portal.Infrastructure.EntityTypeConfigurations
{
    public class LanguageEntityTypeConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.ToTable(nameof(Language));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.Books).WithOne(x => x.Language).HasForeignKey(x => x.LanguageId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}