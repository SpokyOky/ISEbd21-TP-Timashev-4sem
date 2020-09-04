using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemListImplement.Models
{
    public class EquipmentRaw
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int RawId { get; set; }
        public int Count { get; set; }
    }
}
