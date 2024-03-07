using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IDoListRepository DoLists { get; }
        ITasksRepository Tasks { get; }

        IAppUserRepository AppUsers { get; }

        ISubTaskRepository SubTasks { get; }
        void Save();
    }
}
