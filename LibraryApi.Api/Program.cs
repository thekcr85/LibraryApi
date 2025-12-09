using LibraryApi.Api.Middleware;
using LibraryApi.Application.DependencyInjection;
using LibraryApi.Infrastructure.DependencyInjection;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

// Add modern ProblemDetails support (RFC 7807)
builder.Services.AddProblemDetails(options =>
{
	options.CustomizeProblemDetails = ctx =>
	{
		ctx.ProblemDetails.Instance = ctx.HttpContext.Request.Path;
		if (!ctx.HttpContext.Response.Headers.ContainsKey("X-Trace-Id"))
		{
			ctx.HttpContext.Response.Headers.Append("X-Trace-Id", ctx.HttpContext.TraceIdentifier);
		}
	};
});

// Add OpenAPI support
builder.Services.AddOpenApi();

// Add Infrastructure and Application layers
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

var app = builder.Build();

// Exception handling pipeline (order matters!)
app.UseExceptionHandler();
app.UseStatusCodePages();

// Custom exception handler middleware for domain-specific exceptions
app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
