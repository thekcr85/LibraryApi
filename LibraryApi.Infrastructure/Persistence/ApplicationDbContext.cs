using LibraryApi.Domain.Common;
using LibraryApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryApi.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
	public DbSet<Book> Books => Set<Book>();
	public DbSet<Author> Authors => Set<Author>();

	/// <summary>
	/// Configures the EF Core model for the application's entities.
	/// </summary>
	/// <param name="modelBuilder">The <see cref="ModelBuilder"/> used to configure entity mappings.</param>
	/// <remarks>
	/// This method defines primary keys, property constraints, indexes, and relationships for:
	/// <list type="bullet">
	/// <item><description><see cref="Book"/> entity: Title is required, ISBN is unique, includes search index on Title, and PublishedDate column type is set to date.</description></item>
	/// <item><description><see cref="Author"/> entity: Name is required, Email has max length, BirthDate column type is set to date, and one-to-many relationship with Books.</description></item>
	/// </list>
	/// </remarks>
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Book>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Title)
				.IsRequired()
				.HasMaxLength(100);
			entity.Property(e => e.ISBN)
				.HasMaxLength(20);
			entity.HasIndex(e => e.ISBN)
				.IsUnique()
				.HasFilter("[ISBN] IS NOT NULL"); // Allow multiple nulls for ISBN
			entity.HasIndex(e => e.Title);
			entity.Property(e => e.PublishedDate)
				.HasColumnType("date"); // Store only the date part
			entity.Property(e => e.CreatedAt)
				.IsRequired();
			entity.Property(e => e.UpdatedAt);
			entity.HasOne(e => e.Author)
				.WithMany(a => a.Books)
				.HasForeignKey(e => e.AuthorId)
				.OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete
		});

		modelBuilder.Entity<Author>(entity =>
		{
			entity.HasKey(e => e.Id);
			entity.Property(e => e.Name)
				.IsRequired()
				.HasMaxLength(100);
			entity.Property(e => e.Email)
				.HasMaxLength(100);
			entity.Property(e => e.BirthDate)
				.HasColumnType("date");
			entity.Property(e => e.CreatedAt)
				.IsRequired();
			entity.Property(e => e.UpdatedAt);
		});
	}

	public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
	{
		SetAuditProperties();
		return base.SaveChangesAsync(cancellationToken);
	}

	public override int SaveChanges()
	{
		SetAuditProperties();
		return base.SaveChanges();
	}

	private void SetAuditProperties()
	{
		var entries = ChangeTracker.Entries<BaseEntity>();

		foreach (var entry in entries)
		{
			if (entry.State == EntityState.Added)
			{
				entry.Entity.CreatedAt = DateTime.UtcNow;
			}
			else if (entry.State == EntityState.Modified)
			{
				entry.Entity.UpdatedAt = DateTime.UtcNow;
			}
		}
	}
}
