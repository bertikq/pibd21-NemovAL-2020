using AbstractShopBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;

namespace AbstractShopView.UIForms
{
    public partial class FormTour : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id { set { id = value; } }

        private readonly ITourLogic logic;

        private int? id;

        private Dictionary<int, (string, int)> productComponents;
        public FormTour(ITourLogic service)
        {
            InitializeComponent();
            this.logic = service;
        }
        private void FormProduct_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    TourViewModel view = logic.Read(new TourBindingModel
                    {
                        Id = id.Value
                    })?[0];

                    if (view != null)
                    {
                        textBoxName.Text = view.TourName;
                        textBoxPrice.Text = view.Price.ToString();
                        productComponents = view.ProductComponents;
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
                productComponents = new Dictionary<int, (string, int)>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (productComponents != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var pc in productComponents)
                    {
                        dataGridView.Rows.Add(new object[] { pc.Key, pc.Value.Item1, pc.Value.Item2 });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormTourComponent>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (productComponents.ContainsKey(form.Id))
                {
                    productComponents[form.Id] = (form.ComponentName, form.Count);
                }
                else
                {
                    productComponents.Add(form.Id, (form.ComponentName, form.Count));
                }
                LoadData();
            }
        }

        private void ButtonChange_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = Container.Resolve<FormTourComponent>();
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                form.Id = id;
                form.Count = productComponents[id].Item2;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    productComponents[form.Id] = (form.ComponentName, form.Count);
                    LoadData();
                }
            }
        }

        private void ButtonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {

                        productComponents.Remove(Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                       MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }

        private void ButtonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (productComponents == null || productComponents.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                logic.CreateOrUpdate(new TourBindingModel
                {
                    Id = id,
                    TourName = textBoxName.Text,
                    Price = Convert.ToDecimal(textBoxPrice.Text),
                    TourComponents = productComponents
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

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
