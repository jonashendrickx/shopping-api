using System;
using System.Collections.Generic;
using JonasHendrickx.Shop.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace JonasHendrickx.Shop.DataContext.Context
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions options) : base(options)  
        {  
        }
        
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketLineItem> BasketLineItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductListing> ProductListings { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            var products = new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code = "PEN",
                    Name = "Lana Pen"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code = "TSHIRT",
                    Name = "Lana T-Shirt"
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Code = "MUG",
                    Name = "Lana Coffee Mug"
                },
            };
            
            modelBuilder.Entity<Product>(m =>
            {
                m.HasKey(x => x.Id);
                
                m.Property(x => x.Code)
                    .HasMaxLength(10)
                    .IsRequired();

                m.Property(x => x.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                m.HasData(products);
            });
            
            var productListings = new List<ProductListing>
            {
                new ProductListing { Id = Guid.NewGuid(), Price = 5, StartedAt = DateTime.Today, ProductId = products[0].Id },
                new ProductListing { Id = Guid.NewGuid(), Price = 20, StartedAt = DateTime.Today, ProductId = products[1].Id },
                new ProductListing { Id = Guid.NewGuid(), Price = 7.5M, StartedAt = DateTime.Today, ProductId = products[2].Id }
            };
            
            modelBuilder.Entity<ProductListing>(m =>
            {
                m.HasKey(x => x.Id);
                
                m.Property(x => x.StartedAt)
                    .IsRequired();

                m.Property(x => x.EndedAt);

                m.Property(x => x.Price).HasPrecision(11, 2);
                
                m.HasOne(x => x.Product)
                    .WithMany(x => x.ProductListings)
                    .HasForeignKey(x => x.ProductId);

                m.HasData(productListings);
            });
            
            modelBuilder.Entity<BasketLineItem>(m =>
            {
                m.HasKey(x => x.Id);
                
                m.Property(x => x.Amount)
                    .IsRequired();
                
                m.HasOne(x => x.ProductListing)
                    .WithMany(x => x.BasketLineItems)
                    .HasForeignKey(x => x.ProductListingId);
            });
            
            modelBuilder.Entity<Basket>(m =>
            {
                m.HasKey(x => x.Id);
                
                m.HasMany(x => x.LineItems)
                    .WithOne(x => x.Basket)
                    .HasForeignKey(x => x.BasketId);
            });
            
            modelBuilder.Entity<Discount>(m =>
            {
                m.HasKey(x => x.Id);
                
                m.Property(x => x.Code)
                    .IsRequired();
                
                m.Property(x => x.ProductListingId)
                    .IsRequired();
                
                m.Property(x => x.Rules)
                    .IsRequired();
                
                m.HasOne<ProductListing>(x => x.ProductListing)
                    .WithMany(x => x.Discounts)
                    .HasForeignKey(x => x.ProductListingId);
                
                var discounts = new List<Discount>
                {
                    new Discount
                    {
                        Id = Guid.NewGuid(),
                        Code = "QTY_TO_PCT",
                        Rules = "{\"qty\":\"3\",\"pct\":\"0.25\"}",
                        ProductListingId = productListings[1].Id // T-SHIRT 25% discount if you at least 3
                    },
                    new Discount
                    {
                        Id = Guid.NewGuid(),
                        Code = "BUY_TO_FREE_QTY",
                        Rules = "{\"buy_qty\":\"2\",\"free_qty\":\"1\"}",
                        ProductListingId = productListings[0].Id // PEN BUY 2 GET 1 FREE DISCOUNT
                    }
                };

                m.HasData(discounts);
            });
        }
    }
}