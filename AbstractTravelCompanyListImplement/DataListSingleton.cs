﻿using AbstractTravelCompanyListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Tour> Tours { get; set; }
        public List<TourComponent> TourComponents { get; set; }
        public List<Client> Clients { get; set; }
        public List<Manager> Managers { get; set; }

        private DataListSingleton()
        {
            Components = new List<Component>();
            Orders = new List<Order>();
            Tours = new List<Tour>();
            TourComponents = new List<TourComponent>();
            Clients = new List<Client>();
            Managers = new List<Manager>();
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
