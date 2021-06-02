using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace krosas_task.Models
{
    public class zamestnanec
    {
        public long id { get; set; }
        public string titul { get; set; }
        public string meno { get; set; }
        public string priezvisko { get; set; }
        public string telefon { get; set; }
        public string email { get; set; }

    }
}
