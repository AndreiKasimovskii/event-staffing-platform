using EventStaffingPlatform.Host;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UsersDbContext>(options 
    => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<VacanciesDbContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("VacaniesDbConnection"),
        npgsqlOptions => npgsqlOptions.CommandTimeout(30).MigrationsHistoryTable("__ef_migrations_history"))
    .LogTo(
        Console.WriteLine,
        [DbLoggerCategory.Database.Command.Name],
        LogLevel.Information)
    .EnableDetailedErrors();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/vacancies/new_vacancy", async (VacancyModel vacancy, VacanciesDbContext context) =>
{
    Vacancy newVacancy = new()
    {
        Title = vacancy.Title,
        Conditions = vacancy.Conditions,
        Functions = vacancy.Functions,
        CreateDate = DateTimeOffset.UtcNow,
        Status = VacancyStatus.Open
    };
    var requirements = vacancy.Requirements?.Select(r => new Requirement()
        {
            RequirementTypeId = r.RequirementTypeId,
            Vacancy = newVacancy,
            Value = r.Value
        });
    await context.Vacancies.AddAsync(newVacancy);
    if(requirements is not null)
        await context.Requirements.AddRangeAsync(requirements);
    await context.SaveChangesAsync();
});

app.MapPost("/requirements/requirement_types/create_new", async (RequirementTypeModel requirementType, VacanciesDbContext context) =>
{
    RequirementType newRequirementType = new()
    {
        Name = requirementType.Name,
        Caption = requirementType.Caption,
        ValueType = requirementType.ValueType,
        ValueConfiguration = requirementType.ValueConfiguration
    };
    await context.RequirementTypes.AddAsync(newRequirementType);
    await context.SaveChangesAsync();
});

app.Run();

record UserModel(string Login, string Password, string FirstName, string LastName, char Sex, DateTime BirthDate, string? Email, string? PhoneNumber);

record VacancyModel(string Title, string? Description, VacancyRequirement[]? Requirements, string Conditions, string Functions);

record VacancyRequirement(int RequirementTypeId, string Value);

record RequirementTypeModel(string Name, string Caption, string ValueType, string ValueConfiguration);
