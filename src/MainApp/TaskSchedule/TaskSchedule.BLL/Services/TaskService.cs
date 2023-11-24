using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskSchedule.BLL.Models;
using TaskSchedule.DAL.Models;
using TaskSchedule.DAL;
using Microsoft.EntityFrameworkCore;

namespace TaskSchedule.BLL.Services
{
    public static class TaskService
    {
        public async static Task CreateTask(this ApplicationContext context, string name, string description, DateTime dateTime, int userId, int? boardId)
        {
            await context.BoardTasks.AddAsync(new BoardTask() { AssignedUserId = userId, BoardId = boardId, Deadline = dateTime, Description = description, Status = "ToDo", Title = name });
            await context.SaveChangesAsync();
        }
        public async static ValueTask<(List<BoardTask>, List<BoardTask>, List<BoardTask>)> GetTasksByBoardId(this ApplicationContext context, int? boardId)
        {
            var list = context.BoardTasks.Where(i => i.BoardId == boardId).ToList();
            return new (list.Where(i => i.Status == "ToDo").ToList(), list.Where(i => i.Status == "InReview").ToList(), list.Where(i => i.Status == "Done").ToList());
        }
        public async static Task<BoardTask> GetTaskById(this ApplicationContext context, int? taskId)
        {
            return context.BoardTasks.First(i => i.Id == taskId);
        }
        public async static ValueTask SaveTask(this ApplicationContext context, BoardTask task)
        {
            context.Update(task);
            context.SaveChanges();
        }
    }
}
