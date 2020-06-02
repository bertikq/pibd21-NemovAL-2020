﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace AbstractTravelCompanyBusinessLogic.ViewModels
{
    [DataContract]
    public class StoreViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Название склада")]
        public string Name { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> StoreComponents { get; set; }
    }
}
