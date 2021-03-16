using CoreAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext()
        {
        }

        public OrderContext(DbContextOptions<OrderContext> options)
            : base(options)
        {
            var orders = new[]
            {
                new Order
                {
                    Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    OrderState = 1,
                    CustomerGuid = Guid.Parse("d3e3137e-ccc9-488c-9e89-50ba354738c2"),
                    CustomerFullName = "Wolfgang Ofner"
                },
                new Order
                {
                    Id = Guid.Parse("bffcf83a-0224-4a7c-a278-5aae00a02c1e"),
                    OrderState = 1,
                    CustomerGuid = Guid.Parse("4a2f1e35-f527-4136-8b12-138a57e1ba08"),
                    CustomerFullName = "Darth Vader"
                },
                new Order
                {
                    Id = Guid.Parse("58e5cd7d-856b-4224-bdff-bd8f85bf5a6d"),
                    OrderState = 2,
                    CustomerGuid = Guid.Parse("334feb16-d7bb-4ca9-ab56-f4fadeb88d21"),
                    CustomerFullName = "Son Goku"
                }
            };


            var movies = new[]
            {
                new Movie
                {
                    Id = 1 ,
                    Name  = "Alien: Covenant",
                    Genre = "Sci-fi",
                    Director = "Ridley Scott"
                },
                new Movie
                {
                     Id = 2 ,
                    Name  = "The Lord Of The Rings",
                    Genre = "Drama ,Fantasy",
                    Director = "Peter Jackson"
                },
                new Movie
                {
                     Id = 3 ,
                    Name  = "Original Sin",
                    Genre = "Drama",
                    Director = "Michael Cristofer"
                },
                new Movie
                {
                     Id = 4 ,
                    Name  = "World War Z",
                    Genre = "Horror",
                    Director = "Marc Forster"
                }
            };


            Order.AddRange(orders);
            SaveChanges();
            Movies.AddRange(movies);
            SaveChanges();
        }

        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CustomerFullName).IsRequired();
            });
        }
    }
}