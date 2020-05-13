using AbstractShopBusinessLogic.BusinessLogics;
using AbstractShopView.UIForms;
using AbstractTravelCompanyFileImplement.Implements;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace AbstractShopView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IComponentLogic, ComponentLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITourLogic, TourLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStoreLogic, StoreLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<MainLogic>(new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
