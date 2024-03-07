using ToDoList.Models;

namespace ToDoList.Repository.IRepository
{
    public interface IDoListRepository : IRepository<DoList>
    {
        void Update(DoList obj);
    }
}
