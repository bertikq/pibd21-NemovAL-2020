using AbstractShopBusinessLogic.Enums;
using AbstractTravelCompanyFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace AbstractTravelCompanyFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string ProductFileName = "Product.xml";
        private readonly string ProductComponentFileName = "ProductComponent.xml";
        private readonly string ManagerFileName = "Manager.xml";
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Tour> Tours { get; set; }
        public List<TourComponent> TourComponents { get; set; }
        public List<Client> Clients { get; set; }
        public List<Manager> Managers { get; set; }
        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Tours = LoadTours();
            TourComponents = LoadTourComponents();
            Clients = new List<Client>();
            Managers = LoadManagers();
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
            SaveComponents();
            SaveOrders();
            SaveTours();
            SaveToursComponents();
            SaveManagers();
        }
        private List<Component> LoadComponents()
        {
            var list = new List<Component>();
            if (File.Exists(ComponentFileName))
            {
                XDocument xDocument = XDocument.Load(ComponentFileName);
                var xElements = xDocument.Root.Elements("Component").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Component
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComponentName = elem.Element("ComponentName").Value
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
                        TourId = Convert.ToInt32(elem.Element("ProductId").Value),
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
        private List<Tour> LoadTours()
        {
            var list = new List<Tour>();
            if (File.Exists(ProductFileName))
            {
                XDocument xDocument = XDocument.Load(ProductFileName);
                var xElements = xDocument.Root.Elements("Product").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Tour
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        TourName = elem.Element("ProductName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value)
                    });
                }
            }
            return list;
        }
        private List<Manager> LoadManagers()
        {
            var list = new List<Manager>();
            if (File.Exists(ManagerFileName))
            {
                XDocument xDocument = XDocument.Load(ManagerFileName);
                var xElements = xDocument.Root.Elements("Manager").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Manager
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ManagerFIO = elem.Element("ManagerFIO").Value,
                        PauseTime = Convert.ToInt32(elem.Element("PauseTime").Value),
                        WorkingTime = Convert.ToInt32(elem.Element("WorkingTime").Value)
                    });
                }
            }
            return list;
        }
        private List<TourComponent> LoadTourComponents()
        {
            var list = new List<TourComponent>();
            if (File.Exists(ProductComponentFileName))
            {
                XDocument xDocument = XDocument.Load(ProductComponentFileName);
                var xElements = xDocument.Root.Elements("ProductComponent").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new TourComponent
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        TourId = Convert.ToInt32(elem.Element("ProductId").Value),
                        ComponentId = Convert.ToInt32(elem.Element("ComponentId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }
        private void SaveComponents()
        {
            if (Components != null)
            {
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                    new XAttribute("Id", component.Id),
                    new XElement("ComponentName", component.ComponentName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ComponentFileName);
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
                    new XElement("ProductId", order.TourId),
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
        private void SaveTours()
        {
            if (Tours != null)
            {
                var xElement = new XElement("Products");
                foreach (var product in Tours)
                {
                    xElement.Add(new XElement("Product",
                    new XAttribute("Id", product.Id),
                    new XElement("ProductName", product.TourName),
                    new XElement("Price", product.Price)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ProductFileName);
            }
        }

        private void SaveManagers()
        {
            if (Managers != null)
            {
                var xElement = new XElement("Managers");
                foreach (var manager in Managers)
                {
                    xElement.Add(new XElement("Manager",
                    new XAttribute("Id", manager.Id),
                    new XElement("ManagerFIO", manager.ManagerFIO),
                    new XElement("PauseTime", manager.PauseTime)),
                    new XElement("WorkingTime", manager.WorkingTime));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ManagerFileName);
            }
        }
        private void SaveToursComponents()
        {
            if (TourComponents != null)
            {
                var xElement = new XElement("ProductComponents");
                foreach (var productComponent in TourComponents)
                {
                    xElement.Add(new XElement("ProductComponent",
                    new XAttribute("Id", productComponent.Id),
                    new XElement("ProductId", productComponent.TourId),
                    new XElement("ComponentId", productComponent.ComponentId),
                    new XElement("Count", productComponent.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ProductComponentFileName);
            }
        }
    }
}
