using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedule.DAL.Models;
using TaskSchedule.DAL;
using Microsoft.EntityFrameworkCore;
using TaskSchedule.BLL.Models;

namespace TaskSchedule.BLL.Services
{
    public static class BoardService
    {
        public async static Task<List<MyBoardsListVM>> GetBoardForUser(this ApplicationContext context, int userId)
        {
            return context.UserBoardRoles.Include(i => i.Board).Include(i => i.Role).Where(i => i.UserId == userId).Select(i => new MyBoardsListVM(i.BoardId, i.Board.Name, i.Role.Name)).ToList();
        }
        public async static Task<Board> GetBoardById(this ApplicationContext context, int? boardId)
        {
            return context.Boards.First(i => i.Id == boardId);
        }
        public async static Task CreateBoard(this ApplicationContext context, string name, string description, int? userId)
        {
            Board board = new Board() { Description = description, Name = name, OwnerId = userId };
            await context.Boards.AddAsync(board);
            await context.SaveChangesAsync();
            UserBoardRole role = new UserBoardRole() { BoardId = board.Id, RoleId = context.Roles.Where(i => i.Name == "Owner").First().Id, UserId = userId };
            await context.UserBoardRoles.AddAsync(role);
            await context.SaveChangesAsync();
        }
    }
}
