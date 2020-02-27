using System;
using System.Collections.Generic;
using System.Text;

namespace PRSPreTestLibrary.Models
{
    public class Requestline //set the initial parameters for Requestline table handled detailed formats assigned in AppDbContext.cs
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Request Request { get; set; }
        public virtual Product Product { get; set; }
    }
}
