using Microsoft.EntityFrameworkCore;
using Webapi.Models;

namespace Webapi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options ):base(options)
        {
            
        }
        public DbSet<Charecter> Charecters {get;set;}
        public DbSet<User> Users {get;set;}
         public DbSet<Weapon> Weapons {get;set;}
         public DbSet<Skill> Skills { get; set; }
         public DbSet<CharecterSkill> CharecterSkills  { get; set; }

         protected override void OnModelCreating(ModelBuilder modelBuilder){
             modelBuilder.Entity<CharecterSkill>().HasKey(cs => new {cs.CharecterId,cs.SkillId});
             modelBuilder.Entity<User>().Property(user => user.Role).HasDefaultValue("Player");

         }
    }
}