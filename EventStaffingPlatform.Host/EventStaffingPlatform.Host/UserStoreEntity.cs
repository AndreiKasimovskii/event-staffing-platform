public class UserStoreEntity
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public char Sex { get; set; }
    public DateTime BirthDate { get; set; }
    public required string Login { get; set; }
    public required string Password { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
}
