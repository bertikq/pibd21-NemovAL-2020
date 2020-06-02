using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace TravelCompanyRestApi.Models
{
    [DataContract]
    public class StoreModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Название склада")]
        public string Name { get; set; }
        [DataMember]
        public List<StoreComponentViewModel> StoreComponents { get; set; }
    }
}
