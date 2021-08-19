using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Core;

namespace Portal.Infrastructure.EntityTypeConfigurations
{
    public class LogEntityTypeConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable(nameof(Log));
            builder.HasKey(x => x.Id);
        }
    }
}