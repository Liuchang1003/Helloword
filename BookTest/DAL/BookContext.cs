﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using BookTest.Models;

namespace BookTest.DAL
{
    public class BookContext:DbContext
    {
        public BookContext() : base("BookContext")
        {
            
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
        
    }
}