﻿using Microsoft.EntityFrameworkCore;

namespace MovieProject.Models
{
    public class MovieContext:DbContext
    {
        public MovieContext (DbContextOptions<MovieContext> options) : base(options) { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }



    }
}
