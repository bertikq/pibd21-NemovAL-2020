using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.BusinessLogics;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TravelCompanyRestApi.Models;

namespace TravelCompanyRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;
        private readonly ITourLogic _tour;
        private readonly MainLogic _main;
        public MainController(IOrderLogic order, ITourLogic tour, MainLogic main)
        {
            _order = order;
            _tour = tour;
            _main = main;
        }

        [HttpGet]
        public List<TourModel> GetProductList() => _tour.Read(null)?.Select(rec => Convert(rec)).ToList();

        [HttpGet]
        public TourModel GetProduct(int productId) => Convert(_tour.Read(new TourBindingModel { Id = productId })?[0]);

        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel { ClientId = clientId });

        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _main.CreateOrder(model);
        
        private TourModel Convert(TourViewModel model)
        {
            if (model == null) return null;
            return new TourModel
            {
                Id = model.Id,
                TourName = model.TourName,
                Price = model.Price
            };
        }
    }

}
