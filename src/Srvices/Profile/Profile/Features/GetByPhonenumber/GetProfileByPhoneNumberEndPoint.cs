using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Profile.Infrastructure.Data;

using System.Threading.Tasks;

namespace Profile.Features.GetByPhonenumber
{
    public static class GetProfileByPhoneNumberEndPoint
    {
        public static RouteGroupBuilder MapGetProfileByPhoneNumberEndPoint(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapGet("/{phoneNumber}", GetProfileByPhoneNumber);
            return groupBuilder;
        }

        private static async Task<IResult> GetProfileByPhoneNumber(
            string phoneNumber,                  
            ProfileDbContext dbcontext,          
            CancellationToken cancellationToken)
        {
            var profile = await dbcontext.Profiles
                .FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);

            return profile is not null
                ? TypedResults.Ok(profile)
                : TypedResults.NotFound(new
                {
                    Message = "No profile found with the provided phone number",
                    PhoneNumber = phoneNumber,
                    Timestamp = DateTime.UtcNow
                });
        }
    }
}
