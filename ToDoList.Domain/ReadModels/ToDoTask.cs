using System;

namespace ToDoList.Domain
{
    public class ToDoTask
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string Description { get; set; }

        public DateTimeOffset CreationDateTimeOffset { get; set; }

        public DateTimeOffset ModificationDateTimeOffset { get; set; }

        public bool IsChecked { get; set; }
    }
}
