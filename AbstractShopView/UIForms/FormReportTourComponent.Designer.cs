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
            this.buttonInPdf = new System.Windows.Forms.Button();
            this.buttonGenerate = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ReportToursComponentsViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ReportToursComponentsViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonInPdf
            // 
            this.buttonInPdf.Location = new System.Drawing.Point(12, 12);
            this.buttonInPdf.Name = "buttonInPdf";
            this.buttonInPdf.Size = new System.Drawing.Size(139, 31);
            this.buttonInPdf.TabIndex = 0;
            this.buttonInPdf.Text = "В pdf";
            this.buttonInPdf.UseVisualStyleBackColor = true;
            this.buttonInPdf.Click += new System.EventHandler(this.ButtonSaveToPdf_Click);
            // 
            // buttonGenerate
            // 
            this.buttonGenerate.Location = new System.Drawing.Point(649, 14);
            this.buttonGenerate.Name = "buttonGenerate";
            this.buttonGenerate.Size = new System.Drawing.Size(136, 31);
            this.buttonGenerate.TabIndex = 4;
            this.buttonGenerate.Text = "Сформировать";
            this.buttonGenerate.UseVisualStyleBackColor = true;
            this.buttonGenerate.Click += new System.EventHandler(this.FormReportProductComponents_Load);
            // 
            // reportViewer
            // 
            this.reportViewer.LocalReport.ReportEmbeddedResource = "AbstractShopView.ReportTours.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(12, 49);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.Size = new System.Drawing.Size(773, 378);
            this.reportViewer.TabIndex = 5;
            // 
            // FormReportTourComponent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(797, 450);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.buttonGenerate);
            this.Controls.Add(this.buttonInPdf);
            this.Name = "FormReportTourComponent";
            this.Text = "Отчет туров и их составляющих";
            ((System.ComponentModel.ISupportInitialize)(this.ReportToursComponentsViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonInPdf;
        private System.Windows.Forms.Button buttonGenerate;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource ReportToursComponentsViewModelBindingSource;
    }
}