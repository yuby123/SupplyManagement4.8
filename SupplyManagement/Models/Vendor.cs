using SupplyManagement.Utilities.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyManagement.Models
{
    [Table("tb_m_vendor")]
    public class Vendor : BaseEntity
    {
        [Column("bidang_usaha")]
        [MaxLength(100)]
        public string BidangUsaha { get; set; }

        [Column("jenis_perusahaan")]
        [MaxLength(50)]
        public string JenisPerusahaan { get; set; }

        [Column("status_vendor")]
        public StatusVendor StatusVendor { get; set; }

        [Column("company_guid")]
        public Guid CompanyGuid { get; set; }
        public Company Company { get; set; } = null;
    }
}
