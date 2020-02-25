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
    public class EquipmentLogic : IEquipmentLogic
    {
        private readonly DataListSingleton source;

        public EquipmentLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(EquipmentBindingModel model)
        {
            Equipment tempEquipment = model.Id.HasValue ? null : new Equipment { Id = 1 };
            foreach (var equipment in source.Equipments)
            {
                if (equipment.EquipmentName == model.EquipmentName && equipment.Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
                if (!model.Id.HasValue && equipment.Id >= tempEquipment.Id)
                {
                    tempEquipment.Id = equipment.Id + 1;
                }
                else if (model.Id.HasValue && equipment.Id == model.Id)
                {
                    tempEquipment = equipment;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempEquipment == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempEquipment);
            }
            else
            {
                source.Equipments.Add(CreateModel(model, tempEquipment));
            }
        }

        public void Delete(EquipmentBindingModel model)
        {
            // удаляем записи по компонентам при удалении изделия
            for (int i = 0; i < source.EquipmentRaws.Count; ++i)
            {
                if (source.EquipmentRaws[i].EquipmentId == model.Id)
                {
                    source.EquipmentRaws.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Equipments.Count; ++i)
            {
                if (source.Equipments[i].Id == model.Id)
                {
                    source.Equipments.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private Equipment CreateModel(EquipmentBindingModel model, Equipment equipment)
        {
            equipment.EquipmentName = model.EquipmentName;
            equipment.Cost = model.Cost;
            //обновляем существуюущие компоненты и ищем максимальный идентификатор
            int maxPCId = 0;
            for (int i = 0; i < source.EquipmentRaws.Count; ++i)
            {
                if (source.EquipmentRaws[i].Id > maxPCId)
                {
                    maxPCId = source.EquipmentRaws[i].Id;
                }
                if (source.EquipmentRaws[i].EquipmentId == equipment.Id)
                {
                    // если в модели пришла запись компонента с таким id
                    if
                    (model.EquipmentRaws.ContainsKey(source.EquipmentRaws[i].RawId))
                    {
                        // обновляем количество
                        source.EquipmentRaws[i].Count =
                        model.EquipmentRaws[source.EquipmentRaws[i].RawId].Item2;
                        // из модели убираем эту запись, чтобы остались только не
                        // просмотренные

                        model.EquipmentRaws.Remove(source.EquipmentRaws[i].RawId);
                    }
                    else
                    {
                        source.EquipmentRaws.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            foreach (var pc in model.EquipmentRaws)
            {
                source.EquipmentRaws.Add(new EquipmentRaw
                {
                    Id = ++maxPCId,
                    EquipmentId = equipment.Id,
                    RawId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
            return equipment;
        }

        public List<EquipmentViewModel> Read(EquipmentBindingModel model)
        {
            List<EquipmentViewModel> result = new List<EquipmentViewModel>();
            foreach (var raw in source.Equipments)
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

        private EquipmentViewModel CreateViewModel(Equipment equipment)
        {
            // требуется дополнительно получить список компонентов для изделия с
            // названиями и их количество
            Dictionary<int, (string, int)> equipmentRaws = new Dictionary<int,
    (string, int)>();
            foreach (var pc in source.EquipmentRaws)
            {
                if (pc.EquipmentId == equipment.Id)
                {
                    string rawName = string.Empty;
                    foreach (var raw in source.Raws)
                    {
                        if (pc.RawId == raw.Id)
                        {
                            rawName = raw.RawName;
                            break;
                        }
                    }
                    equipmentRaws.Add(pc.RawId, (rawName, pc.Count));
                }
            }
            return new EquipmentViewModel
            {
                Id = equipment.Id,
                EquipmentName = equipment.EquipmentName,
                Cost = equipment.Cost,
                EquipmentRaws = equipmentRaws
            };
        }
    }
}
