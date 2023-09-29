using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApiMySample.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
