using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Enums;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using System;

namespace AbstractTravelCompanyBusinessLogic.BusinessLogics
{
    public class MainLogic
    {
        private readonly IOrderLogic orderLogic;
        private readonly IStoreLogic _storeLogic;
        private readonly IComponentLogic _componentLogic;
        private readonly ITourLogic _tourLogic;

        public MainLogic(IOrderLogic orderLogic, IStoreLogic storeLogic, IComponentLogic componentLogic, ITourLogic tourLogic)
        {
            this.orderLogic = orderLogic;
            _storeLogic = storeLogic;
            _componentLogic = componentLogic;
            _tourLogic = tourLogic;
        }

        public void CreateOrder(CreateOrderBindingModel model)
        {
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                TourId = model.TourId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят
            });
        }

        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel
            {
                Id = model.OrderId
            })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Принят)
            {
                throw new Exception("Заказ не в статусе \"Принят\"");
            }

           var tour = _tourLogic.Read(new TourBindingModel
            {
                Id = order.TourId
            })?[0];

            if (tour == null)
            {
                throw new Exception("Не найден тур");
            }

            foreach (int componentId in tour.ProductComponents.Keys)
            {
                if (!_storeLogic.WriteOffComponents(componentId, tour.ProductComponents[componentId].Item2 * order.Count))
                {
                    throw new Exception("Не хватает компонентов");
                }
            }

            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                TourId = order.TourId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = DateTime.Now,
                Status = OrderStatus.Выполняется
            });
        }

        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel
            {
                Id = model.OrderId
            })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                TourId = order.TourId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов
            });
        }

        public void PayOrder(ChangeStatusBindingModel model)
        {
            var order = orderLogic.Read(new OrderBindingModel
            {
                Id = model.OrderId
            })?[0];
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                Id = order.Id,
                TourId = order.TourId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Оплачен
            });
        }

        public void AddComponentInStore(AddComponentInStoreBindingModel model)
        {
            _storeLogic.AddComponent(model);
        }
    }
}
