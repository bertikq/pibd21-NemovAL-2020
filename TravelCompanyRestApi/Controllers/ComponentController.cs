using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelCompanyRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ComponentController : ControllerBase
    {
        private readonly IComponentLogic componentLogic;

        public ComponentController(IComponentLogic componentLogic)
        {
            this.componentLogic = componentLogic;
        }

        [HttpGet]
        public List<ComponentViewModel> Read() => componentLogic.Read(null);
    }
}