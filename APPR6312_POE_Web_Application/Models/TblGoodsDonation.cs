using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace APPR6312_POE_Web_Application.Models
{
    public partial class TblGoodsDonation
    {
        public int GoodsId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Fullname")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Items")]
        public int NumberOfItems { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Name Of Good")]
        public string NameOfGood { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Captured By")]
        public string Username { get; set; }
    }
}
