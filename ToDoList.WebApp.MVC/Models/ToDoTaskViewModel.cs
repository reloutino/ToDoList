using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoList.WebApp.MVC.Models
{
    public class ToDoTaskViewModel
    {
        public Guid Id { get; set; }

        public DateTimeOffset CreationDateTimeOffset { get; set; }

        [DisplayName("Last Modification")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTimeOffset ModificationDateTimeOffset { get; set; }

        public bool IsChecked { get; set; }

        public string Description { get; set; }
    }
}
