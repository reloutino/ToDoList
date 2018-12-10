using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain;

namespace ToDoList.Repository
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly ToDoTaskDbContext _dbContext;

        public ToDoListRepository(ToDoTaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ToDoTask>> GetByUserId(Guid userId)
        {
            return await _dbContext.Set<ToDoTask>()
                .AsNoTracking()
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }

        public async Task<ToDoTask> GetById(Guid toDoTaskId)
        {
            return await _dbContext.Set<ToDoTask>()
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == toDoTaskId);
        }

        public async Task Create(ToDoTask toDoTask)
        {
            await _dbContext.Set<ToDoTask>().AddAsync(toDoTask);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(ToDoTask toDoTask)
        {
            _dbContext.Set<ToDoTask>().Update(toDoTask);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(Guid toDoTaskId)
        {
            var toDoTask = await GetById(toDoTaskId);
            _dbContext.Set<ToDoTask>().Remove(toDoTask);
            await _dbContext.SaveChangesAsync();
        }
    }
}
