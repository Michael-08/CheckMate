using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace ToDoList.Models
{
    public class Tasks
    {
        public int Id { get; set; }
        [Required]
        [ValidateNever]
        public int PostId { get; set; }
        [ForeignKey("PostId")]
        public DateOnly TimeStamp { get; set; }
        [Required]
        public DateOnly Deadline { get; set; }
        [Required]
        [ValidateNever]
        public string AppUserId { get; set; }
        [ForeignKey("AppUserId")]

        [Required]
        public string Body { get; set; }
        public bool isCompleted { get; set; } = false;
    }
}
