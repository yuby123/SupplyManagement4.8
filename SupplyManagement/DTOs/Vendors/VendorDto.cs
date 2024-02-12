using SupplyManagement.Models;
using SupplyManagement.Utilities.Enums;
using System;

namespace API.DTOs.Vendors
{
    public class VendorDto
    {
        public Guid Guid { get; set; }
        public string BidangUsaha { get; set; }
        public string JenisPerusahaan { get; set; }
        public string Status { get; set; }

        public static explicit operator VendorDto(Vendor vendor)
        {
            return new VendorDto
            {
                Guid = vendor.Guid,
                BidangUsaha = vendor.BidangUsaha,
                JenisPerusahaan = vendor.JenisPerusahaan,
                Status = vendor.StatusVendor.ToString(),
            };
        }

        public static implicit operator Vendor(VendorDto vendorDto)
        {
            return new Vendor
            {
                Guid = vendorDto.Guid,
                BidangUsaha = vendorDto.BidangUsaha,
                JenisPerusahaan = vendorDto.JenisPerusahaan,
                StatusVendor = StatusVendor.requested,
            };
        }
    }
}
