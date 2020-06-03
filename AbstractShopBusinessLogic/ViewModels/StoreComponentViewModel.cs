using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AbstractTravelCompanyBusinessLogic.ViewModels
{
    [DataContract]
    public class StoreComponentViewModel
    {
        [DataMember]
        public int ComponentId { get; set; }
        [DataMember]
        public string ComponentName { get; set; }
        [DataMember]
        public int CountComponent { get; set; }
    }
}
