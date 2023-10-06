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
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Amount")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Captured By")]
        public string Username { get; set; }
    }
}
