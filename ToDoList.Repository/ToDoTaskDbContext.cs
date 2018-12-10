using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain;

namespace ToDoList.Repository
{
    public class ToDoTaskDbContext : DbContext
    {
        
        public ToDoTaskDbContext(DbContextOptions<ToDoTaskDbContext> options)
            : base(options)
        {
        }

        public DbSet<ToDoTask> ToDoTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
