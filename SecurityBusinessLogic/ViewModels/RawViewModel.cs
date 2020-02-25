using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SecurityBusinessLogic.ViewModels
{
    public class RawViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название компонента")]
        public string RawName { get; set; }
    }
}
