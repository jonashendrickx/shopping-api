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
            
            modelBuilder.Entity<Product>(m =>
            {
                m.HasKey(x => x.Id);
                
                m.Property(x => x.Code)
                    .HasMaxLength(10)
                    .IsRequired();

                m.Property(x => x.Name)
                    .HasMaxLength(50)
                    .IsRequired();
            });

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
            });
        }
    }
}