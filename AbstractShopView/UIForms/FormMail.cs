using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.Interfaces;
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
    public partial class FormMail : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMessageInfoLogic messageInfoLogic;
        public FormMail(IMessageInfoLogic messageInfoLogic)
        {
            InitializeComponent();
            this.messageInfoLogic = messageInfoLogic;
        }

        private void FormMail_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            if (string.IsNullOrEmpty(textBoxNumPage.Text))
            {
                MessageBox.Show("Заполните номер страницы", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxCountElements.Text))
            {
                MessageBox.Show("Заполните Количество", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                var list = messageInfoLogic.Read(null, int.Parse(textBoxNumPage.Text), int.Parse(textBoxCountElements.Text));
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
