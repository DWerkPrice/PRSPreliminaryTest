using System;
using System.Collections.Generic;
using System.Text;

namespace PRSPreTestLibrary.Models
{
    public class Request {
        public int Id { get; set; }
        public string Description {get;set;}
        public string Justification { get; set; }
        public string RejectionReason { get; set; }
        public string DeliveryMode { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }

        internal virtual List<Requestline> Requestlines { get; set; }// could use ienumerable requestlines just has to be a list returned
        
        

        public Request() {

        }
    }
}
