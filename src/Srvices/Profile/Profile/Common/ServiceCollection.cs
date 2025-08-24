
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Profile.Infrastructure.Data;

using RedLockNet.SERedis.Configuration;

using StackExchange.Redis;

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

        public static void AddRedis(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")));
        }

        public static void AddDistributelock(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton(sp =>
            {
                var connectionMultiplexer = sp.GetRequiredService<IConnectionMultiplexer>();
                var lockMultiplexer = new RedLockMultiplexer(connectionMultiplexer);
                return lockMultiplexer;
            });
        }
    }
}