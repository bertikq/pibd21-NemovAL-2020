using System.Collections.Generic;
using System.Linq;
using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace TravelCompanyRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreLogic storeLogic;

        public StoreController(IStoreLogic storeLogic)
        {
            this.storeLogic = storeLogic;
        }

        [HttpGet]
        public List<StoreApiViewModel> Read() => storeLogic.Read(null).Select(rec => Convert(rec)).ToList();
        [HttpGet]
        public List<StoreApiViewModel> ReadStoreById(int storeId) => storeLogic.Read(new StoreBindingModel
        {
            Id = storeId
        }).Select(rec => Convert(rec)).ToList();

        [HttpPost]
        public void CreateOrUpdate(StoreBindingModel model) => storeLogic.CreateOrUpdate(model);

        [HttpPost]
        public void Delete(StoreBindingModel model) => storeLogic.Delete(model);

        [HttpPost]
        public void WriteOffTour(int tourId, int count) => storeLogic.WriteOffTour(tourId, count);

        private StoreApiViewModel Convert(StoreViewModel model)
        {
            if (model == null) return null;
            return new StoreApiViewModel
            {
                Id = model.Id,
                Name = model.Name,
                StoreComponents = model.StoreComponents.Select(x => new StoreComponentViewModel
                {
                    ComponentId = x.Key,
                    ComponentName = x.Value.Item1,
                    CountComponent = x.Value.Item2
                }).ToList()
            };
        }
    }
}
