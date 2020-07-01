using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using AbstractTravelCompanyFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractTravelCompanyFileImplement.Implements
{
    public class TourLogic : ITourLogic
    {
        private readonly FileDataListSingleton source;
        public TourLogic()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public void CreateOrUpdate(TourBindingModel model)
        {
            Tour element = source.Tours.FirstOrDefault(rec => rec.TourName ==
           model.TourName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            if (model.Id.HasValue)
            {
                element = source.Tours.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
            }
            else
            {
                int maxId = source.Tours.Count > 0 ? source.Components.Max(rec =>
               rec.Id) : 0;
                element = new Tour { Id = maxId + 1 };
                source.Tours.Add(element);
            }
            element.TourName = model.TourName;
            element.Price = model.Price;
            // удалили те, которых нет в модели
            source.TourComponents.RemoveAll(rec => rec.TourId == model.Id &&
           !model.TourComponents.ContainsKey(rec.ComponentId));
            // обновили количество у существующих записей
            var updateComponents = source.TourComponents.Where(rec => rec.TourId ==
           model.Id && model.TourComponents.ContainsKey(rec.ComponentId));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.Count =
               model.TourComponents[updateComponent.ComponentId].Item2;
                model.TourComponents.Remove(updateComponent.ComponentId);
            }
            // добавили новые
            int maxPCId = source.TourComponents.Count > 0 ?
           source.TourComponents.Max(rec => rec.Id) : 0;
            foreach (var pc in model.TourComponents)
            {
                source.TourComponents.Add(new TourComponent
                {
                    Id = ++maxPCId,
                    TourId = element.Id,
                    ComponentId = pc.Key,
                    Count = pc.Value.Item2
                });
            }
        }

        public void Delete(TourBindingModel model)
        {
            // удаяем записи по компонентам при удалении изделия
            source.TourComponents.RemoveAll(rec => rec.TourId == model.Id);
            Tour element = source.Tours.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                source.Tours.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public List<TourViewModel> Read(TourBindingModel model)
        {
            return source.Tours
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new TourViewModel
                {
                    Id = rec.Id,
                    TourName = rec.TourName,
                    Price = rec.Price,
                    ProductComponents = source.TourComponents
                .Where(recPC => recPC.TourId == rec.Id)
               .ToDictionary(recPC => recPC.ComponentId, recPC =>
                (source.Components.FirstOrDefault(recC => recC.Id ==
               recPC.ComponentId)?.ComponentName, recPC.Count))
                })
                .ToList();
        }
    }
}

