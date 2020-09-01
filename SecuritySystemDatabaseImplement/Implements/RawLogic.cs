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
    public class RawLogic : IRawLogic
    {
        public void CreateOrUpdate(RawBindingModel model)
        {
            using (var context = new SecuritySystemDatabase())
            {
                Raw element = context.Raws.FirstOrDefault(rec =>
               rec.RawName == model.RawName && rec.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
                if (model.Id.HasValue)
                {
                    element = context.Raws.FirstOrDefault(rec => rec.Id ==
                   model.Id);
                if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Raw();
                    context.Raws.Add(element);
                }
                element.RawName = model.RawName;
                context.SaveChanges();
            }
        }
        public void Delete(RawBindingModel model)
        {
            using (var context = new SecuritySystemDatabase())
            {
                Raw element = context.Raws.FirstOrDefault(rec => rec.Id ==
               model.Id);
                if (element != null)
                {
                    context.Raws.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<RawViewModel> Read(RawBindingModel model)
        {
            using (var context = new SecuritySystemDatabase())
            {
                return context.Raws
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
}
