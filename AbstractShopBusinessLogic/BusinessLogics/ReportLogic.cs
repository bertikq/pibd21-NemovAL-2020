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

        public Dictionary<DateTime, List<ReportOrdersViewModel>> GetOrders(ReportBindingModel model)
        {
            Dictionary<DateTime, List<ReportOrdersViewModel>> orders = new Dictionary<DateTime, List<ReportOrdersViewModel>>();

            orders = _orderLogic.Read(null, model.DateFrom, model.DateTo)
                .GroupBy(x => x.DateCreate)
                .ToDictionary(x => x.Key, y => y.Select(z => new ReportOrdersViewModel
                {
                    Count = z.Count,
                    DateCreate = z.DateCreate,
                    DateFrom = model.DateFrom.Value,
                    DateTo = model.DateTo.Value,
                    TourName = z.TourName,
                    Status = z.Status,
                    Sum = z.Sum
                }).ToList());

            return orders;
        }

        public List<ReportToursComponentsViewModel> GetTours()
        {
            List<ReportToursComponentsViewModel> result = new List<ReportToursComponentsViewModel>();

            List<TourViewModel> tours = _tourLogic.Read(null);

            foreach (TourViewModel tour in tours)
            {

                foreach ((string, int) component in tour.ProductComponents.Values)
                {
                    ReportToursComponentsViewModel report = new ReportToursComponentsViewModel
                    {
                        TourName = tour.TourName,
                        ComponentCount = component.Item2,
                        ComponentName = component.Item1
                    };
                    result.Add(report);
                }
            }

            return result;
        }

        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список туров",
                Tours = _tourLogic.Read(null)
            });
        }



        public void SaveStoresToWordFile(ReportBindingModel model)
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
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrders(model)
            });
        }



        public void SaveStoresToExcelFile(ReportBindingModel model)
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
        public void SaveStoresComponentsToPdfFile(ReportBindingModel model)
        {
            StoreSaveToPdf.CreateDoc(new StorePdfInfo
            {
                FileName = model.FileName,
                Title = "Список компонентов и складов",
                ComponentStores = GetComponentStores()
            });
        }

        [Obsolete]
        public void SaveToursComponentsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Tours = GetTours()
            });
        }
    }
}

