namespace AbstractShopView.UIForms
{
    partial class FormReportTourComponent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ReportToursComponentsViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ReportToursComponentsViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 31);
            this.button1.TabIndex = 0;
            this.button1.Text = "В pdf";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.ButtonSaveToPdf_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(649, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(136, 31);
            this.button2.TabIndex = 4;
            this.button2.Text = "Сформировать";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.FormReportProductComponents_Load);
            // 
            // reportViewer
            //
            this.reportViewer.LocalReport.ReportEmbeddedResource = "AbstractShopView.ReportTours.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(12, 49);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(773, 378);
            this.reportViewer.TabIndex = 5;
            // 
            // ReportToursComponentsViewModelBindingSource
            // 
            this.ReportToursComponentsViewModelBindingSource.DataSource = typeof(AbstractTravelCompanyBusinessLogic.ViewModels.ReportToursComponentsViewModel);
            // 
            // FormReportTourComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 450);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "FormReportTourComponent";
            this.Text = "FormReportTourComponent";
            ((System.ComponentModel.ISupportInitialize)(this.ReportToursComponentsViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource ReportToursComponentsViewModelBindingSource;
    }
}