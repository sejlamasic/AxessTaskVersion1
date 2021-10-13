using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxessTask.API.Contracts
{
    public class NorthwindSupplier
    {
        public int id { get; set; }

        public string companyName { get; set; }
        public string contactName { get; set; }
        public string contactTitle { get; set; }

    }
}
