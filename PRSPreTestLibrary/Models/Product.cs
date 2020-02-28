using System;
using System.Collections.Generic;
using System.Text;

namespace PRSPreTestLibrary.Models
{
    public class Product {
        public int Id { get; set; }
        public string PartNbr { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public string PhotoPath { get; set; }
        public int VendorId { get; set; }

       public override string ToString() => $"{Id}/{PartNbr}/{Name}/{Price}/{Unit}/{PhotoPath}/{VendorId}"; //overrides

        public virtual List<RequestLine> Requestlines { get; set; }

        public virtual Vendor Vendor { get; set; } 
    }
}
