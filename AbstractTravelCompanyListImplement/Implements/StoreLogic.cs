using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using AbstractTravelCompanyListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractTravelCompanyListImplement.Implements
{
    public class StoreLogic : IStoreLogic
    {
        private readonly DataListSingleton source;
        public StoreLogic()
        {
            source = DataListSingleton.GetInstance();
        }

        public void AddComponent(AddComponentInStoreBindingModel model)
        {
            StoreComponent storeComponent = source.StoreComponents.FirstOrDefault(x => 
                x.StoreId == model.StoreId && x.ComponentId == model.ComponentId);

            if (storeComponent == null)
            {
                int newId = 0;
                while (true)
                {
                    bool isNewId = true;
                    foreach (StoreComponent sc in source.StoreComponents)
                    {
                        if (sc.Id == newId)
                        {
                            newId++;
                            isNewId = false;
                            break;
                        }
                    }

                    if (isNewId)
                    {
                        source.StoreComponents.Add(new StoreComponent
                        {
                            Id = newId,
                            Count = model.Count,
                            ComponentId = model.ComponentId,
                            StoreId = model.StoreId
                        });
                        return;
                    }
                }
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
    }
}
