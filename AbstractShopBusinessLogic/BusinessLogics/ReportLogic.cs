using AbstractShopBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.HelperModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly IComponentLogic _componentLogic;
        private readonly ITourLogic _tourLogic;
        private readonly IOrderLogic _orderLogic;

        public ReportLogic(ITourLogic tourLogic, IComponentLogic componentLogic,
            IOrderLogic orderLogic)
        {
            _componentLogic = componentLogic;
            _orderLogic = orderLogic;
            _tourLogic = tourLogic;

        }
        /// <summary>
        /// Получение списка заказов за определенный период
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Dictionary<DateTime, List<ReportOrdersViewModel>> GetOrders(ReportBindingModel model)
        {
            Dictionary<DateTime, List<ReportOrdersViewModel>> orders = new Dictionary<DateTime, List<ReportOrdersViewModel>>();

            foreach (OrderViewModel orderViewModel in _orderLogic.Read(null, model.DateFrom, model.DateTo))
            {
                if (orderViewModel.DateCreate > model.DateFrom && orderViewModel.DateCreate < model.DateTo)
                {
                    if (!orders.ContainsKey(orderViewModel.DateCreate.Date))
                    {
                        orders.Add(orderViewModel.DateCreate.Date, new List<ReportOrdersViewModel>());
                    }

                    orders[orderViewModel.DateCreate.Date].Add(new ReportOrdersViewModel
                    {
                        Count = orderViewModel.Count,
                        DateCreate = orderViewModel.DateCreate,
                        DateFrom = model.DateFrom.Value,
                        DateTo = model.DateTo.Value,
                        TourName = orderViewModel.TourName,
                        Status = orderViewModel.Status,
                        Sum = orderViewModel.Sum
                    });
                }
            }
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
        /// <summary>
        /// Сохранение компонент в файл-Word
        /// </summary>
        /// <param name="model"></param>
        public void SaveComponentsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список туров",
                Tours = _tourLogic.Read(null)
            });
        }
        /// <summary>
        /// Сохранение компонент с указаеним продуктов в файл-Excel
        /// </summary>
        /// <param name="model"></param>
        public void SaveOrdersToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrders(model)
            });
        }

        /// <summary>
        /// Сохранение заказов в файл-Pdf
        /// </summary>
        /// <param name="model"></param>
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

