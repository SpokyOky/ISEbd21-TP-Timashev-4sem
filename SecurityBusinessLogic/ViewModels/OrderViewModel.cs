using SecurityBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SecurityBusinessLogic.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }

        [DisplayName("Изделие")]
        public string EquipmentName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
