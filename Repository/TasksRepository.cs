using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Repository.IRepository;

namespace ToDoList.Repository
{
    public class TasksRepository : Repository<Tasks>, ITasksRepository
    {
        private readonly ApplicationDbContext _db;
        public TasksRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Edit(Tasks obj)
        {
            var objFromDb = _db.Tasks.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Body = obj.Body;
            }
        }
    }
}
