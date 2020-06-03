using AbstractTravelCompanyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.HelperModels
{
    public class StorePdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public Dictionary<ComponentViewModel, List<(string, int)>> ComponentStores { get; set; }
    }
}
