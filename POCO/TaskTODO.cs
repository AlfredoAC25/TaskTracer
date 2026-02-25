using System;
using System.Collections.Generic;
using System.Text;
using scl.Enums;

namespace scl.POCO
{
    internal class TaskTODO
    {
        public int id { get; set; }
        public string description { get; set; }
        public TaskStatusTodo status { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
