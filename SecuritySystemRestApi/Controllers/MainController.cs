using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecurityBusinessLogic.BindingModels;
using SecurityBusinessLogic.BusinessLogics;
using SecurityBusinessLogic.Interfaces;
using SecurityBusinessLogic.ViewModels;
using SecuritySystemRestApi.Models;

namespace SecuritySystemRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly IEquipmentLogic _equipment;
        private readonly MainLogic _main;

        public MainController(IOrderLogic order, IEquipmentLogic equipment, MainLogic main)
        {
            _order = order;
            _equipment = equipment;
            _main = main;
        }

        [HttpGet]
        public List<EquipmentModel> GetEquipmentList() => _equipment.Read(null)?.Select(rec => Convert(rec)).ToList();

        [HttpGet]
        public EquipmentModel GetEquipment(int equipmentId) => Convert(_equipment.Read(new EquipmentBindingModel { Id = equipmentId })?[0]);

        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel { ClientId = clientId });

        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _main.CreateOrder(model);

        private EquipmentModel Convert(EquipmentViewModel model)
        {
            if (model == null) return null;

            return new EquipmentModel
            {
                Id = model.Id,
                EquipmentName = model.EquipmentName,
                Cost = model.Cost
            };
        }
    }
}