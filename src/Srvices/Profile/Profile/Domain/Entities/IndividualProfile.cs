using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Profile.Domain.Entities
{
    public sealed class IndividualProfile : Profile
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string NationalCode { get; set; }

    }

    public class IndividualProfileEntityTypeConfiguration : IEntityTypeConfiguration<IndividualProfile>
    {
        public void Configure(EntityTypeBuilder<IndividualProfile> builder)
        {

            builder.Property(x => x.Name).IsRequired().IsUnicode(false).HasMaxLength(35);
            builder.Property(x => x.LastName).IsRequired().IsUnicode(false).HasMaxLength(35);
            builder.Property(x => x.NationalCode).IsRequired().IsUnicode(false).HasMaxLength(10);
            builder.HasIndex(x => x.NationalCode).IsClustered(false).IsUnique();

        }
    }
}
