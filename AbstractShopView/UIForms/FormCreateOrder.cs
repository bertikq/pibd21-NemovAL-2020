using AbstractShopBusinessLogic.BindingModels;
using AbstractShopBusinessLogic.BusinessLogics;
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
    public partial class FormCreateOrder : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly ITourLogic logicP;

        private readonly IClientLogic _clientLogic;

        private readonly MainLogic logicM;
        public FormCreateOrder(ITourLogic logicP, MainLogic logicM, IClientLogic clientLogic)
        {
            InitializeComponent();
            this.logicP = logicP;
            this.logicM = logicM;
            _clientLogic = clientLogic;
        }
        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                List<TourViewModel> list = logicP.Read(null);
                if (list != null)
                {
                    comboBoxTour.DisplayMember = "TourName";
                    comboBoxTour.ValueMember = "Id";
                    comboBoxTour.DataSource = list;
                    comboBoxTour.SelectedItem = null;
                }

                List<ClientViewModel> clients = _clientLogic.Read(null);
                if (clients != null)
                {
                    clientComboBox.DisplayMember = "FIO";
                    clientComboBox.ValueMember = "Id";
                    clientComboBox.DataSource = clients;
                    clientComboBox.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxTour.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxTour.SelectedValue);
                    TourViewModel product = logicP.Read(new TourBindingModel
                    {
                        Id = id
                    })?[0];
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSumm.Text = (count * product?.Price ?? 0).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxTour.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                logicM.CreateOrder(new CreateOrderBindingModel
                {
                    TourId = Convert.ToInt32(comboBoxTour.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToDecimal(textBoxSumm.Text),
                    ClientId = Convert.ToInt32(clientComboBox.SelectedValue)
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

        private void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void ComboBoxTour_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
    }
}
