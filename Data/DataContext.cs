using System;
using Microsoft.EntityFrameworkCore;
using NewsApp.Models;

namespace NewsApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)  { }
        public DbSet<User> Users { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<ArchivedNews> Archive { get; set; }
    }
}