using SecurityBusinessLogic.Enums;
using SecuritySystemFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SecuritySystemFileImplemet
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string RawFileName = "Raw.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string EquipmentFileName = "Equipment.xml";
        private readonly string EquipmentRawFileName = "EquipmentRaw.xml";
        public List<Raw> Raws { get; set; }
        public List<Order> Orders { get; set; }
        public List<Equipment> Equipments { get; set; }
        public List<EquipmentRaw> EquipmentRaws { get; set; }

        private FileDataListSingleton()
        {
            Raws = LoadRaws();
            Orders = LoadOrders();
            Equipments = LoadEquipments();
            EquipmentRaws = LoadEquipmentRaws();
        }

        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }

        ~FileDataListSingleton()
        {
            SaveRaws();
            SaveOrders();
            SaveEquipments();
            SaveEquipmentRaws();
        }

        private List<Raw> LoadRaws()
        {
            var list = new List<Raw>();
            if (File.Exists(RawFileName))
            {
                XDocument xDocument = XDocument.Load(RawFileName);
                var xElements = xDocument.Root.Elements("Raw").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Raw
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        RawName = elem.Element("RawName").Value
                    });
                }
            }
            return list;
        }

        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        EquipmentId = Convert.ToInt32(elem.Element("EquipmentId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus),
                   elem.Element("Status").Value),
                        DateCreate =
                   Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement =
                   string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null :
                   Convert.ToDateTime(elem.Element("DateImplement").Value),
                    });
                }
            }
            return list;
        }

        private List<Equipment> LoadEquipments()
        {
            var list = new List<Equipment>();
            if (File.Exists(EquipmentFileName))
            {
                XDocument xDocument = XDocument.Load(EquipmentFileName);
                var xElements = xDocument.Root.Elements("Equipment").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Equipment
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        EquipmentName = elem.Element("EquipmentName").Value,
                        Cost = Convert.ToDecimal(elem.Element("Cost").Value)
                    });
                }
            }
            return list;
        }

        private List<EquipmentRaw> LoadEquipmentRaws()
        {
            var list = new List<EquipmentRaw>();
            if (File.Exists(EquipmentRawFileName))
            {
                XDocument xDocument = XDocument.Load(EquipmentRawFileName);
                var xElements = xDocument.Root.Elements("EquipmentRaw").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new EquipmentRaw
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        EquipmentId = Convert.ToInt32(elem.Element("EquipmentId").Value),
                        RawId = Convert.ToInt32(elem.Element("RawId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }

        private void SaveRaws()
        {
            if (Raws != null)
            {
                var xElement = new XElement("Raws");
                foreach (var component in Raws)
                {
                    xElement.Add(new XElement("Raw",
                    new XAttribute("Id", component.Id),
                    new XElement("RawName", component.RawName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(RawFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", order.Id),
                    new XElement("EquipmentId", order.EquipmentId),
                    new XElement("Count", order.Count),
                    new XElement("Sum", order.Sum),
                    new XElement("Status", order.Status),
                    new XElement("DateCreate", order.DateCreate),
                    new XElement("DateImplement", order.DateImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }

        private void SaveEquipments()
        {
            if (Equipments != null)
            {
                var xElement = new XElement("Equipments");
                foreach (var equipment in Equipments)
                {
                    xElement.Add(new XElement("Equipment",
                    new XAttribute("Id", equipment.Id),
                    new XElement("EquipmentName", equipment.EquipmentName),
                    new XElement("Cost", equipment.Cost)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(EquipmentFileName);
            }
        }

        private void SaveEquipmentRaws()
        {
            if (EquipmentRaws != null)
            {
                var xElement = new XElement("EquipmentRaws");
                foreach (var equipmentRaw in EquipmentRaws)
                {
                    xElement.Add(new XElement("EquipmentRaw",
                    new XAttribute("Id", equipmentRaw.Id),
                    new XElement("EquipmentId", equipmentRaw.EquipmentId),
                    new XElement("RawId", equipmentRaw.RawId),
                    new XElement("Count", equipmentRaw.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(EquipmentRawFileName);
            }
        }
    }
}
