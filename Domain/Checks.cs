using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    class Checks
    {
        [Key]
        public int id { get; set; }
        public Customer customer { get; set; }
        public string Memo { get; set; }

    }
}
