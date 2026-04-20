using Microsoft.EntityFrameworkCore;

public class MinimalApiDatabaseContext : DbContext
{

  public DbSet<Course> Courses {get; set;}
  public MinimalApiDatabaseContext(DbContextOptions options) : base(options)
  {
    

  }


}