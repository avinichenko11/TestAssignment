using System.Collections.Generic;


namespace FareportalTestAssignment.Responses
{
    class TodosResponses
    {
        public List<Todo> TodosList { get; set; }
    }

    public class Todo
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public bool completed { get; set; }
    }
}

