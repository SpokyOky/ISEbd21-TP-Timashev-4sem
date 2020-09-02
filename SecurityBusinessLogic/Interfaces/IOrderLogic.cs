using SecurityBusinessLogic.BindingModels;
using SecurityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBusinessLogic.Interfaces
{
    public interface IOrderLogic
    {
        List<OrderViewModel> Read(OrderBindingModel model);
        void CreateOrUpdate(OrderBindingModel model);
        void Delete(OrderBindingModel model);
    }
}
