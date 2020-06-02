using AbstractTravelCompanyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.BindingModels
{
    [DataContract]
    public class AddComponentInStoreBindingModel
    {
        [DataMember]
        public int StoreId { get; set; }
        [DataMember]
        public int Count { get; set; }
        [DataMember]
        public int ComponentId { get; set; }

    }
}
