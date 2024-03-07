using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Build.Framework;

namespace ToDoList.Models
{
    public class SubTask
    {
        public int Id { get; set; }
        [ValidateNever]
        [Required]
        public int AnsId { get; set; }
        [ForeignKey("AnswerId")]
        public DateOnly TimeStamp { get; set; }
        [Required]
        public DateOnly Deadline { get; set; }

        [Required]
        [ValidateNever]
        public string AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        public string Body { get; set; }

        public bool isCompleted { get; set; } = false;

    }
}
