using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Profile.Domain.Entities
{
    public sealed class CorporateProfile : Profile
    {
        public required string CompanyName { get; set; }
        public required string NationalId { get; set; }
    }

    public class CorporateProfileEntityTypeConfiguration : IEntityTypeConfiguration<CorporateProfile>
    {
        public void Configure(EntityTypeBuilder<CorporateProfile> builder)
        {

            builder.Property(x => x.CompanyName).IsRequired().IsUnicode(false).HasMaxLength(100);
            builder.Property(x => x.NationalId).IsRequired().IsUnicode(false).HasMaxLength(10);
            builder.HasIndex(x => x.NationalId).IsClustered(false).IsUnique();

        }
    }
}
