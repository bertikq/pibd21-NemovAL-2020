namespace AbstractShopView.UIForms
{
    partial class FormDateOrders
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
            this.buttonInExcel = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.tourColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.componentColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonGenerateData = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonInExcel
            // 
            this.buttonInExcel.Location = new System.Drawing.Point(611, 14);
            this.buttonInExcel.Name = "buttonInExcel";
            this.buttonInExcel.Size = new System.Drawing.Size(92, 23);
            this.buttonInExcel.TabIndex = 3;
            this.buttonInExcel.Text = "В Excel";
            this.buttonInExcel.UseVisualStyleBackColor = true;
            this.buttonInExcel.Click += new System.EventHandler(this.ButtonToExcel_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.tourColumn,
            this.componentColumn,
            this.countColumn});
            this.dataGridView.Location = new System.Drawing.Point(12, 49);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(776, 389);
            this.dataGridView.TabIndex = 4;
            // 
            // tourColumn
            // 
            this.tourColumn.HeaderText = "Дата";
            this.tourColumn.MinimumWidth = 6;
            this.tourColumn.Name = "tourColumn";
            this.tourColumn.Width = 125;
            // 
            // componentColumn
            // 
            this.componentColumn.HeaderText = "Тур";
            this.componentColumn.MinimumWidth = 6;
            this.componentColumn.Name = "componentColumn";
            this.componentColumn.Width = 125;
            // 
            // countColumn
            // 
            this.countColumn.HeaderText = "Сумма";
            this.countColumn.MinimumWidth = 6;
            this.countColumn.Name = "countColumn";
            this.countColumn.Width = 125;
            // 
            // buttonGenerateData
            // 
            this.buttonGenerateData.Location = new System.Drawing.Point(478, 14);
            this.buttonGenerateData.Name = "buttonGenerateData";
            this.buttonGenerateData.Size = new System.Drawing.Size(115, 23);
            this.buttonGenerateData.TabIndex = 2;
            this.buttonGenerateData.Text = "Сформировать";
            this.buttonGenerateData.UseVisualStyleBackColor = true;
            this.buttonGenerateData.Click += new System.EventHandler(this.ButtonMake_Click);
            // 
            // FormDateOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonInExcel);
            this.Controls.Add(this.buttonGenerateData);
            this.Name = "FormDateOrders";
            this.Text = "Отчет по складам и компонентам";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonInExcel;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn tourColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn componentColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countColumn;
        private System.Windows.Forms.Button buttonGenerateData;
    }
}