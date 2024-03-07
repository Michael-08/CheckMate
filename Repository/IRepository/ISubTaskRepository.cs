using ToDoList.Models;

namespace ToDoList.Repository.IRepository
{
    public interface ISubTaskRepository : IRepository<SubTask>
    {
        public void Edit(SubTask obj);

    }
}
