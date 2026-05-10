using Microsoft.EntityFrameworkCore;

internal class UsersDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserStoreEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
    }
}
