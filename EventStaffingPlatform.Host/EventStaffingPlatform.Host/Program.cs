using EventStaffingPlatform.Host.Events;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("EspDbConnectionString")
        ?? throw new InvalidOperationException("Connection string 'EspDbConnectionString' is missing.");

    var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
    return dataSourceBuilder.Build();
});

builder.Services.AddDbContext<EventDbContext>((sp, options)=>
{
    var dataSource = sp.GetRequiredService<NpgsqlDataSource>();
    options.UseNpgsql(
        dataSource,
        npgsqlOptions => npgsqlOptions.CommandTimeout(30).MigrationsHistoryTable("__ef_migrations_history"));

    if (builder.Environment.IsDevelopment())
        options.LogTo(
            Console.WriteLine,
            [DbLoggerCategory.Database.Command.Name],
            LogLevel.Information)
        .EnableDetailedErrors()
        .EnableSensitiveDataLogging();
});

builder.Services.AddScoped<IEventsRepository, EventsRepository>();
builder.Services.AddScoped<IEventsService, EventsService>();

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

app.MapPost("/events/create", async (EventDto newEvent, IEventsService service) =>
{
    try
    {
        var result = await service.CreateNewEvent(newEvent);
        if (result)
            return Results.Created();
        else
            return Results.UnprocessableEntity();
    }
    catch(Exception ex)
    {
        return Results.ValidationProblem([new KeyValuePair<string, string[]>("Exception", [ex.Message])]);
    }
});

app.Run();