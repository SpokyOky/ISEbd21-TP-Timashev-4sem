using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SecurityBusinessLogic.ViewModels
{
    public class EquipmentViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название изделия")]
        public string EquipmentName { get; set; }

        [DisplayName("Цена")]
        public decimal Cost { get; set; }

        public Dictionary<int, (string, int)> EquipmentRaws { get; set; }
    }
}
