using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace ToDoList.Models
{
    public class DoList
    {

        public int Id { get; set; }
        [ValidateNever]
        public string AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public DateOnly TimeStamp { get; set; }
        [Required]
        public DateOnly Deadline { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public List<string>? Collaborators { get; set; }
        public bool isCompleted { get; set; } = false;
    }
}
