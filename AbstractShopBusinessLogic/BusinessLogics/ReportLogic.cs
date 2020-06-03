using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.HelperModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractTravelCompanyBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IComponentLogic _componentLogic;
        private readonly ITourLogic _tourLogic;
        private readonly IOrderLogic _orderLogic;
        private readonly IStoreLogic _storeLogic;

        public ReportLogic(ITourLogic tourLogic, IComponentLogic componentLogic,
            IOrderLogic orderLogic, IStoreLogic storeLogic)
        {
            _componentLogic = componentLogic;
            _orderLogic = orderLogic;
            _tourLogic = tourLogic;
            _storeLogic = storeLogic;
        }

        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            StoreSaveToWord.CreateDoc(new StoreWordInfo
            {
                FileName = model.FileName,
                Title = "Список складов",
                Stores = _storeLogic.Read(null)
            });
        }

        public void SaveOrdersToExcelFile(ReportBindingModel model)
        {
            StoreSaveToExcel.CreateDoc(new StoreExcelInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Stores = _storeLogic.Read(null)
            });
        }

        public Dictionary<ComponentViewModel, List<(string, int)>> GetComponentStores()
        {
            var stores = _storeLogic.Read(null);

            return _componentLogic.Read(null).ToDictionary(x => new ComponentViewModel
            {
                ComponentName = x.ComponentName,
                Id = x.Id
            },
            y => stores.Where(x => x.StoreComponents.Keys.Contains(y.Id)).
            Select(x => (x.Name, x.StoreComponents[y.Id].Item2)).ToList());
        }

        public List<ReportComponentStoresViewModel> GetListComponentStores()
        {
            var componentStores = new List<ReportComponentStoresViewModel>();
            var dictComponentsStores = GetComponentStores();

            foreach(var component in dictComponentsStores)
            {
                componentStores.Add(new ReportComponentStoresViewModel
                {
                    ComponentName = component.Key.ComponentName,
                    StoreName = "",
                    ComponentCount = ""
                });

                foreach (var store in component.Value)
                {
                    componentStores.Add(new ReportComponentStoresViewModel
                    {
                        StoreName = store.Item1,
                        ComponentCount = store.Item2.ToString(),
                        ComponentName = ""
                    });
                }

                componentStores.Add(new ReportComponentStoresViewModel
                {
                    ComponentName = "",
                    StoreName = "",
                    ComponentCount = component.Value.Sum(x => x.Item2).ToString()
                });
            }

            return componentStores;
        }

        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
        [Obsolete]
        public void SaveToursComponentsToPdfFile(ReportBindingModel model)
        {
            StoreSaveToPdf.CreateDoc(new StorePdfInfo
            {
                FileName = model.FileName,
                Title = "Список компонентов и складов",
                ComponentStores = GetComponentStores()
            });
        }
    }
}

