using ToDoList.Models;

namespace ToDoList.Models.ViewModels
{
    public class ListTask
    {
        public List<DoList> DoLists{ get; set; }
        public List<Tasks> Tasks { get; set; }

        public List<SubTask> SubTasks { get; set; }

        public List<AppUser> AppUsers { get; set; }

        public AppUser currUser { get; set; }

        public ListTask()
        {
            SubTasks = new List<SubTask>();
            DoLists = new List<DoList>();
            Tasks = new List<Tasks>();
        }
    }
}