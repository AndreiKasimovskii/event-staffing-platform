using EventStaffingPlatform.Host;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PositionsDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PositionsConncection"),
        npgsqlOptions => npgsqlOptions.CommandTimeout(30).MigrationsHistoryTable("__ef_migrations_history"))
    .LogTo(
        Console.WriteLine,
        [DbLoggerCategory.Database.Command.Name],
        LogLevel.Information)
    .EnableDetailedErrors();
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

app.MapPost("/positions/new_position", async (Position position, IPositionService service) =>
{
    await service.CreatePosition(position);
});

app.Run();
