using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTodo
{
    public class MyDbContext : DbContext
    {
        public string UserName { get; set; }
        public MyDbContext(string username)
        {
            UserName = username;
            Database.EnsureCreated();
        }
        public DbSet<TodoModel> TodoList { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                UserName = "test";
            }
            optionsBuilder.UseSqlite(@"Data Source="+ UserName + ".db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            MyBlogMap(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        private void MyBlogMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoModel>().HasKey(p => p.RecID);
            modelBuilder.Entity<TodoModel>().ToTable("TodoList");
        }
    }
}
