using EventStaffingPlatform.Host;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PositionsDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PositionsConncection"),
        npgsqlOptions => npgsqlOptions.CommandTimeout(30).MigrationsHistoryTable("__ef_migrations_history"));

    if (builder.Environment.IsDevelopment())
        options.LogTo(
            Console.WriteLine,
            [DbLoggerCategory.Database.Command.Name],
            LogLevel.Information)
        .EnableDetailedErrors()
        .EnableSensitiveDataLogging();
});
builder.Services.AddTransient<IPositionService, PositionService>();

#region Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/positions/new_position", async (PositionDto position, IPositionService service) =>
{
    var result = await service.CreatePosition(position);
    if (result.IsSuccess)
        return Results.Created();
    else
        return Results.UnprocessableEntity();
});

app.Run();

public record PositionDto(string Title, string? Description, string? Requirements, string Conditions);