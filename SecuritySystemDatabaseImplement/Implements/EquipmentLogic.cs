using Microsoft.EntityFrameworkCore;
using SecurityBusinessLogic.BindingModels;
using SecurityBusinessLogic.Interfaces;
using SecurityBusinessLogic.ViewModels;
using SecuritySystemDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecuritySystemDatabaseImplement.Implements
{
    public class EquipmentLogic : IEquipmentLogic
    {
        public void CreateOrUpdate(EquipmentBindingModel model)
        {
            using (var context = new SecuritySystemDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Equipment element = context.Equipments.FirstOrDefault(rec => rec.EquipmentName == model.EquipmentName && rec.Id != model.Id);

                        if (element != null)
                        {
                            throw new Exception("Уже есть изделие с таким названием");
                        }

                        if (model.Id.HasValue)
                        {
                            element = context.Equipments.FirstOrDefault(rec => rec.Id == model.Id);

                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Equipment();
                            context.Equipments.Add(element);
                        }

                        element.EquipmentName = model.EquipmentName;
                        element.Cost = model.Cost;

                        context.SaveChanges();

                        if (model.Id.HasValue)
                        {
                            var equipmentRaws = context.EquipmentRaws.Where(rec => rec.EquipmentId == model.Id.Value).ToList();
                            context.EquipmentRaws.RemoveRange(equipmentRaws.Where(rec => !model.EquipmentRaws.ContainsKey(rec.RawId)).ToList());

                            context.SaveChanges();

                            foreach (var updateRaw in equipmentRaws)
                            {
                                updateRaw.Count =
                                model.EquipmentRaws[updateRaw.RawId].Item2;

                                model.EquipmentRaws.Remove(updateRaw.RawId);
                            }

                            context.SaveChanges();
                        }

                        foreach (var pc in model.EquipmentRaws)
                        {
                            context.EquipmentRaws.Add(new EquipmentRaw
                            {
                                EquipmentId = element.Id,
                                RawId = pc.Key,
                                Count = pc.Value.Item2
                            });

                            context.SaveChanges();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(EquipmentBindingModel model)
        {
            using (var context = new SecuritySystemDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.EquipmentRaws.RemoveRange(context.EquipmentRaws.Where(rec => rec.EquipmentId == model.Id));
                        Equipment element = context.Equipments.FirstOrDefault(rec => rec.Id == model.Id);

                        if (element != null)
                        {
                            context.Equipments.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<EquipmentViewModel> Read(EquipmentBindingModel model)
        {
            using (var context = new SecuritySystemDatabase())
            {
                return context.Equipments
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
                .Select(rec => new EquipmentViewModel
                {
                    Id = rec.Id,
                    EquipmentName = rec.EquipmentName,
                    Cost = rec.Cost,
                    EquipmentRaws = context.EquipmentRaws
                    .Include(recPC => recPC.Raw)
                    .Where(recPC => recPC.EquipmentId == rec.Id)
                    .ToDictionary(recPC => recPC.RawId, recPC => (recPC.Raw?.RawName, recPC.Count))
                })
                .ToList();
            }
        }
    }
}
