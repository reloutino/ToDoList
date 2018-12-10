using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Commands;

namespace ToDoList.Domain
{
    public interface IToDoListService
    {
        Task<IEnumerable<ToDoTask>> GetByUserId(Guid userId);

        Task CreateTask(CreateNewToDoTask command);

        Task UpdateTask(UpdateToDoTask command);

        Task DeleteTask(DeleteToDoTask command);

        Task<ToDoTask> GetById(Guid userId, Guid taskId);
    }
}
