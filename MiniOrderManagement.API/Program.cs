using MediatR;
using MiniOrderManagement.API.ExceptionHandling;
using MiniOrderManagement.Application;
using MiniOrderManagement.Application.Behaviors;
using MiniOrderManagement.Application.Interfaces;
using MiniOrderManagement.Infrastructure.Persistence;
using MiniOrderManagement.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// CORS configuration
// Allows the React frontend running on http://localhost:5173
// to make HTTP requests to this ASP.NET Core API.
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactApp", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddInfrastructure(
    builder.Configuration);

builder.Services.AddLogging();

// Global Exception Handling
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Global Exception Handler Middleware
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Apply the CORS policy defined above.
// This allows requests coming from the React application
// at http://localhost:5173.
app.UseCors("ReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();