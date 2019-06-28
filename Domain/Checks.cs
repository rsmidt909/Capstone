using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain
{
    public class Checks
    {
        [Key]
        public int id { get; set; }
        public Customer customer { get; set; }
        public string Memo { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationId { get; set; }

    }
}
