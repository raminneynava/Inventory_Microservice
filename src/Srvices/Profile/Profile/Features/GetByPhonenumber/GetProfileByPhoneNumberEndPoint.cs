using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Profile.Infrastructure.Data;

using System.Threading.Tasks;

namespace Profile.Features.GetByPhonenumber
{
    public static class GetProfileByPhoneNumberEndPoint
    {
        public static RouteGroupBuilder MapGetProfilePhoneNumberByIdEndPoint(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapGet("/{id}/phone-number", GetProfileByPhoneNumber);
            return groupBuilder;
        }

        private static async Task<IResult> GetProfileByPhoneNumber(
            int id,                  
            ProfileDbContext dbcontext,          
            CancellationToken cancellationToken)
        {
            var profile = await dbcontext.Profiles
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return profile is not null
                ? TypedResults.Ok(new GetProfilePhoneNumberByIdResponse(profile.PhoneNumber))
                : TypedResults.NotFound(new
                {
                    Message = "No profile found with the provided phone number",
                    Id = id,
                    Timestamp = DateTime.UtcNow
                });
        }
    }
}

public record GetProfilePhoneNumberByIdResponse(string phonenumber);