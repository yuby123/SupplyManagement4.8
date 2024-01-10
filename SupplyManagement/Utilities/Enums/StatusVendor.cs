using System.ComponentModel.DataAnnotations;

namespace SupplyManagement.Utilities.Enums
{
    public enum StatusVendor
    {
        [Display(Name = "Tidak Terdaftar")]
        none = 0,

        [Display(Name = "Menunggu")]
        waiting = 1,

        [Display(Name = "Approve by Admin")]
        approvedByAdmin = 2,
    }
}

