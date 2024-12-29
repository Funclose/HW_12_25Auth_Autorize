using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace HW_12_25Auth_Autorize.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.EmailAddress)]

 
        public string? Hobby { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]        
        
        public string Password { get; set; }
        [ValidateNever]
        public DateTime CreatedAt { get; set; }
        [ValidateNever]

        public Role Role { get; set; }
    }
}
