using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Portal.Infrastructure.EntityTypeConfigurations.Auth
{
    public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<IdentityRole>

    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {

            builder.HasData(
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }, 
                new IdentityRole
                {
                    Name = "Manager",
                    NormalizedName = "MANAGER"
                }, 
                new IdentityRole
                {
                    Name = "Staff",
                    NormalizedName = "STAFF"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
           );
        }
    }
}