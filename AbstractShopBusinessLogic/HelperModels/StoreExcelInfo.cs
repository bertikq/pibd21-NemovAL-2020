using AbstractTravelCompanyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.HelperModels
{
    public class StoreExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<StoreViewModel> Stores { get; set; }
    }
}
