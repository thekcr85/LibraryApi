using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Api.Middleware;

/// <summary>
/// Global exception handler middleware that converts unhandled exceptions to RFC 7807 ProblemDetails responses.
/// </summary>
public sealed class GlobalExceptionHandlerMiddleware(
	RequestDelegate next,
	ILogger<GlobalExceptionHandlerMiddleware> logger,
	IHostEnvironment environment)
{
	/// <summary>
	/// Processes an HTTP request by invoking the next middleware in the pipeline and handling any exceptions that occur
	/// during execution.
	/// </summary>
	/// <remarks>If an exception is thrown by the next middleware, it is caught and handled to ensure an appropriate
	/// response is sent to the client. This method should be called as part of the middleware pipeline.</remarks>
	/// <param name="context">The <see cref="HttpContext"/> for the current HTTP request. Provides access to request and response information,
	/// user identity, and other HTTP-specific data.</param>
	/// <returns>A task that represents the asynchronous operation of processing the HTTP request.</returns>
	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await next(context);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(context, ex);
		}
	}

	private async Task HandleExceptionAsync(HttpContext context, Exception ex)
	{
		logger.LogError(ex, "An unhandled exception occurred");

		var statusCode = GetStatusCode(ex);
		var problemDetails = new ProblemDetails
		{
			Status = statusCode,
			Title = GetTitle(ex),
			Detail = environment.IsDevelopment() ? ex.Message : null,
			Type = $"https://httpstatuses.com/{statusCode}",
			Instance = context.Request.Path
		};

		context.Response.ContentType = "application/problem+json";
		context.Response.StatusCode = statusCode;

		await context.Response.WriteAsJsonAsync(problemDetails);
	}

	private static int GetStatusCode(Exception ex) => ex switch
	{
		ArgumentNullException or ArgumentException => StatusCodes.Status400BadRequest,
		KeyNotFoundException => StatusCodes.Status404NotFound,
		InvalidOperationException => StatusCodes.Status409Conflict,
		_ => StatusCodes.Status500InternalServerError
	};

	private static string GetTitle(Exception ex) => ex switch
	{
		ArgumentNullException => "Invalid request",
		ArgumentException => "Invalid request",
		KeyNotFoundException => "Not found",
		InvalidOperationException => "Invalid operation",
		_ => "Internal server error"
	};
}