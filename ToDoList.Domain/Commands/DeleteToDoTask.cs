using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Domain.Commands
{
    public class DeleteToDoTask
    {
        public Guid UserId { get; set; }

        public Guid TaskId { get; set; }
    }
}
