﻿using AbstractShopBusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AbstractShopBusinessLogic.BindingModels
{
    /// <summary>
    /// Заказ
    /// </summary>
    [DataContract]
    public class OrderBindingModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public int TourId { get; set; }

        [DataMember]
        public int Count { get; set; }

        [DataMember]
        public decimal Sum { get; set; }

        [DataMember]
        public OrderStatus Status { get; set; }

        [DataMember]
        public DateTime DateCreate { get; set; }

        [DataMember]
        public DateTime? DateImplement { get; set; }

        [DataMember]
        public int? ClientId { get; set; }

    }
}

