using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.EntityFrameworkCore; 


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MinimalApiDatabaseContext>(options => 
  options.UseInMemoryDatabase("MinimalApiDatabase")); 

builder.Services.AddAutoMapper(typeof(MinimalApiMapper)); 
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

app.MapPost("/courses", async (CourseDto courseDto, MinimalApiDatabaseContext db, IMapper mapper) =>
{

  var newCourse = mapper.Map<Course>(courseDto); 

  db.Courses.Add(newCourse); 
  await db.SaveChangesAsync(); 


  var courseResponse = mapper.Map<CourseDto>(newCourse); 


  return Results.Created($"/courses/{courseResponse.CourseId}", courseResponse); 
}); 

app.Run();


public class MinimalApiMapper : Profile
{
  public MinimalApiMapper()
  {
    CreateMap<Course, CourseDto>(); 
    CreateMap<CourseDto, Course>(); 
  }
}

public class CourseDto
{
    public int CourseId { get; set; }
  public string CourseName { get; set; } = string.Empty; 

[JsonConverter(typeof(JsonStringEnumConverter))]
  public COURSE_TYPE CourseType { get; set; }
}


public enum COURSE_TYPE
{
  ENGINEERING = 1, 
  MEDICAL = 2, 
  MANAGEMENT = 3
}