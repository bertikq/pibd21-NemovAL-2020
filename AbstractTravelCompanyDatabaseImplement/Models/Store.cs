using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AbstractTravelCompanyDatabaseImplement.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("StoreId")]
        public virtual List<StoreComponent> StoreComponents { get; set; }
    }
}
