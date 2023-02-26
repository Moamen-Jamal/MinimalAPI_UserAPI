using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SAQAYA.UserAPI.Entities.Entities
{
    public class BaseModelConfiguration : IEntityTypeConfiguration<BaseModel>
    {
        public void Configure(EntityTypeBuilder<BaseModel> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.Id)
            .ValueGeneratedOnAdd();
        }

    }
}
