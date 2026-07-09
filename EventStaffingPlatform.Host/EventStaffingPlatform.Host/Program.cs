using EventStaffingPlatform.Host.Events;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("EspDbConnectionString")
        ?? throw new InvalidOperationException("Connection string 'EspDbConnectionString' is missing.");

    var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
    //dataSourceBuilder.EnableDynamicJson();
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
//builder.Services.AddTransient<IPositionService, PositionService>();
//builder.Services.AddTransient<IConditionsService, ConditionsService>();

#region Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapPost("/positions/new_position", async (PositionDto position, IPositionService service) =>
//{
//    var result = await service.CreatePosition(position);
//    if (result.IsSuccess)
//        return Results.Created();
//    else
//        return Results.UnprocessableEntity();
//});

//app.MapPost("/conditions/new", async (ConditionTemplateDto conditionTemplate, IConditionsService service) =>
//{
//    var result = await service.CreateConditionTemplate(conditionTemplate);
//    if (result.IsSuccess)
//        return Results.Created();
//    else
//        return Results.UnprocessableEntity();
//});

app.Run();

public record PositionDto(string Title, string? Description, string? Requirements, ConditionDto[] Conditions);

public record ConditionDto(string Name, string Value);

public record ConditionTemplateDto(string Name, string Caption, string Template);