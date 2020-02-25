using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBusinessLogic.BindingModels
{
    public class CreateOrderBindingModel
    {
        public int EquipmentId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
