using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class ServiceCalls
    {
        [Key]
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Phone { get; set; }
        public string Address { get; set; }
        public string Memo { get; set; }
        public int NumberOfSystems { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }
        public bool Completed { get; set; }
    }
}
