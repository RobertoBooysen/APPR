using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE_Web_Application.Models
{
    public partial class TblAllocateGoods
    {
        [Key]
        public int AllocateGoodsID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Disasters")]
        public string Disasters { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Goods")]
        public string Goods { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Number Of Goods")]
        public int NumberOfGoods { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }
    }
}
