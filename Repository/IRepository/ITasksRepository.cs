using ToDoList.Models;

namespace ToDoList.Repository.IRepository
{
    public interface ITasksRepository : IRepository<Tasks>
    {
        public void Edit(Tasks obj){
        } 
    }
}
