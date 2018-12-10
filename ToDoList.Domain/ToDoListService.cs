using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Domain.Commands;

namespace ToDoList.Domain
{
    public class ToDoListService : IToDoListService
    {
        private IToDoListRepository _toDoRepository;

        public ToDoListService(IToDoListRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task<IEnumerable<ToDoTask>> GetByUserId(Guid userId)
        {
            return await _toDoRepository.GetByUserId(userId);
        }

        public async Task<ToDoTask> GetById(Guid userId, Guid taskId)
        {

            var existingTask = await _toDoRepository.GetById(taskId);

            if (existingTask == null)
            {
                throw new Exception("can't retrieve task - id not found");
            }

            if (existingTask.UserId != userId)
            {
                throw new Exception("can't retrieve task - wrong user");
            }

            return existingTask;
        }

        public async Task CreateTask(CreateNewToDoTask command)
        {
            var existingTask = await _toDoRepository.GetById(command.TaskId);
            if (existingTask != null)
            {
                throw new Exception("can't create task - id already present");
            }

            await _toDoRepository.Create(
                new ToDoTask()
                {
                    CreationDateTimeOffset = DateTimeOffset.Now,
                    Description = command.Description,
                    Id = command.TaskId,
                    IsChecked = false,
                    ModificationDateTimeOffset = DateTimeOffset.Now,
                    UserId = command.UserId
                });
        }

        public async Task UpdateTask(UpdateToDoTask command)
        {
            var existingTask = await _toDoRepository.GetById(command.TaskId);
            if (existingTask == null)
            {
                throw new Exception("can't update task - id not found");
            }

            if (existingTask.UserId != command.UserId)
            {
                throw new Exception("can't update task - wrong user");
            }

            await _toDoRepository.Update(
                new ToDoTask()
                {
                    CreationDateTimeOffset = existingTask.CreationDateTimeOffset,
                    Description = command.Description ?? existingTask.Description,
                    Id = existingTask.Id,
                    IsChecked = command.IsChecked ?? existingTask.IsChecked,
                    ModificationDateTimeOffset = DateTimeOffset.Now,
                    UserId = existingTask.UserId
                });
        }

        public async Task DeleteTask(DeleteToDoTask command)
        {
            var existingTask = await _toDoRepository.GetById(command.TaskId);
            if (existingTask == null)
            {
                throw new Exception("can't update task - id not found");
            }

            if (existingTask.UserId != command.UserId)
            {
                throw new Exception("can't update task - wrong user");
            }

            await _toDoRepository.Delete(command.TaskId);
        }
    }
}
