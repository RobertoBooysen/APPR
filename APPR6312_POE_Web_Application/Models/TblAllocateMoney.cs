using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE_Web_Application.Models
{
    public partial class TblAllocateMoney
    {
        [Key]
        public int AllocateMoneyID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Disasters")]
        public string Disasters { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Amount")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }
    }
}
