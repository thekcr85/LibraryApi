using LibraryApi.Application.Books.Interfaces.Repositories;
using LibraryApi.Infrastructure.Persistence;
using LibraryApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApi.Infrastructure.DependencyInjection;

/// <summary>
/// Extension methods for configuring Infrastructure layer services.
/// </summary>
public static class DependencyInjection
{
	/// <summary>
	/// Registers Infrastructure layer services to the dependency injection container.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <param name="configuration">The application configuration.</param>
	/// <returns>The service collection for chaining.</returns>
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
	{
		// Register DbContext
		services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseSqlServer(
				configuration.GetConnectionString("DefaultConnection"),
				sqlOptions => sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
		});

		// Register repositories
		services.AddScoped<IBookRepository, BookRepository>();

		return services;
	}
}
