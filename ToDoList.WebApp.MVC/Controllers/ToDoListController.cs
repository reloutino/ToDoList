using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ToDoList.Domain;
using ToDoList.Domain.Commands;
using ToDoList.WebApp.MVC.Models;


namespace ToDoList.WebApp.MVC.Controllers
{
    //[Authorize]
    public class ToDoListController : Controller
    {
        private readonly IToDoListService _toDoListService;

        public ToDoListController(IToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        public Guid GetUserId()
        {
            try
            {
                return Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == "UserId").Value);
            }
            catch (Exception)
            {
                var name = User.Claims.FirstOrDefault(x => x.Type == "Name") ==null ? "[unknown user]" : User.Claims.FirstOrDefault(x => x.Type == "Name").Value;
                throw new Exception($"Invalid user : {name} has no userId Claim");
            }

        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UserLogin", "Login");
            }

            var userId = GetUserId();
            var toDoTasks = await _toDoListService.GetByUserId(userId);
            var result = toDoTasks.OrderBy(y => y.CreationDateTimeOffset).
                Select(x => new ToDoTaskViewModel()
            {
                Id = x.Id,
                Description = x.Description,
                IsChecked = x.IsChecked,
                CreationDateTimeOffset = x.CreationDateTimeOffset,
                ModificationDateTimeOffset = x.ModificationDateTimeOffset
            });
            
            return View(result);
        }

        [HttpDelete("todolist/tasks/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            await _toDoListService.DeleteTask(new DeleteToDoTask()
            {
                UserId = userId,
                TaskId = id
            });
            return Json(new { success = true });
        }

        [HttpPut("todolist/tasks/{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] JObject task)
        {
            var userId = GetUserId();
            var description = task["Description"]?.ToObject<string>();
            var isChecked = task["IsChecked"]?.ToObject<bool?>();

            await _toDoListService.UpdateTask(new UpdateToDoTask()
            {
                UserId = userId,
                TaskId = id,
                Description = description,
                IsChecked = isChecked
            });

            var updatedTask = await _toDoListService.GetById(userId, id);
            var updatedTaskViewModel = new ToDoTaskViewModel()
            {
                Id = updatedTask.Id,
                ModificationDateTimeOffset = updatedTask.ModificationDateTimeOffset,
                CreationDateTimeOffset = updatedTask.CreationDateTimeOffset,
                IsChecked = updatedTask.IsChecked,
                Description = updatedTask.Description
            };

            return PartialView("_TodoTaskViewModel", updatedTaskViewModel);
        }

        [HttpPost("todolist/tasks")]
        public async Task<IActionResult> Create([FromBody] JObject task)
        {
            var userId = GetUserId();
            var taskId = task["TaskId"].ToObject<Guid>();
            var description = task["Description"].ToObject<string>();

            await _toDoListService.CreateTask(new CreateNewToDoTask()
            {
                UserId = userId,
                TaskId = taskId,
                Description = description
            });

            var createdTask = await _toDoListService.GetById(userId, taskId);
            var createdTaskViewModel = new ToDoTaskViewModel()
            {
                Id = createdTask.Id,
                ModificationDateTimeOffset = createdTask.ModificationDateTimeOffset,
                CreationDateTimeOffset = createdTask.CreationDateTimeOffset,
                IsChecked = createdTask.IsChecked,
                Description = createdTask.Description
            };
            return PartialView("_TodoTaskViewModel", createdTaskViewModel);
        }
    }

    public class CreateTask
    {
        public Guid TaskId { get; set; }
        public string Description { get; set; }
    }
}
