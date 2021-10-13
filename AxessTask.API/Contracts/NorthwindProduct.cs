using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AxessTask.API.Contracts
{
    public class NorthwindProduct
    {
       
        public int id { get; set; }

        public int supplierId { get; set; }
        public NorthwindSupplier supplier { get; set; }
        public int categoryId { get; set; }
        public NorthwindCategory category { get; set; }
        public string quantityPerUnit { get; set; }

        public string unitPrice { get; set; }
        public int unitInStock { get; set; }
        public int unitsOnOrder { get; set; }
        public int reorderLevel { get; set; }
        public string discounted { get; set; }
        public string name { get; set; }
    }
}
