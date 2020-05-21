using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.ViewModels
{
    public class StoreViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название склада")]
        public string Name { get; set; }
        public Dictionary<int, (string, int)> StoreComponents { get; set; }
    }
}
