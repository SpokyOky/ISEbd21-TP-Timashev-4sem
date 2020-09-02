using SecurityBusinessLogic.BindingModels;
using SecurityBusinessLogic.Interfaces;
using SecurityBusinessLogic.ViewModels;
using SecuritySystemListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemListImplement.Implements
{
    public class RawLogic : IRawLogic
    {
        private readonly DataListSingleton source;

        public RawLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(RawBindingModel model)
        {
            Raw tempRaw = model.Id.HasValue ? null : new Raw
            {
                Id = 1
            };
            foreach (var raw in source.Raws)
            {
                if (raw.RawName == model.RawName && raw.Id !=
               model.Id)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
                if (!model.Id.HasValue && raw.Id >= tempRaw.Id)
                {
                    tempRaw.Id = raw.Id + 1;
                }
                else if (model.Id.HasValue && raw.Id == model.Id)
                {
                    tempRaw = raw;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempRaw == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempRaw);
            }
            else
            {
                source.Raws.Add(CreateModel(model, tempRaw));
            }
        }

        public void Delete(RawBindingModel model)
        {
            for (int i = 0; i < source.Raws.Count; ++i)
            {
                if (source.Raws[i].Id == model.Id.Value)
                {
                    source.Raws.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public List<RawViewModel> Read(RawBindingModel model)
        {
            List<RawViewModel> result = new List<RawViewModel>();
            foreach (var raw in source.Raws)
            {
                if (model != null)
                {
                    if (raw.Id == model.Id)
                    {
                        result.Add(CreateViewModel(raw));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(raw));
            }
            return result;
        }

        private Raw CreateModel(RawBindingModel model, Raw raw)
        {
            raw.RawName = model.RawName;
            return raw;
        }

        private RawViewModel CreateViewModel(Raw raw)
        {
            return new RawViewModel
            {
                Id = raw.Id,
                RawName = raw.RawName
            };
        }
    }
}
