using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace APPR6312_POE_Web_Application.Models
{
    public partial class TblDisaster
    {
        public int DisasterId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Location")]
        public string Location { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Name Of Disaster")]
        public string NameOfDisaster { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Allocated Goods")]
        public string AllocatedGoods { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Allocated Money")]
        public int AllocatedMoney { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Status")]
        public string Status { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Captured By")]
        public string Username { get; set; }
    }
}
