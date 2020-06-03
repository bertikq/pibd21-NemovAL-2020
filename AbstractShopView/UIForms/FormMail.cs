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

namespace AbstractShopView.UIForms
{
    public partial class FormMail : Form
    {
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
            try
            {
                var list = messageInfoLogic.Read(null);
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
