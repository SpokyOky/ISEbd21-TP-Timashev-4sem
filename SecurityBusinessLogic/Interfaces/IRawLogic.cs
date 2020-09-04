using SecurityBusinessLogic.BindingModels;
using SecurityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBusinessLogic.Interfaces
{
    public interface IRawLogic
    {
        List<RawViewModel> Read(RawBindingModel model);
        void CreateOrUpdate(RawBindingModel model);
        void Delete(RawBindingModel model);
    }
}
