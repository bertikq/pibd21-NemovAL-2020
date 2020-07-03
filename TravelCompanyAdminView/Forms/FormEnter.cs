using System;
using System.Configuration;
using System.Windows.Forms;
using TravelCompanyAdminView;

namespace TravelCompanyClientView.Forms
{
    public partial class FormEnter : Form
    {
        public FormEnter()
        {
            InitializeComponent();
        }
        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxPassword.Text))
            {
                if (ConfigurationManager.AppSettings["Password"] == textBoxPassword.Text)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Не правильный пароль", "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
    }

}
