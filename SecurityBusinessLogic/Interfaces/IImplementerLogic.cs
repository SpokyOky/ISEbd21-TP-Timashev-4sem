using SecurityBusinessLogic.BindingModels;
using SecurityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBusinessLogic.Interfaces
{
    public interface IImplementerLogic
    {
        List<ImplementerViewModel> Read(ImplementerBindingModel model);

        void CreateOrUpdate(ImplementerBindingModel model);

        void Delete(ImplementerBindingModel model);
    }
}
