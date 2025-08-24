

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.AddServiceDbContext();
builder.AddRedis();
builder.AddDistributelock();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapGroup("/profiles")
    .MapCreateIfNotexistEndPoint()
    .MapGetProfileByPhoneNumberEndPoint();

app.Run();

