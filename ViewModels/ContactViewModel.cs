using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DAW.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Name is required!")]
        [MinLength(5, ErrorMessage ="Name is too short.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email address is required!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MaxLength(300, ErrorMessage = "The message is too long. Try make it shorter of email us! :)")]
        public string Message { get; set; }

    }
}
