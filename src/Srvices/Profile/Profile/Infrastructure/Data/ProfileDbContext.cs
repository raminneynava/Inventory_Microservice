using Microsoft.EntityFrameworkCore;

namespace Profile.Infrastructure.Data
{
    public sealed class ProfileDbContext(DbContextOptions<ProfileDbContext> contextOptions) : DbContext(contextOptions)
    {
        private const string DbContextSchema = "Profiles";
        public const string DbContextConnectionStringName = "DbContextString";


        DbSet<Domain.Entities.Profile> Profiles { get; set; }
        DbSet<Domain.Entities.IndividualProfile> IndividualProfiles { get; set; }
        DbSet<Domain.Entities.CorporateProfile> CorporateProfiles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DbContextSchema);
            modelBuilder.Entity<Domain.Entities.Profile>().UseTpcMappingStrategy();

            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyMarker.Assembly);


        }
    }
}
