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
    public class EquipmentLogic : IEquipmentLogic
    {
        private readonly FileDataListSingleton source;

        public EquipmentLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(EquipmentBindingModel model)
        {
            Equipment element = source.Equipments.FirstOrDefault(rec => rec.EquipmentName ==
           model.EquipmentName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Equipments.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Equipments.Count > 0 ? source.Raws.Max(rec =>
               rec.Id) : 0;
                element = new Equipment { Id = maxId + 1 };
                source.Equipments.Add(element);
            }
            element.EquipmentName = model.EquipmentName;
            element.Cost = model.Cost;
            // удалили те, которых нет в модели
            source.EquipmentRaws.RemoveAll(rec => rec.EquipmentId == model.Id &&
           !model.EquipmentRaws.ContainsKey(rec.RawId));
            // обновили количество у существующих записей
            var updateRaws = source.EquipmentRaws.Where(rec => rec.EquipmentId ==
           model.Id && model.EquipmentRaws.ContainsKey(rec.RawId));
            foreach (var updateRaw in updateRaws)
            {
                updateRaw.Count =
               model.EquipmentRaws[updateRaw.RawId].Item2;
                model.EquipmentRaws.Remove(updateRaw.RawId);
            }
            // добавили новые
            int maxPCId = source.EquipmentRaws.Count > 0 ?
           source.EquipmentRaws.Max(rec => rec.Id) : 0;
            foreach (var pc in model.EquipmentRaws)
            {
                source.EquipmentRaws.Add(new EquipmentRaw
                {
                    Id = ++maxPCId,
                    EquipmentId = element.Id,
                    RawId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
        }

        public void Delete(EquipmentBindingModel model)
        {
            // удаяем записи по компонентам при удалении изделия
            source.EquipmentRaws.RemoveAll(rec => rec.EquipmentId == model.Id);
            Equipment element = source.Equipments.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Equipments.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public List<EquipmentViewModel> Read(EquipmentBindingModel model)
        {
            return source.Equipments
            .Where(rec => model == null || rec.Id == model.Id)
            .Select(rec => new EquipmentViewModel
            {
                Id = rec.Id,
                EquipmentName = rec.EquipmentName,
                Cost = rec.Cost,
                EquipmentRaws = source.EquipmentRaws
            .Where(recPC => recPC.EquipmentId == rec.Id)
           .ToDictionary(recPC => recPC.RawId, recPC =>
            (source.Raws.FirstOrDefault(recC => recC.Id ==
           recPC.RawId)?.RawName, recPC.Count))
            })
            .ToList();
        }
    }
}
