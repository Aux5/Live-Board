﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LiveBoard.Models;

namespace LiveBoard.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options){}
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Class> Class { get; set; }
        //public DbSet<Image> Image { get; set; 
        public DbSet<Notes> Notes { get; set; }
        public DbSet<UserClass> UserClass { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                Role = "Teacher",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };
            
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);


            modelBuilder.Entity<Class>().HasData(
               new Class()
               {
                   ClassId = 1,
                   ClassName = "Mrs.Teacher's Class",

               },
               new Class()
               {
                   ClassId = 2,
                   ClassName = "Mr.Teach's Class"
               }
               
               );

          //  modelBuilder.Entity<Image>().HasData(
          //new Image()
          //{
          //    ImageId = 1,
          //    ImgB64 = "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",
             
          //},
          //new Image()
          //{
          //    ImageId = 2,
          //    ImgB64 = "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",
              
          //}
          //);


            modelBuilder.Entity<Notes>().HasData(
              new Notes()
              {
                  NoteId = 1,
                  Text = "This is a note",
                  UserId = user.Id,
                  
              },
              new Notes()
              {
                  NoteId = 2,
                  Text = "This is another note",
                  UserId = user.Id,

              }
          );
            modelBuilder.Entity<UserClass>().HasData(
             new UserClass()
             {
                 UserClassId = 1,
                 UserId = user.Id,
                 ClassId = 1
               
             },
            new UserClass()
            {
                UserClassId = 2,
                UserId = user.Id,
                ClassId = 1

            }, new UserClass()
            {
                UserClassId = 3,
                UserId = user.Id,
                ClassId = 2

            }, new UserClass()
            {
                UserClassId = 4,
                UserId = user.Id,
                ClassId = 2

            }
         );


        }

    }
}
