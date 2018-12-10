using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoList.Domain.Commands
{
    public class UpdateToDoTask
    {
        public Guid UserId { get; set; }

        public Guid TaskId { get; set; }

        public string Description { get; set; }

        public bool? IsChecked { get; set; }
    }
}
