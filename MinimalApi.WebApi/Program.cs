var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


/* 
In this section we can expose our API endpoints, with MapGet, MapPost, MapPut etc 
*/

// MapGet takes 2 parameters. 1st is the pattern, i.e. "/" specifies the path for this 'API' call. Because it is just /, it means when the site root is visited it will return this call. I.e. http://localhost:5047
app.MapGet("/", () => "Hello World!");

app.Run();

