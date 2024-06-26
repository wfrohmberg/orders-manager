﻿using Microsoft.EntityFrameworkCore;
using OrdersManager.Models;

namespace OrdersManager.DbContexts
{
    public class OrdersContext: DbContext
    {
        private readonly IConfiguration _configuration;

        public OrdersContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(_configuration.GetConnectionString("Sqlite"));
        }

        public DbSet<User> Users { get; set; } 
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrdersProducts { get; set; }
    }
}
