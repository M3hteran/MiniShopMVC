using Microsoft.EntityFrameworkCore;
using MiniShopMVC.Models;

namespace MiniShopMVC.Data

{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<CartItem>()
				.HasOne(x => x.Cart)
				.WithMany(x => x.CartItems)
				.HasForeignKey(x => x.CartId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<CartItem>()
				.HasOne(x => x.Product)
				.WithMany(x => x.CartItems)
				.HasForeignKey(x => x.ProductId)
				.OnDelete(DeleteBehavior.Restrict);
			modelBuilder.Entity<AppUser>()
				.HasOne(x => x.Cart)
				.WithOne(x => x.AppUser)
				.HasForeignKey<Cart>(x => x.AppUserId)
				.OnDelete(DeleteBehavior.Restrict);
			modelBuilder.Entity<Order>()
				.Property(x => x.Status)
				.HasConversion<string>();
		}

		public DbSet<Product> Products { get; set; }

		public DbSet<Category> Categories { get; set; }

		public DbSet<AppUser> AppUsers { get; set; }

		public DbSet<Order> Orders { get; set; }

		public DbSet<OrderItem> OrderItems { get; set; }

		public DbSet<CartItem> CartItems { get; set; }

		public DbSet<Cart> Carts { get; set; }
	}
}
