namespace TravelCompanyClientView.Forms
{
    partial class FormMail
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.textBoxCountElements = new System.Windows.Forms.TextBox();
            this.labelCountElements = new System.Windows.Forms.Label();
            this.buttonShow = new System.Windows.Forms.Button();
            this.labelNumPage = new System.Windows.Forms.Label();
            this.textBoxNumPage = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(12, 12);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(699, 404);
            this.dataGridView.TabIndex = 5;
            // 
            // textBoxCountElements
            // 
            this.textBoxCountElements.Location = new System.Drawing.Point(720, 53);
            this.textBoxCountElements.Name = "textBoxCountElements";
            this.textBoxCountElements.Size = new System.Drawing.Size(161, 22);
            this.textBoxCountElements.TabIndex = 6;
            this.textBoxCountElements.Text = "100";
            // 
            // labelCountElements
            // 
            this.labelCountElements.AutoSize = true;
            this.labelCountElements.Location = new System.Drawing.Point(717, 16);
            this.labelCountElements.Name = "labelCountElements";
            this.labelCountElements.Size = new System.Drawing.Size(164, 34);
            this.labelCountElements.TabIndex = 7;
            this.labelCountElements.Text = "Количество элементов \r\nна странице";
            // 
            // buttonShow
            // 
            this.buttonShow.Location = new System.Drawing.Point(720, 166);
            this.buttonShow.Name = "buttonShow";
            this.buttonShow.Size = new System.Drawing.Size(161, 32);
            this.buttonShow.TabIndex = 8;
            this.buttonShow.Text = "Показать";
            this.buttonShow.UseVisualStyleBackColor = true;
            this.buttonShow.Click += new System.EventHandler(this.buttonShow_Click);
            // 
            // labelNumPage
            // 
            this.labelNumPage.AutoSize = true;
            this.labelNumPage.Location = new System.Drawing.Point(735, 95);
            this.labelNumPage.Name = "labelNumPage";
            this.labelNumPage.Size = new System.Drawing.Size(119, 17);
            this.labelNumPage.TabIndex = 10;
            this.labelNumPage.Text = "Номер страницы";
            // 
            // textBoxNumPage
            // 
            this.textBoxNumPage.Location = new System.Drawing.Point(720, 115);
            this.textBoxNumPage.Name = "textBoxNumPage";
            this.textBoxNumPage.Size = new System.Drawing.Size(161, 22);
            this.textBoxNumPage.TabIndex = 9;
            this.textBoxNumPage.Text = "0";
            // 
            // FormMail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 423);
            this.Controls.Add(this.labelNumPage);
            this.Controls.Add(this.textBoxNumPage);
            this.Controls.Add(this.buttonShow);
            this.Controls.Add(this.labelCountElements);
            this.Controls.Add(this.textBoxCountElements);
            this.Controls.Add(this.dataGridView);
            this.Name = "FormMail";
            this.Text = "Письма";
            this.Load += new System.EventHandler(this.FormMail_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TextBox textBoxCountElements;
        private System.Windows.Forms.Label labelCountElements;
        private System.Windows.Forms.Button buttonShow;
        private System.Windows.Forms.Label labelNumPage;
        private System.Windows.Forms.TextBox textBoxNumPage;
    }
}