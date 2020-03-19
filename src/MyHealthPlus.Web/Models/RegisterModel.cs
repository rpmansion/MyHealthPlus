using System.ComponentModel.DataAnnotations;

namespace MyHealthPlus.Web.Models
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        public string ComparePassword { get; set; }

        public string Contact { get; set; }
    }
}