using SecurityBusinessLogic.Attributes;
using SecurityBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace SecurityBusinessLogic.ViewModels
{
    [DataContract]
    public class EquipmentViewModel : BaseViewModel
    {
        [Column(title: "Название изделия", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DataMember]
        public string EquipmentName { get; set; }

        [Column(title: "Цена", width: 50)]
        [DataMember]
        public decimal Cost { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> EquipmentRaws { get; set; }

        public override List<string> Properties() => new List<string>
        {
            "Id",
            "EquipmentName",
            "Price"
        };
    }
}
