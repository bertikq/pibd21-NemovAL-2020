using AbstractTravelCompanyBusinessLogic.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.ViewModels
{

    [DataContract]
    public class TourViewModel : BaseViewModel
    {

        [DataMember]
        [DisplayName("Название тура")]
        [Column(title: "Название тура", width: 150)]
        public string TourName { get; set; }

        [DataMember]
        [DisplayName("Цена")]
        [Column(title: "Цена", width: 100)]
        public decimal Price { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> ProductComponents { get; set; }

        public override List<string> Properties() => new List<string> { "Id", "TourName", "Price" };
    }
}

