﻿using SecurityBusinessLogic.BindingModels;
using SecurityBusinessLogic.Enums;
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
    public class OrderLogic : IOrderLogic
    {
        private readonly DataListSingleton source;

        public OrderLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(OrderBindingModel model)
        {
            Order tempOrder = model.Id.HasValue ? null : new Order { Id = 1 };

            foreach (var order in source.Orders)
            {
                if (!model.Id.HasValue && order.Id >= tempOrder.Id)
                {
                    tempOrder.Id = order.Id + 1;
                }
                else if (model.Id.HasValue && order.Id == model.Id)
                {
                    tempOrder = order;
                }
            }

            if (model.Id.HasValue)
            {
                if (tempOrder == null)
                {
                    throw new Exception("Элемент не найден");
                }

                CreateModel(model, tempOrder);
            }
            else
            {
                source.Orders.Add(CreateModel(model, tempOrder));
            }
        }

        public void Delete(OrderBindingModel model)
        {
            for (int i = 0; i < source.Orders.Count; ++i)
            {
                if (source.Orders[i].Id == model.Id)
                {
                    source.Orders.RemoveAt(i);
                    return;
                }
            }

            throw new Exception("Элемент не найден");
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.EquipmentId = model.EquipmentId;
            order.Count = model.Count;
            order.ClientId = (int)model.ClientId;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            order.Sum = model.Sum;
            order.Status = model.Status;

            return order;
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            List<OrderViewModel> result = new List<OrderViewModel>();

            foreach (var order in source.Orders)
            {
                if (
                    model != null && order.Id == model.Id
                    || model.DateFrom.HasValue && model.DateTo.HasValue && order.DateCreate >= model.DateFrom && order.DateCreate <= model.DateTo
                    || model.ClientId.HasValue && order.ClientId == model.ClientId
                    || model.FreeOrders.HasValue && model.FreeOrders.Value
                    || model.ImplementerId.HasValue && order.ImplementerId == model.ImplementerId && order.Status == OrderStatus.Выполняется
                )
                {
                    result.Add(CreateViewModel(order));
                    break;
                }

                result.Add(CreateViewModel(order));
            }

            return result;
        }

        private OrderViewModel CreateViewModel(Order order)
        {
            string equipmentName = null;

            foreach (var equipment in source.Equipments)
            {
                if (equipment.Id == order.EquipmentId)
                {
                    equipmentName = equipment.EquipmentName;
                }
            }

            if (equipmentName == null)
            {
                throw new Exception("Продукт не найден");
            }

            return new OrderViewModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                EquipmentId = order.EquipmentId,
                EquipmentName = equipmentName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}
