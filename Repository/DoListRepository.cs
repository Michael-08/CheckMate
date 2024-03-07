using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Repository.IRepository;

namespace ToDoList.Repository
{
    public class DoListRepository : Repository<DoList>, IDoListRepository
    {
        private readonly ApplicationDbContext _db;

        public DoListRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(DoList obj)
        {
            var objFromDb = _db.DoLists.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Body = obj.Body;
                objFromDb.Deadline = obj.Deadline;
            }

        }
    }
}
