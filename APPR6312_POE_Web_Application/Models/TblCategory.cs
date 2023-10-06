using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace APPR6312_POE_Web_Application.Models
{
    public partial class TblCategory
    {
        public int CategoriesId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Category Name")]
        public string CategoryNames { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Captured By")]
        public string Username { get; set; }
    }
}
