using AbstractShopBusinessLogic.BindingModels;
using AbstractShopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.Interfaces
{
    public interface ITourLogic
    {
        List<TourViewModel> Read(TourBindingModel model);
        void CreateOrUpdate(TourBindingModel model);
        void Delete(TourBindingModel model);
    }
}

