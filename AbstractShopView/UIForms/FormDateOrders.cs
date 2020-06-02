using AbstractTravelCompanyBusinessLogic.BindingModels;
using AbstractTravelCompanyBusinessLogic.BusinessLogics;
using AbstractTravelCompanyBusinessLogic.Interfaces;
using AbstractTravelCompanyBusinessLogic.ViewModels;
using Microsoft.Reporting.WinForms;
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
    public partial class FormDateOrders : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic reportLogic;
        private readonly IStoreLogic _storeLogic;
        public FormDateOrders(ReportLogic reportLogic, IStoreLogic storeLogic)
        {
            InitializeComponent();
            this.reportLogic = reportLogic;
            _storeLogic = storeLogic;
        }
        private void ButtonMake_Click(object sender, EventArgs e)
        {
            try
            {
                var stores = _storeLogic.Read(null);
                if (stores != null)
                {
                    dataGridView.Rows.Clear();
                    foreach (var store in stores)
                    {
                        dataGridView.Rows.Add(new object[] { store.Name, "", "" });
                        foreach (var storeComponent in store.StoreComponents.Values)
                        {
                            dataGridView.Rows.Add(new object[] { "", storeComponent.Item1, storeComponent.Item2 });
                        }
                        dataGridView.Rows.Add(new object[] { "", "Сумма компонентов", 
                            store.StoreComponents.Values.Sum(x => x.Item2) });
                        dataGridView.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ButtonToExcel_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        reportLogic.SaveOrdersToExcelFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                        });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
