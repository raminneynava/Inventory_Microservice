using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Profile.Domain.Entities;
using Profile.Infrastructure.Data;

using RedLockNet.SERedis;

namespace Profile.Features.CreateIfNotexist
{
    public static class CreateIfNotexistEndPoint
    {
        public const string RedisDistributedLockPattern = "profile:create-not-exist:{0}";
        public static TimeSpan ExpireTime = TimeSpan.FromSeconds(20);
        public static TimeSpan WaitTime = TimeSpan.FromSeconds(5);
        public static TimeSpan RetryTime = TimeSpan.FromSeconds(3);

        public static RouteGroupBuilder MapCreateIfNotexistEndPoint(this RouteGroupBuilder groupBuilder)
        {
            groupBuilder.MapPost("/not-exist", PostCreateIfNotexist);
            return groupBuilder;
        }

        private static async Task<IResult> PostCreateIfNotexist(
            CreateIfNotexistRequest request,
            ProfileDbContext dbcontext,
            [FromServices] RedLockFactory redLockFactory,
            CancellationToken cancellationToken)
        {
            var resource = string.Format(RedisDistributedLockPattern,request.Phonenumber);

            await using (var redlock = await redLockFactory.CreateLockAsync(resource, ExpireTime, WaitTime, RetryTime, cancellationToken))
            {
                if (redlock.IsAcquired)
                {
                    var existprofile = await dbcontext.Profiles.FirstOrDefaultAsync(x => x.PhoneNumber == request.Phonenumber, cancellationToken);

                    if (existprofile is null)
                    {
                        var newProfile = Domain.Entities.Profile.Create(request.Phonenumber, request.Code, request.Name);
                        dbcontext.Profiles.Add(newProfile);
                        await dbcontext.SaveChangesAsync(cancellationToken);
                        return Results.Ok(new CreateIfNotexistResponse(newProfile.Id));
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


                    return Results.Ok(new CreateIfNotexistResponse(existprofile.Id));
                }

                return Results.InternalServerError();

            }



        }
    }
}

public record CreateIfNotexistRequest(string Name, string Code, string Phonenumber);
public record CreateIfNotexistResponse(int Id);