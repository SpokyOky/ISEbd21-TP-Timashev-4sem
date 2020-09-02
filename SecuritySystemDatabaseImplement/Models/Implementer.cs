using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemDatabaseImplement.Models
{
    public class Implementer
    {
        public int Id { get; set; }

        public string ImplementerFIO { get; set; }

        public int WorkingTime { get; set; }

        public int PauseTime { get; set; }

        public List<Order> Orders { get; set; }
    }
}
