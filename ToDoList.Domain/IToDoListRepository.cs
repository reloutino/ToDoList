using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.Domain
{
    public interface IToDoListRepository
    {
        Task<IEnumerable<ToDoTask>> GetByUserId(Guid userId);

        Task<ToDoTask> GetById(Guid toDoTaskId);

        Task Create(ToDoTask toDoTask);

        Task Update(ToDoTask toDoTask);

        Task Delete(Guid toDoTaskId);
    }
}
