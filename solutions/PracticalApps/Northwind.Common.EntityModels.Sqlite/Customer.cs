using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
    [Index("City", Name = "City")]
    [Index("CompanyName", Name = "CompanyNameCustomers")]
    [Index("PostalCode", Name = "PostalCodeCustomers")]
    [Index("Region", Name = "Region")]
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        [StringLength(5)]
        [Column(TypeName = "nvarchar (5)")]
        [RegularExpression("[A-Z]{5}")]
        public string CustomerId { get; set; } = null!;
        [StringLength(40)]
        [Column(TypeName = "nvarchar (40)")]
        public string CompanyName { get; set; } = null!;
        [StringLength(30)]
        [Column(TypeName = "nvarchar (30)")]
        public string? ContactName { get; set; }
        [StringLength(30)]
        [Column(TypeName = "nvarchar (30)")]
        public string? ContactTitle { get; set; }
        [StringLength(60)]
        [Column(TypeName = "nvarchar (60)")]
        public string? Address { get; set; }
        [StringLength(15)]
        [Column(TypeName = "nvarchar (15)")]
        public string? City { get; set; }
        [StringLength(15)]
        [Column(TypeName = "nvarchar (15)")]
        public string? Region { get; set; }
        [StringLength(10)]
        [Column(TypeName = "nvarchar (10)")]
        public string? PostalCode { get; set; }
        [StringLength(15)]
        [Column(TypeName = "nvarchar (15)")]
        public string? Country { get; set; }
        [StringLength(24)]
        [Column(TypeName = "nvarchar (24)")]
        public string? Phone { get; set; }
        [StringLength(24)]
        [Column(TypeName = "nvarchar (24)")]
        public string? Fax { get; set; }

        [InverseProperty("Customer")]
        [XmlIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
