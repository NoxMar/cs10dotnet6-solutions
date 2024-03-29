﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Packt.Shared
{
    public partial class Shipper
    {
        public Shipper()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int ShipperId { get; set; }
        [StringLength(40)]
        [Column(TypeName = "nvarchar (40)")]
        [Required]
        public string CompanyName { get; set; } = null!;
        [StringLength(24)]
        [Column(TypeName = "nvarchar (24)")]
        public string? Phone { get; set; }

        [InverseProperty("ShipViaNavigation")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
