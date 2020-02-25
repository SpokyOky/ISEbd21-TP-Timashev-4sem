using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBusinessLogic.BindingModels
{
    public class EquipmentRawBindingModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int RawId { get; set; }
        public int Count { get; set; }
    }
}
