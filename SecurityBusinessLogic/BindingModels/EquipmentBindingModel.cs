using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBusinessLogic.BindingModels
{
    public class EquipmentBindingModel
    {
        public int? Id { get; set; }
        public string EquipmentName { get; set; }
        public decimal Cost { get; set; }
        public Dictionary<int, (string, int)> EquipmentRaws { get; set; }
    }
}
