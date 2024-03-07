using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }

    }
}