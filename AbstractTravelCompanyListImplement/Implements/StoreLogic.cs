using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using AbstractTravelCompanyFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractTravelCompanyFileImplement.Implements
{
    public class StoreLogic : IStoreLogic
    {
        private readonly DataListSingleton source;

        private readonly ITourLogic _tourLogic;

        public StoreLogic(ITourLogic tourLogic)
        {
            _tourLogic = tourLogic;
            source = DataListSingleton.GetInstance();
        }

        public void AddComponent(AddComponentInStoreBindingModel model)
        {
            StoreComponent storeComponent = source.StoreComponents.FirstOrDefault(x => 
                x.StoreId == model.StoreId && x.ComponentId == model.ComponentId);

            if (storeComponent == null)
            {

                int? newId = source.Stores.Max(s => (int?)s.Id) + 1;
                if (!newId.HasValue)
                    newId = 0;

                source.StoreComponents.Add(new StoreComponent
                {
                    Id = newId.Value,
                    Count = model.Count,
                    ComponentId = model.ComponentId,
                    StoreId = model.StoreId
                });
                return;
            }

            storeComponent.Count += model.Count;
        }

        public void CreateOrUpdate(StoreBindingModel model)
        {
            Store curStore = source.Stores.FirstOrDefault(x => x.Id == model.Id);

            if (model.Name == "")
                throw new Exception("Название не может быть пустым");

            if (curStore == null)
            {

                if (source.Stores.FirstOrDefault(x => x.Name == model.Name) != null)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }

                int? newId = source.Stores.Max(s => (int?)s.Id) + 1;
                if (!newId.HasValue)
                    newId = 0;

                source.Stores.Add(new Store
                {
                    Id = newId.Value,
                    Name = model.Name
                });
                model.Id = newId;
                return;
            }

            curStore.Name = model.Name;
        }

        public void Delete(StoreBindingModel model)
        {
            Store curStore = source.Stores.FirstOrDefault(x => x.Id == model.Id);

            if (curStore == null)
            {
                throw new Exception("Элемент не найден");
            }

            source.Stores.Remove(curStore);

            for (int i = 0; i < source.StoreComponents.Count; i++)
            {
                if (source.StoreComponents[i].StoreId == curStore.Id)
                {
                    source.StoreComponents.RemoveAt(i);
                }
            }
        }

        public bool IsWriteOffComponents(int componentId, int count)
        {
            int curCount = 0;
            curCount += ((StoreComponent)source.StoreComponents.Where(sc => sc.ComponentId == componentId)).Count;

            if (curCount < count)
            {
                return false;
            }

            return true;
        }

        public List<StoreViewModel> Read(StoreBindingModel model)
        {
            List<StoreViewModel> result = new List<StoreViewModel>();
            foreach (var component in source.Stores)
            {
                if (model != null)
                {
                    if (component.Id == model.Id)
                    {
                        result.Add(CreateStoreViewModel(component));
                        break;
                    }
                    continue;
                }
                result.Add(CreateStoreViewModel(component));
            }
            return result;
        }

        public void WriteOffComponents(int componentId, int count)
        {
            foreach (StoreComponent storeComponent in source.StoreComponents.Where(sc => sc.ComponentId == componentId))
            {
                if (count > 0)
                {
                    if (storeComponent.Count >= count)
                    {
                        storeComponent.Count -= count;
                        count = 0;
                    }
                    else
                    {
                        count -= storeComponent.Count;
                        storeComponent.Count = 0;
                    }
                }
            }

            source.StoreComponents.RemoveAll(sc => sc.Count == 0);
        }

        private StoreViewModel CreateStoreViewModel(Store model)
        {
            StoreViewModel storeViewModel = new StoreViewModel
            {
                Id = model.Id,
                Name = model.Name,
                StoreComponents = new Dictionary<int, (string, int)>()
            };

            foreach(StoreComponent storeComponent in source.StoreComponents)
            {
                if (storeComponent.StoreId == model.Id) {
                    storeViewModel.StoreComponents.Add(
                        storeComponent.ComponentId,
                        (source.Components.FirstOrDefault(x => x.Id == storeComponent.ComponentId).ComponentName,
                        storeComponent.Count));
                }
            }

            return storeViewModel;
        }

        public void WriteOffTour(int tourId, int count)
        {
            var tour = _tourLogic.Read(new TourBindingModel
            {
                Id = tourId
            })?[0];

            if (tour == null)
            {
                throw new Exception("Не найден тур");
            }
            foreach (var componentId in tour.ProductComponents.Keys)
            {
                int countComponent = tour.ProductComponents[componentId].Item2;
                foreach (StoreComponent storeComponent in source.StoreComponents.Where(sc => sc.ComponentId == componentId))
                {
                    if (countComponent > 0)
                    {
                        if (storeComponent.Count >= countComponent)
                        {
                            storeComponent.Count -= countComponent;
                            countComponent = 0;
                        }
                        else
                        {
                            countComponent -= storeComponent.Count;
                            storeComponent.Count = 0;
                        }
                    }
                }

                if (countComponent > 0)
                {
                    throw new Exception("Недостаточно компонентов на складе");
                }
            }

            source.StoreComponents.RemoveAll(x => x.Count == 0);
        }
    }
}
