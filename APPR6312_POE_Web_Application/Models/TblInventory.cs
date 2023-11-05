using System.ComponentModel.DataAnnotations;

namespace APPR6312_POE_Web_Application.Models
{
    public partial class TblInventory
    {
        [Key]
        public int InventoryID { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Goods")]
        public string GoodsInventory { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Purchased Amount")]
        public int PurchasedAmount { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Number Of Inventory Purchased")]
        public int numberOfInventoryPurchased { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Available Inventory")]
        public int NumberOfInventoryGoods { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Username")]
        public string Username { get; set; }
    }
}
