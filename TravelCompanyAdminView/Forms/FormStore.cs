using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TravelCompanyAdminView.Forms
{
    public partial class FormStore : Form
    {
        public int Id { set { id = value; } }

        private int? id;

        private List<StoreComponentViewModel> storeComponents;
        public FormStore()
        {
            InitializeComponent();
        }

        private void FormStore_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    var view = APIAdmin.GetRequest<List<StoreApiViewModel>>($"api/store/ReadStoreById?storeId={id.Value}")?[0];

                    if (view != null)
                    {
                        textBoxName.Text = view.Name;
                        storeComponents = view.StoreComponents;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                storeComponents = new List<StoreComponentViewModel>();
            }
        }

        private void LoadData()
        {
            try
            {
                if (storeComponents != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in storeComponents)
                    {
                        dataGridView.Rows.Add(new object[] { pc.ComponentId, pc.ComponentName, pc.CountComponent });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                APIAdmin.PostRequest("api/store/CreateOrUpdate", new StoreBindingModel
                {
                    Id = id,
                    Name = textBoxName.Text
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
