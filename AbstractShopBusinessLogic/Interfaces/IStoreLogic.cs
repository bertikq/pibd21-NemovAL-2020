using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.Interfaces
{
    public interface IStoreLogic
    {
        List<StoreViewModel> Read(StoreBindingModel model);
        void CreateOrUpdate(StoreBindingModel model);
        void AddComponent(AddComponentInStoreBindingModel model);
        void Delete(StoreBindingModel model);
        bool WriteOffComponents(int componentId, int count);
    }
}
