using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using ToDoList.Domain.Commands;
using Xunit;

namespace ToDoList.Domain.UnitTests
{
    public class ToDoListServiceTests
    {
        [Fact]
        public async void GetById_OK()
        {
            var mockedRepositoru = new Mock<IToDoListRepository>();
            var userId = Guid.NewGuid();
            var taskId = Guid.NewGuid();
            mockedRepositoru.Setup(x => x.GetById(taskId)).Returns(Task.FromResult(new ToDoTask() { Id = taskId, UserId = userId }) );
            var todoListService = new ToDoListService(mockedRepositoru.Object);

            var result = await todoListService.GetById(userId, taskId);
            Assert.NotNull(result);
        }

        [Fact]
        public async void GetById_throwsIfWrongUser()
        {
            var mockedRepositoru = new Mock<IToDoListRepository>();
            var userId = Guid.NewGuid();
            var taskId = Guid.NewGuid();
            mockedRepositoru.Setup(x => x.GetById(taskId)).Returns(Task.FromResult(new ToDoTask() { Id = taskId, UserId = Guid.NewGuid() }));
            var todoListService = new ToDoListService(mockedRepositoru.Object);

            await Assert.ThrowsAsync<Exception>(() => todoListService.GetById(userId, taskId));
        }

        [Fact]
        public async void GetById_throwsIfdoesNotExist()
        {
            var mockedRepositoru = new Mock<IToDoListRepository>();
            var userId = Guid.NewGuid();
            var taskId = Guid.NewGuid();
            mockedRepositoru.Setup(x => x.GetById(taskId)).Returns(Task.FromResult(null as ToDoTask));
            var todoListService = new ToDoListService(mockedRepositoru.Object);

            await Assert.ThrowsAsync<Exception>(() => todoListService.GetById(userId, taskId));
        }

        [Fact]
        public async void Create_OK()
        {
            var mockedRepositoru = new Mock<IToDoListRepository>();
            var userId = Guid.NewGuid();
            var taskId = Guid.NewGuid();
            mockedRepositoru.Setup(x => x.GetById(taskId)).Returns(Task.FromResult(null as ToDoTask));
            var todoListService = new ToDoListService(mockedRepositoru.Object);

            await todoListService.CreateTask(new CreateNewToDoTask() {TaskId = taskId, UserId = userId});
            mockedRepositoru.Verify(x => x.Create(It.IsAny<ToDoTask>()), Times.Once);
        }

        [Fact]
        public void Create_throwsIfIdExists()
        {
            var mockedRepositoru = new Mock<IToDoListRepository>();
            var userId = Guid.NewGuid();
            var taskId = Guid.NewGuid();
            mockedRepositoru.Setup(x => x.GetById(taskId)).Returns(Task.FromResult(new ToDoTask() { Id = taskId, UserId = userId }));
            var todoListService = new ToDoListService(mockedRepositoru.Object);

            Assert.ThrowsAsync<Exception>(() => todoListService.GetById(userId, taskId));
        }

        [Fact]
        public async void Update_OK()
        {
            var mockedRepositoru = new Mock<IToDoListRepository>();
            var userId = Guid.NewGuid();
            var taskId = Guid.NewGuid();
            mockedRepositoru.Setup(x => x.GetById(taskId)).Returns(Task.FromResult(new ToDoTask() { Id = taskId, UserId = userId }));
            var todoListService = new ToDoListService(mockedRepositoru.Object);

            await todoListService.UpdateTask(new UpdateToDoTask { UserId = userId, TaskId = taskId });
            mockedRepositoru.Verify(x => x.Update(It.IsAny<ToDoTask>()), Times.Once);
        }

        [Fact]
        public async void Update_throwsIfWrongUser()
        {
            var mockedRepositoru = new Mock<IToDoListRepository>();
            var userId = Guid.NewGuid();
            var taskId = Guid.NewGuid();
            mockedRepositoru.Setup(x => x.GetById(taskId)).Returns(Task.FromResult(new ToDoTask() { Id = taskId, UserId = Guid.NewGuid() }));
            var todoListService = new ToDoListService(mockedRepositoru.Object);

            await Assert.ThrowsAsync<Exception>(() => todoListService.UpdateTask(new UpdateToDoTask { UserId = userId, TaskId = taskId }));
        }

        [Fact]
        public async void Update_throwsIfdoesNotExist()
        {
            var mockedRepositoru = new Mock<IToDoListRepository>();
            var userId = Guid.NewGuid();
            var taskId = Guid.NewGuid();
            mockedRepositoru.Setup(x => x.GetById(taskId)).Returns(Task.FromResult(null as ToDoTask));
            var todoListService = new ToDoListService(mockedRepositoru.Object);

            await Assert.ThrowsAsync<Exception>(() => todoListService.UpdateTask(new UpdateToDoTask { UserId = userId, TaskId = taskId }));
        }

        [Fact]
        public async void Delete_OK()
        {
            var mockedRepositoru = new Mock<IToDoListRepository>();
            var userId = Guid.NewGuid();
            var taskId = Guid.NewGuid();
            mockedRepositoru.Setup(x => x.GetById(taskId)).Returns(Task.FromResult(new ToDoTask() { Id = taskId, UserId = userId }));
            var todoListService = new ToDoListService(mockedRepositoru.Object);

            await todoListService.DeleteTask(new DeleteToDoTask { UserId = userId, TaskId = taskId });
            mockedRepositoru.Verify(x => x.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void Delete_throwsIfWrongUser()
        {
            var mockedRepositoru = new Mock<IToDoListRepository>();
            var userId = Guid.NewGuid();
            var taskId = Guid.NewGuid();
            mockedRepositoru.Setup(x => x.GetById(taskId)).Returns(Task.FromResult(new ToDoTask() { Id = taskId, UserId = Guid.NewGuid() }));
            var todoListService = new ToDoListService(mockedRepositoru.Object);

            await Assert.ThrowsAsync<Exception>(() => todoListService.DeleteTask(new DeleteToDoTask() { UserId = userId, TaskId = taskId }));
        }

        [Fact]
        public async void Delete_throwsIfdoesNotExist()
        {
            var mockedRepositoru = new Mock<IToDoListRepository>();
            var userId = Guid.NewGuid();
            var taskId = Guid.NewGuid();
            mockedRepositoru.Setup(x => x.GetById(taskId)).Returns(Task.FromResult(null as ToDoTask));
            var todoListService = new ToDoListService(mockedRepositoru.Object);

            await Assert.ThrowsAsync<Exception>(() => todoListService.DeleteTask(new DeleteToDoTask() { UserId = userId, TaskId = taskId }));
        }
    }
}
