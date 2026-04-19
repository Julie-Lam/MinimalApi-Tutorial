using Microsoft.EntityFrameworkCore; 


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MinimalApiDatabaseContext>(options => 
  options.UseInMemoryDatabase("MinimalApiDatabase")); 


var app = builder.Build();


/* 
In this section we can expose our API endpoints, with MapGet, MapPost, MapPut etc 
*/

// MapGet takes 2 parameters. 1st is the pattern, i.e. "/" specifies the path for this 'API' call. Because it is just /, it means when the site root is visited it will return this call. I.e. http://localhost:5047
app.MapGet("/", () => "Hello World!");

app.MapGet("/courses", async (MinimalApiDatabaseContext db) =>
{
  var courses = await db.Courses.ToListAsync(); 

  return Results.Ok(courses); 

}); 

app.MapPost("courses", async (Course course, MinimalApiDatabaseContext db) =>
{
  db.Courses.Add(course); 
  await db.SaveChangesAsync(); 

  return Results.Created(); 
})
app.Run();



public class Course
{
  public int CourseId { get; set; }
  public string CourseName { get; set; } = string.Empty; 

  public int CourseDuration { get; set; }

  public int CourseType { get; set; }
}

public class MinimalApiDatabaseContext : DbContext
{

  public DbSet<Course> Courses {get; set;}
  public MinimalApiDatabaseContext(DbContextOptions options) : base(options)
  {
    

  }


}