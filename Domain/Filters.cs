using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain
{
    public class Filters
    {
        [Key]
        public int id { get; set; }
        public int NumberOfFilters { get; set; }
        public string SizeOfFilters { get; set; }

    }
}
