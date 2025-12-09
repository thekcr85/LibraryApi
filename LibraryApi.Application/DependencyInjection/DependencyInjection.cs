using LibraryApi.Application.Authors.Interfaces.Services;
using LibraryApi.Application.Authors.Services;
using LibraryApi.Application.Books.Interfaces.Services;
using LibraryApi.Application.Books.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryApi.Application.DependencyInjection;

/// <summary>
/// Extension methods for configuring Application layer services.
/// </summary>
public static class DependencyInjection
{
	/// <summary>
	/// Registers Application layer services to the dependency injection container.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <returns>The service collection for chaining.</returns>
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddScoped<IBookService, BookService>();
		services.AddScoped<IAuthorService, AuthorService>();

		return services;
	}
}
