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

app.MapPost("/events/create", async (CreateEventRequest request, IEventsService service) =>
{
    try
    {
		var result = await service.CreateNewEvent(request);
		if (result)
			return Results.Ok();
		else
			return Results.Problem(statusCode: StatusCodes.Status500InternalServerError);
    }
    catch(ArgumentException ex)
    {
        return Results.ValidationProblem(new Dictionary<string, string[]>
        {
            [ex.ParamName ?? "Event"] = [ex.Message]
        });
    }
});

app.MapGet("/events", async (IEventsService service, CancellationToken cancellationToken) =>
{
    return await service.GetAllEventsAsync(cancellationToken);
});

app.MapGet("/events/actual", async (IEventsService service, CancellationToken cancellationToken) =>
{
    return await service.GetActualEventsAsync(cancellationToken);
});

app.MapGet("/events/{id}", async (int id, IEventsService service, CancellationToken cancellationToken) =>
{
    var gettingResult = await service.GetEventAsync(id, cancellationToken);
    if (!gettingResult.IsSuccess)
        return Results.NotFound();
    return Results.Ok(gettingResult.Event);
});

app.MapDelete("/events/{id}", async (int id, IEventsService service, CancellationToken cancellationToken) =>
{
    var result = await service.DeleteEventAsync(id, cancellationToken);
    if (!result.IsSuccess)
        return result.ReasonType switch
        {
            ReasonType.EventNotFound => Results.NotFound(),
            _ => Results.InternalServerError()
        };

    return Results.NoContent();
});

app.Run();