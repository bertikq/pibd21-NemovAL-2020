using AbstractTravelCompanyBusinessLogic.Enums;
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
        private readonly string TourFileName = "Tour.xml";
        private readonly string TourComponentFileName = "TourComponent.xml";
        private readonly string StoreFileName = "Store.xml";
        private readonly string StoreComponentFileName = "StoreComponent.xml";
        private readonly string ClientFileName = "Client.xml";
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Tour> Tours { get; set; }
        public List<TourComponent> TourComponents { get; set; }
        public List<Store> Stores { get; set; }
        public List<StoreComponent> StoreComponents { get; set; }
        public List<Client> Clients { get; set; }
        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Tours = LoadTours();
            TourComponents = LoadTourComponents();
            Stores = LoadStores();
            StoreComponents = LoadStoreComponents();
            Clients = LoadClients();
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
            SaveStores();
            SaveStoreComponents();
            SaveClients();
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
            if (File.Exists(TourFileName))
            {
                XDocument xDocument = XDocument.Load(TourFileName);
                var xElements = xDocument.Root.Elements("Tour").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Tour
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        TourName = elem.Element("TourName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value)
                    });
                }
            }
            return list;
        }
        private List<Store> LoadStores()
        {
            var list = new List<Store>();
            if (File.Exists(StoreFileName))
            {
                XDocument xDocument = XDocument.Load(StoreFileName);
                var xElements = xDocument.Root.Elements("Store").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Store
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        Name = elem.Element("StoreName").Value
                    });
                }
            }
            return list;
        }
        private List<StoreComponent> LoadStoreComponents()
        {
            var list = new List<StoreComponent>();
            if (File.Exists(StoreComponentFileName))
            {
                XDocument xDocument = XDocument.Load(StoreComponentFileName);
                var xElements = xDocument.Root.Elements("StoreComponent").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new StoreComponent
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        StoreId = Convert.ToInt32(elem.Element("StoreId").Value),
                        ComponentId = Convert.ToInt32(elem.Element("ComponentId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value)
                    });
                }
            }
            return list;
        }
        private List<TourComponent> LoadTourComponents()
        {
            var list = new List<TourComponent>();
            if (File.Exists(TourComponentFileName))
            {
                XDocument xDocument = XDocument.Load(TourComponentFileName);
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
                        Email = elem.Element("Email").Value,
                        FIO = elem.Element("FIO").Value,
                        Password = elem.Element("Password").Value
                    });
                }
            }
            return list;
        }
        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Client");
                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Product",
                    new XAttribute("Id", client.Id),
                    new XAttribute("Email", client.Email),
                    new XAttribute("FIO", client.FIO),
                    new XAttribute("Password", client.Password)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
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
                var xElement = new XElement("Tours");
                foreach (var product in Tours)
                {
                    xElement.Add(new XElement("Tour",
                    new XAttribute("Id", product.Id),
                    new XElement("TourName", product.TourName),
                    new XElement("Price", product.Price)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(TourFileName);
            }
        }
        private void SaveStores()
        {
            if (Tours != null)
            {
                var xElement = new XElement("Stores");
                foreach (var store in Stores)
                {
                    xElement.Add(new XElement("Store",
                    new XAttribute("Id", store.Id),
                    new XElement("StoreName", store.Name)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(StoreFileName);
            }
        }
        private void SaveStoreComponents()
        {
            if (TourComponents != null)
            {
                var xElement = new XElement("StoreComponents");
                foreach (var storeComponent in StoreComponents)
                {
                    xElement.Add(new XElement("StoreComponent",
                    new XAttribute("Id", storeComponent.Id),
                    new XElement("StoreId", storeComponent.StoreId),
                    new XElement("ComponentId", storeComponent.ComponentId),
                    new XElement("Count", storeComponent.Count)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(StoreComponentFileName);
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
                xDocument.Save(TourComponentFileName);
            }
        }
    }
}
