using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.BindingModels
{
    [DataContract]
    public class StoreBindingModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
