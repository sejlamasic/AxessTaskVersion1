using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxessTask.API.Contracts
{
    public class NorthwindOrder
    {
        public int id { get; set; }
        public string customerId { get; set; }
        public int employeeId { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime requiredDate { get; set; }
        public string shippedDate { get; set; }
        public int shipVia { get; set; }
        public double freight { get; set; }
        public string shipName { get; set; }

    }
}
