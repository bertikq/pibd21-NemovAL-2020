using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Enums;
using AbstractTravelCompanyBusinessLogic.HelperModels;
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
        private readonly IClientLogic clientLogic;
        private readonly object locker = new object();

        public MainLogic(IOrderLogic orderLogic, IStoreLogic storeLogic, IComponentLogic componentLogic, ITourLogic tourLogic, IClientLogic clientLogic)
        {
            this.orderLogic = orderLogic;
            _storeLogic = storeLogic;
            _componentLogic = componentLogic;
            _tourLogic = tourLogic;
            this.clientLogic = clientLogic;
        }

        public void CreateOrder(CreateOrderBindingModel model)
        {
            orderLogic.CreateOrUpdate(new OrderBindingModel
            {
                TourId = model.TourId,
                Count = model.Count,
                Sum = model.Sum,
                DateCreate = DateTime.Now,
                Status = OrderStatus.Принят,
                ClientId = model.ClientId
            });

            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = clientLogic.Read(new ClientBindingModel
                {
                    Id = model.ClientId
                })?[0]?.Email,
                Subject = $"Новый заказ",
                Text = $"Заказ принят."
            });
        }

        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            lock (locker)
            {
                    var order = orderLogic.Read(new OrderBindingModel
                    {
                        Id = model.OrderId
                    })?[0];
                    if (order == null)
                    {
                        throw new Exception("Не найден заказ");
                    }
                    if (order.Status != OrderStatus.Принят && order.Status != OrderStatus.ТребуютсяМатериалы)
                    {
                        throw new Exception("Заказ не в статусе \"Принят\"");
                    }
                    if (!model.ManagerId.HasValue)
                    {
                        throw new Exception("Не указан менеджер");
                    }

                try
                {
                    _storeLogic.WriteOffTour(order.TourId, order.Count);

                    orderLogic.CreateOrUpdate(new OrderBindingModel
                    {
                        Id = order.Id,
                        TourId = order.TourId,
                        Count = order.Count,
                        Sum = order.Sum,
                        DateCreate = order.DateCreate,
                        DateImplement = DateTime.Now,
                        Status = OrderStatus.Выполняется,
                        ManagerId = model.ManagerId,
                        ClientId = order.ClientId
                    });
                }
                catch (Exception)
                {
                    orderLogic.CreateOrUpdate(new OrderBindingModel
                    {
                        Id = order.Id,
                        TourId = order.TourId,
                        Count = order.Count,
                        Sum = order.Sum,
                        DateCreate = order.DateCreate,
                        Status = OrderStatus.ТребуютсяМатериалы,
                        ClientId = order.ClientId
                    });
                }
            }
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
                Status = OrderStatus.Готов,
                ClientId = order.ClientId,
                ManagerId = model.ManagerId
            });

            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = clientLogic.Read(new ClientBindingModel
                {
                    Id = order.ClientId
                })?[0]?.Email,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} готов."
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
                Status = OrderStatus.Оплачен,
                ClientId = order.ClientId,
                ManagerId = model.ManagerId
            });

            MailLogic.MailSendAsync(new MailSendInfo
            {
                MailAddress = clientLogic.Read(new ClientBindingModel
                {
                    Id = order.ClientId
                })?[0]?.Email,
                Subject = $"Заказ №{order.Id}",
                Text = $"Заказ №{order.Id} оплачен."
            });
        }

        public void AddComponentInStore(AddComponentInStoreBindingModel model)
        {
            _storeLogic.AddComponent(model);
        }
    }
}
