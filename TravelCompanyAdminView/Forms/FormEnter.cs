using System;
using System.Windows.Forms;
using TravelCompanyAdminView;

namespace TravelCompanyClientView.Forms
{
    public partial class FormEnter : Form
    {
        private readonly string password;
        public FormEnter(string password)
        {
            InitializeComponent();
            this.password = password;
        }
        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxPassword.Text))
            {
                if (password == textBoxPassword.Text)
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
