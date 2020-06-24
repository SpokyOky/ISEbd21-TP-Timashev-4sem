using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecuritySystemRestApi.Models
{
    public class EquipmentModel
    {
        public int Id { get; set; }

        public string EquipmentName { get; set; }

        public decimal Cost { get; set; }
    }
}
