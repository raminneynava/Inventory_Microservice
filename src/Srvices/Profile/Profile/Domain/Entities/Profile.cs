using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Profile.Domain.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public required string PhoneNumber { get; set; }
        public required string TimeZoneId { get; set; }
        public required string DateFormat { get; set; }
        public required string TimeFormat { get; set; }
    }

    public class ProfileEntityTypeConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.PhoneNumber).IsClustered(false).IsUnique();
            builder.Property(x => x.PhoneNumber).IsRequired().IsUnicode(false).HasMaxLength(11);
            builder.Property(x => x.TimeZoneId).IsRequired().HasMaxLength(5);
            builder.Property(x => x.DateFormat).IsRequired().HasMaxLength(20);
            builder.Property(x => x.TimeZoneId).IsRequired().HasMaxLength(20);
        }
    }
}
