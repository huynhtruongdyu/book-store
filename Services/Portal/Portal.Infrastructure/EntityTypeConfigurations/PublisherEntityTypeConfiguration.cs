using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Core;

namespace Portal.Infrastructure.EntityTypeConfigurations
{
    public class PublisherEntityTypeConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable(nameof(Publisher));
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.Books).WithOne(x => x.Publisher).HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}