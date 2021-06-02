using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace krosas_task.Models
{
    public class firma
    {
        public long id { get; set; }

        public string nazov { get; set; }
        public string kod { get; set; }
        public long veduciID { get; set; }
    }
}
