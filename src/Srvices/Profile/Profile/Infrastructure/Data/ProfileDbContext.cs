using Microsoft.EntityFrameworkCore;

namespace Profile.Infrastructure.Data
{
    public sealed class ProfileDbContext(DbContextOptions<ProfileDbContext> contextOptions) : DbContext(contextOptions)
    {
        private const string DbContextSchema = "Profiles";
        public const string DbContextConnectionStringName = "DbContextString";


        public DbSet<Domain.Entities.Profile> Profiles { get; set; }
        public DbSet<Domain.Entities.IndividualProfile> IndividualProfiles { get; set; }
        public DbSet<Domain.Entities.CorporateProfile> CorporateProfiles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(DbContextSchema);
            modelBuilder.Entity<Domain.Entities.Profile>().UseTpcMappingStrategy();

            modelBuilder.ApplyConfigurationsFromAssembly(AssemblyMarker.Assembly);


        }
    }
}
