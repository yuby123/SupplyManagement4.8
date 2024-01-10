using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyManagement.Models
{
    public class BaseEntity
    {
        [Key, Column("guid")]
        public Guid Guid { get; set; }
    }
}
