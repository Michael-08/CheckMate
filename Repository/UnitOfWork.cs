using ToDoList.Data;
using ToDoList.Repository;
using ToDoList.Repository.IRepository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ToDoList.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public IDoListRepository DoLists { get; private set; }
        public ITasksRepository Tasks {  get; private set; }

        public IAppUserRepository AppUsers { get; private set; }
        public ISubTaskRepository SubTasks { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            DoLists = new DoListRepository(_db);
            Tasks = new TasksRepository(_db);
            SubTasks = new SubTaskRepository(_db);
            AppUsers = new AppUserRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
