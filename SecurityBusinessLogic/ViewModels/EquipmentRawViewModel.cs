using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SecurityBusinessLogic.ViewModels
{
    public class EquipmentRawViewModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int RawId { get; set; }

        [DisplayName("Компонент")]
        public string RawName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
