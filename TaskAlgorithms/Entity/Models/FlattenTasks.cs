﻿
namespace NestedTaskFlattener.Entity.Models
{
    public class FlattenTasks
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
