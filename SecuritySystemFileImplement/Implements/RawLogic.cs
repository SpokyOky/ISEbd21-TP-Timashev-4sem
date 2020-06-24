using SecurityBusinessLogic.BindingModels;
using SecurityBusinessLogic.Interfaces;
using SecurityBusinessLogic.ViewModels;
using SecuritySystemFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemFileImplement.Implements
{
    public class RawLogic : IRawLogic
    {
        private readonly FileDataListSingleton source;
        public RawLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(RawBindingModel model)
        {
            Raw element = source.Raws.FirstOrDefault(rec => rec.RawName
           == model.RawName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Raws.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Raws.Count > 0 ? source.Raws.Max(rec =>
               rec.Id) : 0;
                element = new Raw { Id = maxId + 1 };
                source.Raws.Add(element);
            }
            element.RawName = model.RawName;
        }
        public void Delete(RawBindingModel model)
        {
            Raw element = source.Raws.FirstOrDefault(rec => rec.Id ==
           model.Id);
            if (element != null)
            {
                source.Raws.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
        public List<RawViewModel> Read(RawBindingModel model)
        {
            return source.Raws
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new RawViewModel
            {
                Id = rec.Id,
                RawName = rec.RawName
            })
            .ToList();
        }
    }
}
