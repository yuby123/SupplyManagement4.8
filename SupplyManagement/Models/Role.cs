using SupplyManagement.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyManagement.Models
{
    [Table("tb_m_roles")]
    public class Role : BaseEntity
    {
        [Column("name")]
        [MaxLength(25)]
        public string Name { get; set; }

        // Kardinalitas
        public ICollection<Account> Accounts { get; set; } = null;
    }
}
