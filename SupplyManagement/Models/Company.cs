using SupplyManagement.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyManagement.Models
{
    [Table("tb_m_company")]
    public class Company : BaseEntity
    {
        [Column("name")]
        [MaxLength(100)]
        public string Name { get; set; }

        [Column("address")]
        [MaxLength(100)]
        public string Address { get; set; }

        [Column("email")]
        [Index(IsUnique = true)]
        [MaxLength(50)]
        public string Email { get; set; }

        [Column("phone_number")]
        [Index(IsUnique = true)]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Column("foto")]
        public string Foto { get; set; } = null;

        public Account Account { get; set; } = null;
        public ICollection<Vendor> Vendors { get; set; }
    }
}
