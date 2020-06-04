using AbstractTravelCompanyBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TravelCompanyClientView.Forms
{
    public partial class FormMail : Form
    {
        public FormMail()
        {
            InitializeComponent();
        }

        private void FormMail_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = APIClient.GetRequest<List<MessageInfoViewModel>>($"api/client/ReadMessage?clientId={Program.Client.Id}");
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
    }
}
