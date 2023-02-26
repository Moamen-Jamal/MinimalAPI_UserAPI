using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace SAQAYA.UserAPI.Entities.Entities
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User")
                .Property(b => b.FirstName).HasColumnName("FirstName").HasMaxLength(100).IsRequired();
            builder.Property(b => b.LastName).HasColumnName("LastName").HasMaxLength(100).IsRequired();
            builder.Property(b => b.Email).HasColumnName("Email").HasMaxLength(100).IsRequired();
            builder.HasIndex(b => b.Email).IsUnique();
            builder.Property(b => b.MarketingConsent).HasColumnName("MarketingConsent").IsRequired();

        }
    }
}
