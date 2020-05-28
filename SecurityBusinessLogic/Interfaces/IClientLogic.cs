using SecurityBusinessLogic.BindingModels;
using SecurityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBusinessLogic.Interfaces
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);

        void CreateOrUpdate(ClientBindingModel model);

        void Delete(ClientBindingModel model);
    }
}
