using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Profile.Domain.Entities
{
    public abstract class Profile
    {
        public int Id { get; set; }
        public required string PhoneNumber { get; set; }
        public required string TimeZoneId { get; set; }
        public required string DateFormat { get; set; }
        public required string TimeFormat { get; set; }

        public static Profile Create(string phonenumber, string code, string name)
        {

            Profile profile = code.Length switch
            {
                10 => new IndividualProfile
                {
                    PhoneNumber = phonenumber,
                    TimeFormat = "HH:mm",
                    DateFormat = "yyyy/MM/dd",
                    TimeZoneId = "Asia/Tehran",
                    Name = GetFiratName(name),
                    LastName = GetLastName(name),
                    NationalCode = code,
                },
                11 => new CorporateProfile
                {
                    TimeFormat = "HH:mm",
                    DateFormat = "yyyy/MM/dd",
                    TimeZoneId = "Asia/Tehran",
                    CompanyName = name,
                    NationalId = code,
                    PhoneNumber = phonenumber,

                },
                _ => throw new InvalidOperationException("Unknown profile type.")
            };
            return profile;

            string GetFiratName(string name)
            {
                return name.Split(' ')[0];
            }
            string GetLastName(string name)
            {
                var parts = name.Split(' ');
                return string.Join(" ", parts[1..]);
            }
        }
    }



    public class ProfileEntityTypeConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.PhoneNumber).IsClustered(false).IsUnique();
            builder.Property(x => x.PhoneNumber).IsRequired().IsUnicode(false).HasMaxLength(11);
            builder.Property(x => x.TimeZoneId).IsRequired().HasMaxLength(20);
            builder.Property(x => x.DateFormat).IsRequired().HasMaxLength(20);
            builder.Property(x => x.TimeZoneId).IsRequired().HasMaxLength(20);
        }
    }
}
