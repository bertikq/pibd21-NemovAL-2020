using AbstractTravelCompanyFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyFileImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Tour> Tours { get; set; }
        public List<TourComponent> TourComponents { get; set; }
        public List<Store> Stores { get; set; }
        public List<StoreComponent> StoreComponents { get; set; }
        private DataListSingleton()
        {
            Components = new List<Component>();
            Orders = new List<Order>();
            Tours = new List<Tour>();
            TourComponents = new List<TourComponent>();
            Stores = new List<Store>();
            StoreComponents = new List<StoreComponent>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
