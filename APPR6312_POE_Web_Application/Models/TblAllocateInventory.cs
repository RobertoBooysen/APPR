using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE_Web_Application.Models
{
    public partial class TblAllocateInventory
    {
        [Key]
        public int AllocateInventoryID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Disasters")]
        public string Disasters { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Number Of Goods")]
        public int NumberOfInventoryGoods { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Goods")]
        public string GoodsInventory { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }
    }
}
