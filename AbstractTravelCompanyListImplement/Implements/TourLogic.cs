using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using AbstractTravelCompanyListImplement.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractTravelCompanyFileImplement.Implements
{
    public class TourLogic : ITourLogic
    {
        private readonly DataListSingleton source;
        public TourLogic()
        {
            source = DataListSingleton.GetInstance();
        }
        public void CreateOrUpdate(TourBindingModel model)
        {
            Tour tempProduct = model.Id.HasValue ? null : new Tour { Id = 1 };
            foreach (var product in source.Tours)
            {
                if (product.TourName == model.TourName && product.Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
                if (!model.Id.HasValue && product.Id >= tempProduct.Id)
                {
                    tempProduct.Id = product.Id + 1;
                }
                else if (model.Id.HasValue && product.Id == model.Id)
                {
                    tempProduct = product;
                }
            }
            if (model.Id.HasValue)
            {
                if (tempProduct == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, tempProduct);
            }
            else
            {
                source.Tours.Add(CreateModel(model, tempProduct));
            }
        }
        public void Delete(TourBindingModel model)
        {
            // удаляем записи по компонентам при удалении изделия
            for (int i = 0; i < source.TourComponents.Count; ++i)
            {
                if (source.TourComponents[i].TourId == model.Id)
                {
                    source.TourComponents.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Tours.Count; ++i)
            {
                if (source.Tours[i].Id == model.Id)
                {
                    source.Tours.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
        private Tour CreateModel(TourBindingModel model, Tour tour)
        {
            tour.TourName = model.TourName;
            tour.Price = model.Price;
            //обновляем существуюущие компоненты и ищем максимальный идентификатор
            int maxPCId = 0;
            for (int i = 0; i < source.TourComponents.Count; ++i)
            {
                if (source.TourComponents[i].Id > maxPCId)
                {
                    maxPCId = source.TourComponents[i].Id;
                }
                if (source.TourComponents[i].TourId == tour.Id)
                {
                    // если в модели пришла запись компонента с таким id
                    if
                    (model.TourComponents.ContainsKey(source.TourComponents[i].ComponentId))
                    {
                        // обновляем количество
                        source.TourComponents[i].Count =
                        model.TourComponents[source.TourComponents[i].ComponentId].Item2;
                        // из модели убираем эту запись, чтобы остались только не просмотренные
                        model.TourComponents.Remove(source.TourComponents[i].ComponentId);
                    }
                    else
                    {
                        source.TourComponents.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            foreach (var pc in model.TourComponents)
            {
                source.TourComponents.Add(new TourComponent
                {
                    Id = ++maxPCId,
                    TourId = tour.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
            return tour;
        }
        public List<TourViewModel> Read(TourBindingModel model)
        {
            List<TourViewModel> result = new List<TourViewModel>();
            foreach (var component in source.Tours)
            {
                if (model != null)
                {
                    if (component.Id == model.Id)
                    {
                        result.Add(CreateViewModel(component));
                        break;
                    }
                    continue;
                }
                result.Add(CreateViewModel(component));
            }
            return result;
        }
        private TourViewModel CreateViewModel(Tour product)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
            Dictionary<int, (string, int)> productComponents = new Dictionary<int, (string, int)>();
            foreach (var pc in source.TourComponents)
            {
                if (pc.TourId == product.Id)
                {
                    string componentName = string.Empty;
                    foreach (var component in source.Components)
                    {
                        if (pc.ComponentId == component.Id)
                        {
                            componentName = component.ComponentName;
                            break;
                        }
                    }
                    productComponents.Add(pc.ComponentId, (componentName, pc.Count));
                }
            }
            return new TourViewModel
            {
                Id = product.Id,
                TourName = product.TourName,
                Price = product.Price,
                ProductComponents = productComponents
            };
        }
    }
}

