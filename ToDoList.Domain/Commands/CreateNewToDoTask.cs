using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Domain.Commands
{
    public class CreateNewToDoTask
    {
        public Guid UserId { get; set; }

        public Guid TaskId { get; set; }

        public string Description { get; set; }
    }
}
