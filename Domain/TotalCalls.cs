using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TotalCalls
    {
        public IEnumerable<Checks> Checks { get; set; }
        public IEnumerable<ServiceCalls> ServiceCalls { get; set; }
        public IEnumerable<Customer> Customer { get; set; }
        
    }
}
