using SecurityBusinessLogic.BindingModels;
using SecurityBusinessLogic.Interfaces;
using SecurityBusinessLogic.ViewModels;
using SecurityBusinessLogic.HelperModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IRawLogic rawLogic;
        private readonly IEquipmentLogic equipmentLogic;
        private readonly IOrderLogic orderLogic;

        public ReportLogic(IEquipmentLogic equipmentLogic, IRawLogic rawLogic, IOrderLogic orderLogic)
        {
            this.equipmentLogic = equipmentLogic;
            this.rawLogic = rawLogic;
            this.orderLogic = orderLogic;
        }

        public List<ReportEquipmentRawViewModel> GetEquipmentRaw()
        {
            var equipments = equipmentLogic.Read(null);
            var list = new List<ReportEquipmentRawViewModel>();

            foreach (var equipment in equipments)
            {
                foreach (var pc in equipment.EquipmentRaws)
                {
                    var record = new ReportEquipmentRawViewModel
                    {
                        EquipmentName = equipment.EquipmentName,
                        RawName = pc.Value.Item1,
                        Count = pc.Value.Item2
                    };

                    list.Add(record);
                }
            }
            return list;
        }

        public List<IGrouping<DateTime, OrderViewModel>> GetOrders(ReportBindingModel model)
        {
            var list = orderLogic
            .Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .GroupBy(rec => rec.DateCreate.Date)
            .OrderBy(recG => recG.Key)
            .ToList();

            return list;
        }

        public void SaveEquipmentsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список изделий",
                Equipments = equipmentLogic.Read(null)
            });
        }

        public void SaveOrdersToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrders(model)
            });
        }

        public void SaveEquipmentRawsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список издлий с компонентами",
                EquipmentRaws = GetEquipmentRaw()
            });
        }
    }
}
