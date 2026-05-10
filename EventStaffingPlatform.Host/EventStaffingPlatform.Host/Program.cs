using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UsersDbContext>(options 
    => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");

app.MapPost("/sign_in", async (UserModel userData, UsersDbContext dbContext) =>
{
    if (userData.Email is null && userData.PhoneNumber is null)
        return Results.BadRequest();
    var newUser = dbContext.Users.Add(new UserStoreEntity()
    {
        Login = userData.Login,
        Password = userData.Password,
        FirstName = userData.FirstName,
        LastName = userData.LastName,
        Sex = userData.Sex,
        BirthDate = userData.BirthDate,
        Email = userData.Email,
        PhoneNumber = userData.PhoneNumber
    });

    await dbContext.SaveChangesAsync();
    return Results.Created($"/users/{newUser?.Entity.Id}", newUser?.Entity);
});

app.Run();

record UserModel(string Login, string Password, string FirstName, string LastName, char Sex, DateTime BirthDate, string? Email, string? PhoneNumber);
