using SecurityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBusinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }

        public string Title { get; set; }

        public List<EquipmentViewModel> Equipments { get; set; }
    }
}
