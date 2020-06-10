using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using AbstractTravelCompanyDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbstractTravelCompanyDatabaseImplement.Implements
{
    public class ManagerLogic : IManagerLogic
    {
        public void CreateOrUpdate(ManagerBindingModel model)
        {
            using (var context = new DataBaseContext())
            {
                Manager element = context.Managers.FirstOrDefault(c => c.ManagerFIO == model.ManagerFIO && c.Id != model.Id);
                if (element != null)
                {
                    throw new Exception("Уже есть менеджер с таким именем");
                }

                if (model.Id.HasValue)
                {
                    element = context.Managers.FirstOrDefault(rec => rec.Id == model.Id);

                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    else
                    {
                        element.ManagerFIO = model.ManagerFIO;
                        element.PauseTime = model.PauseTime;
                        element.WorkingTime = model.WorkingTime;
                    }
                }
                else
                {
                    element = new Manager
                    {
                        ManagerFIO = model.ManagerFIO,
                        PauseTime = model.PauseTime,
                        WorkingTime = model.WorkingTime
                    };
                    context.Managers.Add(element);
                }

                context.SaveChanges();
            }
        }

        public void Delete(ManagerBindingModel model)
        {
            using (var context = new DataBaseContext())
            {
                Manager element = context.Managers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Managers.Remove(element);
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }

                context.SaveChanges();
            }
        }

        public List<ManagerViewModel> Read(ManagerBindingModel model)
        {
            using (var context = new DataBaseContext())
            {
                return context.Managers
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new ManagerViewModel
                {
                    Id = rec.Id,
                    WorkingTime = rec.WorkingTime,
                    PauseTime = rec.PauseTime,
                    ManagerFIO = rec.ManagerFIO
                })
                .ToList();
            }
        }
    }
}
