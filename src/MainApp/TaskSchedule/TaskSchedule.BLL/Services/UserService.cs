using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedule.BLL.Models;
using TaskSchedule.DAL;
using TaskSchedule.DAL.Models;

namespace TaskSchedule.BLL.Services
{
    public static class UserService
    {
        public async static Task<User> CreateUser(this ApplicationContext context, string name, string email, string password)
        {
            User newUser = new User { Name = name, Email = email, Password = password };
            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();
            return newUser;
        }
        public async static Task<List<UserComboboxVM>> GetUsers(this ApplicationContext context)
        {
            return context.Users.Select(i => new UserComboboxVM() { Id = i.Id, Name = i.Name }).ToList();
        }
        public async static Task<User?> LoginUser(this ApplicationContext context, string email, string password)
        {
            var user = await context.Users.FirstOrDefaultAsync(i => i.Email == email);
            if (user == null || user.Password != password)
            {
                return null;
            }
            return user;
        }
    }
}
