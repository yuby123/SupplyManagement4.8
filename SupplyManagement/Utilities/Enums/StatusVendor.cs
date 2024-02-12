using System.ComponentModel.DataAnnotations;

namespace SupplyManagement.Utilities.Enums
{
    public enum StatusVendor
    {
        [Display(Name = "Menunggu")]
        requested = 1,

        [Display(Name = "Approve by Admin")]
        approvedByAdmin = 2,
    }
}

