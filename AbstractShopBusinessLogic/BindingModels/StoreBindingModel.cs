using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.BindingModels
{
    public class StoreBindingModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public Dictionary<int, (string, int)> StoreComponents { get; set; }
    }
}
