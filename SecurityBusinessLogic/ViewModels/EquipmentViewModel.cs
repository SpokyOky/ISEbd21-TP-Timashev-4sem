using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace SecurityBusinessLogic.ViewModels
{
    [DataContract]
    public class EquipmentViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название изделия")]
        public string EquipmentName { get; set; }

        [DataMember]
        [DisplayName("Цена")]
        public decimal Cost { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> EquipmentRaws { get; set; }
    }
}
