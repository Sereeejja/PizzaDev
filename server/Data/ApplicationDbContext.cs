using Microsoft.EntityFrameworkCore;
using PizzaDev.Models;
using Type = PizzaDev.Models.Type;

namespace PizzaDev.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Pizza> Pizzas { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<PizzaSize> PizzaSizes { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Type> Types { get; set; }
    public DbSet<PizzaType> PizzaTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        /* PizzaSize Many to Many */
        modelBuilder.Entity<PizzaSize>()
            .HasKey(ps => new { ps.SizeId, ps.PizzaId });

        modelBuilder.Entity<PizzaSize>()
            .HasOne(ps => ps.Size)
            .WithMany(s => s.PizzaSizes)
            .HasForeignKey(ps => ps.SizeId);

        modelBuilder.Entity<PizzaSize>()
            .HasOne(ps => ps.Pizza)
            .WithMany(p => p.PizzaSizes)
            .HasForeignKey(ps => ps.PizzaId);
        
        
        /* Pizza Cart Many to Many */
        modelBuilder.Entity<CartItem>()
            .HasKey(cp => new { cp.PizzaId, cp.CartId, cp.TypeId, cp.SizeId });

        modelBuilder.Entity<CartItem>()
            .HasOne(cp => cp.Pizza)
            .WithMany(p => p.CartItems)
            .HasForeignKey(cp => cp.PizzaId);

        modelBuilder.Entity<CartItem>()
            .HasOne(cp => cp.Cart)
            .WithMany(c => c.CartItems)
            .HasForeignKey(cp => cp.CartId);
        /* Pizza type many to many */
        modelBuilder.Entity<PizzaType>()
            .HasKey(pt => new { pt.PizzaId, pt.TypeId });
        
        modelBuilder.Entity<PizzaType>()
            .HasOne(pt => pt.Type)
            .WithMany(t => t.PizzaTypes)
            .HasForeignKey(pt => pt.TypeId);
        modelBuilder.Entity<PizzaType>()
            .HasOne(pt => pt.Pizza)
            .WithMany(t => t.PizzaTypes)
            .HasForeignKey(pt => pt.PizzaId);
        
        // Seed data for Sizes
        modelBuilder.Entity<Size>().HasData(
            new Size { Id = 1, Name = "26cm" },
            new Size { Id = 2, Name = "30cm" },
            new Size { Id = 3, Name = "40cm" }
        );

        // Seed data for Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Meat" },
            new Category { Id = 2, Name = "Cheese" },
            new Category { Id = 3, Name = "Large" },
            new Category { Id = 4, Name = "Small" },
            new Category { Id = 5, Name = "Vegetarian" }
        );

        // Seed data for Types
        modelBuilder.Entity<Type>().HasData(
            new Type { Id = 2, Name = "Thin" },
            new Type { Id = 1, Name = "Traditional" }
        );

        // Seed data for Pizzas
        modelBuilder.Entity<Pizza>().HasData(
            new Pizza
            {
                Id = 99,
                Name = "Pepperoni Fresh with Peppers",
                ImageUrl = "https://dodopizza-a.akamaihd.net/static/Img/Products/f035c7f46c0844069722f2bb3ee9f113_584x584.jpeg",
                Price = 803,
                CategoryId = 1,
                Rating = 4,
            },
            new Pizza 
            {
                Id = 1,
                Name = "Cheese",
                ImageUrl = "https://dodopizza-a.akamaihd.net/static/Img/Products/Pizza/ru-RU/2ffc31bb-132c-4c99-b894-53f7107a1441.jpg",
                Price = 245,
                CategoryId = 1,
                Rating = 6,
            },
            new Pizza 
            {
                Id = 2,
                Name = "Chicken Barbecue",
                ImageUrl = "https://dodopizza-a.akamaihd.net/static/Img/Products/Pizza/ru-RU/6652fec1-04df-49d8-8744-232f1032c44b.jpg",
                Price = 295,
                CategoryId = 1,
                Rating = 4,
            },
            new Pizza 
            {
                Id = 3,
                Name = "Sweet and Sour Chicken",
                ImageUrl = "https://dodopizza-a.akamaihd.net/static/Img/Products/Pizza/ru-RU/af553bf5-3887-4501-b88e-8f0f55229429.jpg",
                Price = 275,
                CategoryId = 2,
                Rating = 2,
            },
            new Pizza 
            {
                Id = 4,
                Name = "Cheeseburger Pizza",
                ImageUrl = "https://dodopizza-a.akamaihd.net/static/Img/Products/Pizza/ru-RU/b750f576-4a83-48e6-a283-5a8efb68c35d.jpg",
                Price = 415,
                CategoryId = 3,
                Rating = 8,
            },
            new Pizza 
            {
                Id = 5,
                Name = "Crazy Pepperoni",
                ImageUrl = "https://dodopizza-a.akamaihd.net/static/Img/Products/Pizza/ru-RU/1e1a6e80-b3ba-4a44-b6b9-beae5b1fbf27.jpg",
                Price = 580,
                CategoryId = 2,
                Rating = 2,
            },
            new Pizza 
            {
                Id = 6,
                Name = "Pepperoni",
                ImageUrl = "https://dodopizza-a.akamaihd.net/static/Img/Products/Pizza/ru-RU/d2e337e9-e07a-4199-9cc1-501cc44cb8f8.jpg",
                Price = 675,
                CategoryId = 1,
                Rating = 9,
            },
            new Pizza 
            {
                Id = 7,
                Name = "Margherita",
                ImageUrl = "https://dodopizza-a.akamaihd.net/static/Img/Products/Pizza/ru-RU/d48003cd-902c-420d-9f28-92d9dc5f73b4.jpg",
                Price = 450,
                CategoryId = 4,
                Rating = 10,
            },
            new Pizza 
            {
                Id = 8,
                Name = "Four Seasons",
                ImageUrl = "https://dodopizza-a.akamaihd.net/static/Img/Products/Pizza/ru-RU/ec29465e-606b-4a04-a03e-da3940d37e0e.jpg",
                Price = 395,
                CategoryId = 5,
                Rating = 10,
            },
            new Pizza 
            {
                Id = 9,
                Name = "Vegetables and Mushrooms",
                ImageUrl = "https://dodopizza-a.akamaihd.net/static/Img/Products/Pizza/ru-RU/30367198-f3bd-44ed-9314-6f717960da07.jpg",
                Price = 285,
                CategoryId = 5,
                Rating = 7,
            }
        );

        // Seed data for PizzaSizes
        modelBuilder.Entity<PizzaSize>().HasData(
            new PizzaSize { PizzaId = 99, SizeId = 1 },
            new PizzaSize { PizzaId = 99, SizeId = 2 },
            new PizzaSize { PizzaId = 99, SizeId = 3 },
            new PizzaSize { PizzaId = 1, SizeId = 1 },
            new PizzaSize { PizzaId = 1, SizeId = 3 },
            new PizzaSize { PizzaId = 2, SizeId = 1 },
            new PizzaSize { PizzaId = 2, SizeId = 3 },
            new PizzaSize { PizzaId = 3, SizeId = 1 },
            new PizzaSize { PizzaId = 3, SizeId = 2 },
            new PizzaSize { PizzaId = 3, SizeId = 3 },
            new PizzaSize { PizzaId = 4, SizeId = 1 },
            new PizzaSize { PizzaId = 4, SizeId = 2 },
            new PizzaSize { PizzaId = 4, SizeId = 3 },
            new PizzaSize { PizzaId = 5, SizeId = 2 },
            new PizzaSize { PizzaId = 5, SizeId = 3 },
            new PizzaSize { PizzaId = 6, SizeId = 1 },
            new PizzaSize { PizzaId = 6, SizeId = 2 },
            new PizzaSize { PizzaId = 6, SizeId = 3 },
            new PizzaSize { PizzaId = 7, SizeId = 1 },
            new PizzaSize { PizzaId = 7, SizeId = 2 },
            new PizzaSize { PizzaId = 7, SizeId = 3 },
            new PizzaSize { PizzaId = 8, SizeId = 1 },
            new PizzaSize { PizzaId = 8, SizeId = 2 },
            new PizzaSize { PizzaId = 8, SizeId = 3 },
            new PizzaSize { PizzaId = 9, SizeId = 1 },
            new PizzaSize { PizzaId = 9, SizeId = 2 },
            new PizzaSize { PizzaId = 9, SizeId = 3 }
        );

        // Seed data for PizzaTypes
        modelBuilder.Entity<PizzaType>().HasData(
            new PizzaType { PizzaId = 99, TypeId = 2 },
            new PizzaType { PizzaId = 99, TypeId = 1 },
            new PizzaType { PizzaId = 1, TypeId = 2 },
            new PizzaType { PizzaId = 2, TypeId = 2 },
            new PizzaType { PizzaId = 3, TypeId = 1 },
            new PizzaType { PizzaId = 4, TypeId = 2 },
            new PizzaType { PizzaId = 4, TypeId = 1 },
            new PizzaType { PizzaId = 5, TypeId = 2 },
            new PizzaType { PizzaId = 6, TypeId = 2 },
            new PizzaType { PizzaId = 6, TypeId = 1 },
            new PizzaType { PizzaId = 7, TypeId = 2 },
            new PizzaType { PizzaId = 7, TypeId = 1 },
            new PizzaType { PizzaId = 8, TypeId = 2 },
            new PizzaType { PizzaId = 8, TypeId = 1 },
            new PizzaType { PizzaId = 9, TypeId = 2 },
            new PizzaType { PizzaId = 9, TypeId = 1 }
        );

        modelBuilder.Entity<Cart>().HasData(
            new Cart{Id = 1, TotalPrice = 0}
        );

    }
}