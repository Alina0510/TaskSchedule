using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedule.DAL.Models;

namespace TaskSchedule.DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Board> Boards { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<BoardTask> BoardTasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserBoardRole> UserBoardRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=TaskSchedule;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
