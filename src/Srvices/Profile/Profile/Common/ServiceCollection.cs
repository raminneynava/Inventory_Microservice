
using Microsoft.EntityFrameworkCore;

using Profile.Infrastructure.Data;

namespace Profile.Common
{
    public static class ServiceCollectionExtention
    {
        public static void AddServiceDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ProfileDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString(ProfileDbContext.DbContextConnectionStringName));

            });
        }
    }
}