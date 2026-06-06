using EventStaffingPlatform.Host;
using Microsoft.EntityFrameworkCore;

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
        Requirements = vacancy.Requirements,
        Conditions = vacancy.Conditions,
        Functions = vacancy.Functions,
        CreateDate = DateTimeOffset.UtcNow,
        Status = VacancyStatus.Open
    };
    await context.Vacancies.AddAsync(newVacancy);
    await context.SaveChangesAsync();
});

app.Run();

record UserModel(string Login, string Password, string FirstName, string LastName, char Sex, DateTime BirthDate, string? Email, string? PhoneNumber);

record VacancyModel(string Title, string? Description, string Requirements, string Conditions, string Functions);
