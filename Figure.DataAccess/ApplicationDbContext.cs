using Figure.DataAccess.Entities;
using Figure.DataAccess.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Figure.DataAccess;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser> {
	private readonly IDbSeeder _seeder;

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDbSeeder seeder) : base(options) {
		_seeder = seeder;
	}

	public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	public DbSet<Order> Orders { get; set; }
	public DbSet<Entities.Figure> Figures { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);
		_seeder.Seed(modelBuilder);
	}
}
