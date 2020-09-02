using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemDatabaseImplement.Models
{
    public class Equipment
    {
        public int Id { get; set; }

        [Required]
        public string EquipmentName { get; set; }

        [Required]
        public decimal Cost { get; set; }

        public virtual List<EquipmentRaw> EquipmentRaw { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
