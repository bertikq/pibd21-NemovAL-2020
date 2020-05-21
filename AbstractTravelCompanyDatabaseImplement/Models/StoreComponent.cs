using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyDatabaseImplement.Models
{
    public class StoreComponent
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public virtual Store Store{ get; set; }
        public int ComponentId { get; set; }
        public virtual Component Component { get; set; }
        public int Count { get; set; }
    }
}
