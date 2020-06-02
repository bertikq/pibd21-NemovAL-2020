using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TravelCompanyRestApi.Models;

namespace TravelCompanyAdminView.Forms
{
    public partial class FormAddComponentInStore : Form
    {
        public FormAddComponentInStore()
        {
            InitializeComponent();
        }

        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                var stores = APIAdmin.GetRequest<List<StoreModel>>($"api/store/Read");
                if (stores != null)
                {
                    comboBoxStores.DisplayMember = "Name";
                    comboBoxStores.ValueMember = "Id";
                    comboBoxStores.DataSource = stores;
                    comboBoxStores.SelectedItem = null;
                }

                List<ComponentViewModel> components = APIAdmin.GetRequest<List<ComponentViewModel>>("api/component/Read");
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

            if (comboBoxComponents.SelectedItem == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStores.SelectedItem == null)
            {
                MessageBox.Show("Выберите хранилище", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }

            APIAdmin.PostRequest("api/main/AddComponentInStore", new AddComponentInStoreBindingModel
            {
                Count = int.Parse(textBoxCount.Text),
                ComponentId = ((ComponentViewModel)comboBoxComponents.SelectedItem).Id,
                StoreId = ((StoreModel)comboBoxStores.SelectedItem).Id
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
