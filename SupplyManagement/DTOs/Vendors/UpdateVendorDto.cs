using SupplyManagement.Models;
using SupplyManagement.Utilities.Enums;
using System;

namespace API.DTOs.Vendors
{
    public class UpdateVendorDto
    {
        public Guid Guid { get; set; }
        public StatusVendor StatusVendor { get; set; }

        public static implicit operator Vendor(UpdateVendorDto createDto)
        {
            return new Vendor
            {
                Guid = createDto.Guid,
                StatusVendor = createDto.StatusVendor,
            };
        }
    }
}
