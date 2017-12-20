using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleTodo
{
    public class TodoModel
    {
        public int RecID { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string CreateTime { get; set; }
        public string CompletedTime { get; set; }
        public bool IsCompleted { get; set; }
    }
    public class TodoList
    {
        public List<TodoModel> DataList { get; set; }
    }
}
