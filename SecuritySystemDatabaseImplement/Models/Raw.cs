using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemDatabaseImplement.Models
{
    public class Raw
    {
        public int Id { set; get; }

        [Required]
        public string RawName { get; set; }

        [ForeignKey("RawId")]
        public virtual List<EquipmentRaw> EquipmentRaws { get; set; }
    }
}
