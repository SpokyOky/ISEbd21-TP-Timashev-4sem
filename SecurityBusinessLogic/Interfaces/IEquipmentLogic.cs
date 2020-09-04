using SecurityBusinessLogic.BindingModels;
using SecurityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBusinessLogic.Interfaces
{
    public interface IEquipmentLogic
    {
        List<EquipmentViewModel> Read(EquipmentBindingModel model);
        void CreateOrUpdate(EquipmentBindingModel model);
        void Delete(EquipmentBindingModel model);
    }
}
