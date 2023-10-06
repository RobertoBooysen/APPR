using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace APPR6312_POE_Web_Application.Models
{
    public partial class TblMonetaryDonation
    {
        public int DonationId { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Fullname")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Amount")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Total Received")]
        public int TotalReceived { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Captured By")]
        public string Username { get; set; }
    }
}
