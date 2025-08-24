using Microsoft.EntityFrameworkCore;

using Profile.Domain.Entities;
using Profile.Infrastructure.Data;

namespace Profile.Features.CreateIfNotexist
{
    public static class CreateIfNotexistEndPoint
    {
        public const string RedisDistributedLock = "Key";

        public static RouteGroupBuilder MapCreateIfNotexistEndPoint(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapPost("/", PostCreateIfNotexist);
            return groupBuilder;
        }

        private static async Task<IResult> PostCreateIfNotexist(
            CreateIfNotexistRequest request,
            ProfileDbContext dbcontext,
            CancellationToken cancellationToken)
        {


            var existprofile = await dbcontext.Profiles
                .FirstOrDefaultAsync(x => x.PhoneNumber == request.Phonenumber, cancellationToken);


            if (existprofile is null)
            {
                var newProfile = Domain.Entities.Profile.Create(request.Phonenumber, request.Code, request.Name);
                dbcontext.Profiles.Add(newProfile);
                await dbcontext.SaveChangesAsync(cancellationToken);
                return TypedResults.Ok(newProfile);
            }


            if (existprofile is IndividualProfile individualProfile)
            {
                if (individualProfile.NationalCode != request.Code)
                {
                    return Results.BadRequest();
                }
            }
            else if (existprofile is CorporateProfile corporateProfile)
            {
                if (corporateProfile.NationalId != request.Code)
                {
                    return Results.BadRequest();
                }
            }
            else
                throw new InvalidOperationException();


            return Results.Ok(existprofile);

        }
    }
}

public record CreateIfNotexistRequest(string Name, string Code, string Phonenumber);