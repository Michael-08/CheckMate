using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using ToDoList.Models;
using ToDoList.Models.ViewModels;
using ToDoList.Repository;
using ToDoList.Repository.IRepository;

namespace ToDoList.Controllers
{
    [Authorize]
    public class DoListController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public DoListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claims = (ClaimsIdentity)User.Identity;
            var id = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

            ListTask listTask = new ListTask();
            listTask.DoLists = _unitOfWork.DoLists.GetAll(u => u.AppUserId == id).ToList();
            listTask.Tasks = (List<Tasks>)(_unitOfWork.Tasks.GetAll());
            listTask.AppUsers = (List<AppUser>)_unitOfWork.AppUsers.GetAll();

            listTask.DoLists = listTask.DoLists.OrderBy(p => p.Deadline).ToList();
            listTask.SubTasks = listTask.SubTasks.OrderBy(p => p.Deadline).ToList();
            listTask.Tasks = listTask.Tasks.OrderBy(p=>p.Deadline).ToList();

            return View(listTask);
        }

        public IActionResult CreateDoList(DoList doList)
        {
            if (doList == null)
            {
                doList = new DoList();
            }
            return View(doList);
        }

        [HttpPost]
        public IActionResult CreateDoListPost(DoList doList)
        {
            var claims = (ClaimsIdentity)User.Identity;
            var id = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

            doList.AppUserId = id;
            doList.TimeStamp = DateOnly.FromDateTime(DateTime.Now);

            bool isPresent = _unitOfWork.DoLists.Get(u => u.Id == doList.Id) != null;
            var curr_post = _unitOfWork.DoLists.Get(u => u.Id == doList.Id);

            if (!isPresent)
            {
                _unitOfWork.DoLists.Add(doList);
            }
            else
            {
                _unitOfWork.DoLists.Update(doList);
            }
            _unitOfWork.Save();
            return RedirectToAction("Index");

        }
        public IActionResult UpdateDoList(int doListId)
        {
            var claims = (ClaimsIdentity)User.Identity;
            var id = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

            var obj = _unitOfWork.DoLists.Get(u => u.Id == doListId);
            if (obj.AppUserId != id)
            {
                return Forbid();
            }
            return RedirectToAction("CreateDoList", obj);

        }
        [HttpPost]
        public IActionResult DeleteDoList(int doListId)
        {
            var claims = (ClaimsIdentity)User.Identity;
            var id = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

            var obj = _unitOfWork.DoLists.Get(u => u.Id == doListId);
            if (obj.AppUserId != id)
            {
                return Forbid(); // Return 403 Forbidden if the user is not authorized
            }
            _unitOfWork.DoLists.Remove(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult AnsPost(int id)
        {
            ListTask listTask = new ListTask();
            DoList currPost = _unitOfWork.DoLists.Get(u => u.Id == id);
            listTask.DoLists = new List<DoList>() { currPost };
            listTask.Tasks = (List<Tasks>)_unitOfWork.Tasks.GetAll(u => u.PostId == id);
            listTask.SubTasks = (List<SubTask>)_unitOfWork.SubTasks.GetAll();
            listTask.AppUsers = (List<AppUser>)_unitOfWork.AppUsers.GetAll();


            listTask.DoLists = listTask.DoLists.OrderBy(p => p.Deadline).ToList();
            listTask.Tasks = listTask.Tasks.OrderBy(task => task.isCompleted)
                .ThenBy(task => task.Deadline)
                .ToList();

            listTask.SubTasks = listTask.SubTasks.OrderBy(p => p.Deadline).ToList();

            
            var claims = (ClaimsIdentity)User.Identity;
            var currUserid = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

            listTask.currUser = _unitOfWork.AppUsers.Get(u => u.Id == currUserid);

            return View(listTask);
        }

        [HttpPost]
        public IActionResult AddAns(int postId, string answerText,DateOnly deadline)
        {

            if (answerText != null)
            {

                Tasks task = new Tasks();
                var claims = (ClaimsIdentity)User.Identity;
                var Userid = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

                task.AppUserId = Userid;
                task.Deadline = deadline;
                task.TimeStamp = DateOnly.FromDateTime(DateTime.Now);
                task.PostId = postId;
                task.Body = answerText;

                _unitOfWork.Tasks.Add(task);
                _unitOfWork.Save();
            }
            return RedirectToAction("AnsPost", new { id = postId });
        }

        
        public IActionResult EditTask(int taskId)
        {
            var claims = (ClaimsIdentity)User.Identity;
            var id = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            var obj = _unitOfWork.Tasks.Get(u => u.Id == taskId);
            if (obj.AppUserId != id)
            {
                return Forbid(); // Return 403 Forbidden if the user is not authorized
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult EditTask(int Id, string AnsBody, DateOnly deadline)
        {

            var obj = _unitOfWork.Tasks.Get(u => u.Id == Id);
            obj.Body = AnsBody;
            obj.Deadline = deadline;

            _unitOfWork.Tasks.Edit(obj);
            _unitOfWork.Save();
            return RedirectToAction("AnsPost", new { id = obj.PostId });
        }
        [HttpPost]
        public IActionResult DeleteTask(int taskId, int doListId)
        {
            var obj = _unitOfWork.Tasks.Get(u => u.Id == taskId);
            var claims = (ClaimsIdentity)User.Identity;
            var id = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (obj.AppUserId != id)
            {
                return Forbid(); // Return 403 Forbidden if the user is not authorized
            }
            _unitOfWork.Tasks.Remove(obj);
            _unitOfWork.Save();
            return RedirectToAction("AnsPost", new { id = doListId });
        }


        [HttpPost]
        public IActionResult AddSubTask(int taskId, string commentText, int doListId,DateOnly deadline)
        {
            if (commentText != null)
            {
                SubTask subTask = new SubTask();
                var claims = (ClaimsIdentity)User.Identity;
                var Userid = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

                subTask.AppUserId = Userid;
                subTask.TimeStamp = DateOnly.FromDateTime(DateTime.Now);
                subTask.AnsId = taskId;
                subTask.Body = commentText;
                subTask.Deadline = deadline;

                _unitOfWork.SubTasks.Add(subTask);
                _unitOfWork.Save();
            }

            //return Json(comment)
            return RedirectToAction("AnsPost", new { id = doListId });
        }
        [HttpPost]
        public IActionResult EditSubTask(int doListId, int subTaskId, string commentText,DateOnly deadline)
        {
            SubTask obj = _unitOfWork.SubTasks.Get(u => u.Id == subTaskId);
            var claims = (ClaimsIdentity)User.Identity;
            var id = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (obj.AppUserId != id)
            {
                return Forbid(); // Return 403 Forbidden if the user is not authorized
            }
            if (obj == null)
            {
                return NotFound();
            }
            obj.Body = commentText;
            obj.Deadline = deadline;

            _unitOfWork.SubTasks.Edit(obj);
            _unitOfWork.Save();

            return RedirectToAction("AnsPost", new { id = doListId });
        }
        [HttpPost]
        public IActionResult DeleteSubTask(int subTaskId, int doListId)
        {
            var obj = _unitOfWork.SubTasks.Get(u => u.Id == subTaskId);
            var claims = (ClaimsIdentity)User.Identity;
            var id = claims.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (obj.AppUserId != id)
            {
                return Forbid(); // Return 403 Forbidden if the user is not authorized
            }
            _unitOfWork.SubTasks.Remove(obj);
            _unitOfWork.Save();
            return RedirectToAction("AnsPost", new { id = doListId });
        }



        [HttpPost]
        public IActionResult Checkbox(int taskId,bool doneOrNot,int doListId)
        {
            Tasks task = _unitOfWork.Tasks.Get(u => u.Id == taskId);
            task.isCompleted = !doneOrNot;
            _unitOfWork.Save();
            return RedirectToAction("AnsPost", new { id = doListId });
        }

    }
}
