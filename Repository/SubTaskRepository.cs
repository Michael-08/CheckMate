using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Repository.IRepository;

namespace ToDoList.Repository
{
    public class SubTaskRepository : Repository<SubTask>, ISubTaskRepository
    {
        private readonly ApplicationDbContext _db;
        public SubTaskRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Edit(SubTask obj)
        {
            var objFromDb = _db.SubTasks.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Body = obj.Body;
            }
        }
    }
}
