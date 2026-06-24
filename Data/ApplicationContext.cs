using BasicFitnessApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace BasicFitnessApp.Data;

public class ApplicationContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<UserProfile> UserProfiles => Set<UserProfile>();
    public DbSet<Workout> Workouts => Set<Workout>();
    public DbSet<Exercise> Exercises => Set<Exercise>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Password).IsRequired().HasMaxLength(256);
            entity.HasOne(e => e.Profile).WithOne(e => e.User).HasForeignKey<UserProfile>(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
            entity.HasMany(e => e.Tokens).WithOne(e => e.User).HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.Cascade);
        });
        
        modelBuilder.Entity<UserProfile>(entity =>
        {
           entity.ToTable("UserProfiles");
           entity.HasKey(e => e.Id);
           entity.Property(e => e.UserId).IsRequired();
           entity.HasIndex(e => e.UserId).IsUnique();
           entity.Property(e => e.Height).HasPrecision(5, 2);
           entity.Property(e => e.Weight).HasPrecision(5, 2);
           entity.HasMany(e => e.Workouts).WithOne(e => e.UserProfile).HasForeignKey(e => e.ProfileId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Workout>(entity =>
        {
            entity.ToTable("Workouts");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ProfileId).IsRequired();
            entity.HasIndex(e => e.ProfileId);
            entity.HasMany(e => e.Exercises).WithOne(e => e.Workout).HasForeignKey(e => e.WorkoutId);
        });

        modelBuilder.Entity<Exercise>(entity =>
        {
            entity.ToTable("Exercises");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.WorkoutId).IsRequired();
            entity.HasIndex(e => e.WorkoutId);
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshTokens");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.UserId).IsRequired().HasMaxLength(256);
            entity.Property(e => e.HashedValue).IsRequired().HasMaxLength(256);
            entity.HasIndex(e => e.HashedValue).IsUnique();
            entity.Property(e => e.Version).HasColumnName("xmin").HasColumnType("xid").IsRowVersion();
        });
    }
}  