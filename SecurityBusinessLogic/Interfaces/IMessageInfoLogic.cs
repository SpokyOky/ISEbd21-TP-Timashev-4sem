using SecurityBusinessLogic.BindingModels;
using SecurityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBusinessLogic.Interfaces
{
    public interface IMessageInfoLogic
    {
        List<MessageInfoViewModel> Read(MessageInfoBindingModel model);

        void Create(MessageInfoBindingModel model);
    }
}
