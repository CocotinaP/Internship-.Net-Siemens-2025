using Internship.NetSiemens2025.domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Internship.NetSiemens2025.persistence.database;

/// <summary>
/// Database context for the library system.
/// Provides access to Books, Readers, Borrowings, and BorrowingItems.
/// </summary>
public class LibraryDbContext : DbContext
{
    
    public DbSet<Book> Books { get; set; }
    public DbSet<Reader> Readers { get; set; }
    public DbSet<Borrowing> Borrowings { get; set; }
    public DbSet<BorrowingItem> BorrowingItems { get; set; }

    /// <summary>
    /// Initializes a new instance of LibraryDbContext with provided options.
    /// </summary>
    /// <param name="options">
    /// Database context configuration options.
    /// </param>
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options){}

    public LibraryDbContext(){}

    /// <summary>
    /// Configures the database provider if options are not already set.
    /// Defaults to SQLite.
    /// </summary>
    /// <param name="optionsBuilder">
    /// Options builder for configuring the database context.
    /// </param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=Library.db");
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reader>()
            .HasIndex(r => r.Cnp)
            .IsUnique();
        
        modelBuilder.Entity<Reader>()
            .HasIndex(r => r.PhoneNumber)
            .IsUnique();
        
        modelBuilder.Entity<Reader>()
            .HasIndex(r => r.Email)
            .IsUnique();
        
        modelBuilder.Entity<Borrowing>()
            .HasOne(b => b.Reader)
            .WithMany()
            .HasForeignKey("ReaderId")
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<BorrowingItem>()
            .HasOne(bi => bi.Book)
            .WithMany()
            .HasForeignKey("BookId")
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Borrowing>()
            .HasMany(b => b.BorrowingItems)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}