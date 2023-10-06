using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace APPR6312_POE_Web_Application.Models
{
    public partial class TblAdmin
    {
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
