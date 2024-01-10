using SupplyManagement.Utilities.Enums;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyManagement.Models
{
    [Table("tb_m_accounts")]
    public class Account : BaseEntity
    {
        [Column("password")]
        public string Password { get; set; }

        [Column("otp")]
        public int Otp { get; set; }

        [Column("status")]
        public StatusAccount Status { get; set; }

        [Column("is_used")]
        public bool IsUsed { get; set; }

        [Column("expired_time", TypeName = "datetime2")]
        public DateTime ExpiredTime { get; set; }

        [Column("role_guid")]
        public Guid RoleGuid { get; set; }

        // Kardinalitas
        public Company Company { get; set; } = null;
        public Role Role { get; set; } = null;
    }
}
