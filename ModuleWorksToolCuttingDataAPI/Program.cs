using GetComponents;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var config = app.Configuration;

app.MapGet("/components", async (HttpContext context) =>
{
    var connectionString = config.GetConnectionString("DefaultConnection");

    var components = new Components(connectionString);
   

    return Results.Json(components.GetComponents());
})
.WithName("GetComponent")
.WithOpenApi();

app.Run();
