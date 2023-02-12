using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
    [Index("CompanyName", Name = "CompanyNameSuppliers")]
    [Index("PostalCode", Name = "PostalCodeSuppliers")]
    public partial class Supplier
    {
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int SupplierId { get; set; }
        [StringLength(40)]
        [Column(TypeName = "nvarchar (40)")]
        [Required]
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
        [Column(TypeName = "ntext")]
        public string? HomePage { get; set; }

        [InverseProperty("Supplier")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
