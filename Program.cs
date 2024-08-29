using BLL.Interfaces;
using BLL.Services;
using DAL.interfaces;
using DAL.ORM;
using Domain.interfaces;



var builder = WebApplication.CreateBuilder(args);

// Register services with the dependency injection container using the new file
builder.Services.AddApplicationServices();



// Add services to the container.
builder.Services.AddControllers();

// Add CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
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

app.UseAuthorization();

app.MapControllers();

// Use CORS policy
app.UseCors("AllowSpecificOrigins");

app.Run();
