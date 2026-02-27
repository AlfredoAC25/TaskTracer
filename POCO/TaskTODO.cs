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

        public override string ToString()
        {
            return $"ID: {id} | Description: {description} | Status: {status} | CreatedAt: {createdAt} | UpdatedAt: {updatedAt}";
        }
    }
}
