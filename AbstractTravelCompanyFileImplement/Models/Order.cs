﻿using AbstractShopBusinessLogic.Enums;
using System;

namespace AbstractTravelCompanyFileImplement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int TourId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public int ClientId { get; set; }

    }
}
