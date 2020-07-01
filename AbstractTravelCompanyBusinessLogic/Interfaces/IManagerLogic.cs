using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.Interfaces
{
    public interface IManagerLogic
    {
        List<ManagerViewModel> Read(ManagerBindingModel model);
        void CreateOrUpdate(ManagerBindingModel model);
        void Delete(ManagerBindingModel model);
    }
}
