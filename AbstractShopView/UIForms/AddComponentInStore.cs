using AbstractShopBusinessLogic.BusinessLogics;
using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace AbstractShopView.UIForms
{
    public partial class AddComponentInStore : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IStoreLogic _storeLogic;
        private readonly IComponentLogic _componentLogic;

        private readonly MainLogic _mainLogic;
        public AddComponentInStore(IStoreLogic storeLogic, IComponentLogic componentLogic, MainLogic mainLogic)
        {
            InitializeComponent();
            _storeLogic = storeLogic;
            _componentLogic = componentLogic;
            _mainLogic = mainLogic;
        }

        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                List<StoreViewModel> stores = _storeLogic.Read(null);
                if (stores != null)
                {
                    comboBoxStores.DisplayMember = "Name";
                    comboBoxStores.ValueMember = "Id";
                    comboBoxStores.DataSource = stores;
                    comboBoxStores.SelectedItem = null;
                }
                
                List<ComponentViewModel> components = _componentLogic.Read(null);
                if (components != null)
                {
                    comboBoxComponents.DisplayMember = "ComponentName";
                    comboBoxComponents.ValueMember = "Id";
                    comboBoxComponents.DataSource = components;
                    comboBoxComponents.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }

            _mainLogic.AddComponentInStore(new AddComponentInStoreBindingModel
            {
                Count = int.Parse(textBoxCount.Text),
                ComponentId = ((ComponentViewModel)comboBoxComponents.SelectedItem).Id,
                StoreId = ((StoreViewModel)comboBoxStores.SelectedItem).Id
            });

            MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
