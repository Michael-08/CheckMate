using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ToDoList.Models;
using ToDoList.Repository.IRepository;

namespace vFlow.Controllers
{
    public class SearchController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SearchController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(string searchString)
        {
            var claims = (ClaimsIdentity)User.Identity;
            var id = claims.FindFirst(ClaimTypes.NameIdentifier).Value;

            var matchedPosts = new List<Tuple<DoList, int>>();
            if (searchString == null)
            {
                return View(matchedPosts);
            }
            var temp = new List<DoList>();
            var lists = _unitOfWork.DoLists.GetAll(u => u.AppUserId == id || (u.Collaborators != null && u.Collaborators.Contains(id)));
            var searchWords = searchString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                           .Select(word => word.ToUpper())
                                           .ToList();

                foreach (var post in lists)
                {
                    var titleWords = post.Title.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                               .Select(word => word.ToUpper())
                                               .ToList();
                    var wordMatchCount = titleWords.Count(titleWord => searchWords.Contains(titleWord));
                    if (wordMatchCount > 0)
                    {
                        matchedPosts.Add(Tuple.Create(post, wordMatchCount));
                    }
                }

            matchedPosts = matchedPosts.OrderByDescending(tuple => tuple.Item2).ToList();
            TempData["success"] = "Results!";
            return View(matchedPosts);
        }
    }
}