using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemDatabaseImplement.Models
{
    public class EquipmentRaw
    {
        public int Id { get; set; }

        public int EquipmentId { get; set; }

        public int RawId { get; set; }

        [Required]
        public int Count { get; set; }

        public virtual Raw Raw { get; set; }

        public virtual Equipment Equipment { get; set; }
    }
}
