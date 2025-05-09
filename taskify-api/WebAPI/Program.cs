using Infrastructure;
using Application;
using WebAPI;
using WebAPI.Middleware;
using WebAPI.Extension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddCors(options =>
{
    options.AddPolicy("NewPolicy", policy =>
    {
        policy.WithOrigins(
            "http://localhost:4200",    // Angular local
            "http://web:4200"           // Contenedor UI
        )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); 
    });
});



var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ApplyMigration();
app.UseHttpsRedirection();
app.UseCors("NewPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
