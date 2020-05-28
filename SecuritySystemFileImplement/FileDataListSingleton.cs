﻿using SecurityBusinessLogic.Enums;
using SecuritySystemFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SecuritySystemFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;

        private readonly string RawFileName = "Raw.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string EquipmentFileName = "Equipment.xml";
        private readonly string EquipmentRawFileName = "EquipmentRaw.xml";
        private readonly string ClientFileName = "Client.xml";
        private readonly string ImplementerFileName = "Implementer.xml";
        private readonly string MessageInfoFileName = "MessageInfo.xml";

        public List<Raw> Raws { get; set; }

        public List<Order> Orders { get; set; }

        public List<Equipment> Equipments { get; set; }

        public List<EquipmentRaw> EquipmentRaws { get; set; }

        public List<Client> Clients { get; set; }

        public List<Implementer> Implementers { get; set; }

        public List<MessageInfo> MessageInfoes { get; set; }

        private FileDataListSingleton()
        {
            Raws = LoadRaws();
            Orders = LoadOrders();
            Equipments = LoadEquipments();
            EquipmentRaws = LoadEquipmentRaws();
            Clients = LoadClients();
            Implementers = LoadImplementers();
            MessageInfoes = LoadMessageInfoes();
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
            SaveClients();
            SaveImplementers();
            SaveMessageInfoes();
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
                        ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                        EquipmentId = Convert.ToInt32(elem.Element("EquipmentId").Value),
                        ImplementerId = string.IsNullOrEmpty(elem.Element("ImplementerId").Value) ? (int?)null : Convert.ToInt32(elem.Element("ImplementerId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), elem.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null : Convert.ToDateTime(elem.Element("DateImplement").Value),
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

        private List<Client> LoadClients()
        {
            var list = new List<Client>();

            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        FIO = elem.Element("FIO").Value,
                        Email = elem.Element("Email").Value,
                        Password = elem.Element("Password").Value
                    });
                }
            }

            return list;
        }

        private List<Implementer> LoadImplementers()
        {
            var list = new List<Implementer>();

            if (File.Exists(ImplementerFileName))
            {
                XDocument xDocument = XDocument.Load(ImplementerFileName);
                var xElements = xDocument.Root.Elements("Implementer").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new Implementer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ImplementerFIO = elem.Element("ImplementerFIO").Value,
                        WorkingTime = Convert.ToInt32(elem.Element("WorkingTime").Value),
                        PauseTime = Convert.ToInt32(elem.Element("PauseTime").Value)
                    });
                }
            }

            return list;
        }

        private List<MessageInfo> LoadMessageInfoes()
        {
            var list = new List<MessageInfo>();

            if (File.Exists(MessageInfoFileName))
            {
                XDocument xDocument = XDocument.Load(MessageInfoFileName);
                var xElements = xDocument.Root.Elements("MessageInfo").ToList();

                foreach (var elem in xElements)
                {
                    list.Add(new MessageInfo
                    {
                        MessageId = elem.Attribute("MessageId").Value,
                        ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                        SenderName = elem.Element("SenderName").Value,
                        DateDelivery = Convert.ToDateTime(elem.Element("DateDelivery").Value),
                        Subject = elem.Element("Subject").Value,
                        Body = elem.Element("Body").Value
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

                foreach (var raw in Raws)
                {
                    xElement.Add(new XElement("Raw",
                    new XAttribute("Id", raw.Id),
                    new XElement("RawName", raw.RawName)));
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
                    new XElement("ClientId", order.ClientId),
                    new XElement("EquipmentId", order.EquipmentId),
                    new XElement("ImplementerId", order.ImplementerId),
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

        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");

                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("FIO", client.FIO),
                    new XElement("Email", client.Email),
                    new XElement("Password", client.Password)));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }

        private void SaveImplementers()
        {
            if (Implementers != null)
            {
                var xElement = new XElement("Implementers");

                foreach (var implementer in Implementers)
                {
                    xElement.Add(new XElement("Implementer",
                    new XAttribute("Id", implementer.Id),
                    new XElement("ImplementerFIO", implementer.ImplementerFIO),
                    new XElement("WorkingTime", implementer.WorkingTime),
                    new XElement("PauseTime", implementer.PauseTime)));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ImplementerFileName);
            }
        }

        private void SaveMessageInfoes()
        {
            if (MessageInfoes != null)
            {
                var xElement = new XElement("MessageInfoes");

                foreach (var messageInfo in MessageInfoes)
                {
                    xElement.Add(new XElement("MessageInfo",
                    new XAttribute("Id", messageInfo.MessageId),
                    new XElement("ClientId", messageInfo.ClientId),
                    new XElement("SenderName", messageInfo.SenderName),
                    new XElement("DateDelivery", messageInfo.DateDelivery),
                    new XElement("Subject", messageInfo.Subject),
                    new XElement("Body", messageInfo.Body)));
                }

                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(MessageInfoFileName);
            }
        }
    }
}
