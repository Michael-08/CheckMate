using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Repository.IRepository;

namespace ToDoList.Repository
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private readonly ApplicationDbContext _db;

        public AppUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
