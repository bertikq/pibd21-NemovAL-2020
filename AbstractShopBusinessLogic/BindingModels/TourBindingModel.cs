using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.BindingModels
{
    /// <summary>
    /// Туристические путевки
    /// </summary>
    public class TourBindingModel
    {
        public int? Id { get; set; }
        public string TourName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> TourComponents { get; set; }
    }
}
