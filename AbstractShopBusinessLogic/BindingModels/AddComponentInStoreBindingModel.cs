using AbstractTravelCompanyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.BindingModels
{
    public class AddComponentInStoreBindingModel
    {
        public int StoreId { get; set; }
        public int Count { get; set; }
        public int ComponentId { get; set; }

    }
}
