﻿using AbstractShopBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractShopBusinessLogic.Interfaces
{
    public interface ITourLogic
    {
        List<TourViewModel> Read(TourBindingModel model);
        void CreateOrUpdate(TourBindingModel model);
        void Delete(TourBindingModel model);
    }
}

