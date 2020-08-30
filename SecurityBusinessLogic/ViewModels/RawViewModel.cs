using SecurityBusinessLogic.Attributes;
using SecurityBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SecurityBusinessLogic.ViewModels
{
    public class RawViewModel : BaseViewModel
    {
        [Column(title: "Компонент", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string RawName { get; set; }

        public override List<string> Properties() => new List<string>
        {
            "Id",
            "RawName"
        };
    }
}
