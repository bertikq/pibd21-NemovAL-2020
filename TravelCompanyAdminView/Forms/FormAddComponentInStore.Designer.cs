namespace TravelCompanyAdminView.Forms
{
    partial class FormAddComponentInStore
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
            this.comboBoxStores = new System.Windows.Forms.ComboBox();
            this.labelStores = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.comboBoxComponents = new System.Windows.Forms.ComboBox();
            this.labelCount = new System.Windows.Forms.Label();
            this.labelComponents = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBoxStores
            // 
            this.comboBoxStores.FormattingEnabled = true;
            this.comboBoxStores.Location = new System.Drawing.Point(102, 16);
            this.comboBoxStores.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxStores.Name = "comboBoxStores";
            this.comboBoxStores.Size = new System.Drawing.Size(255, 24);
            this.comboBoxStores.TabIndex = 21;
            // 
            // labelStores
            // 
            this.labelStores.AutoSize = true;
            this.labelStores.Location = new System.Drawing.Point(13, 19);
            this.labelStores.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelStores.Name = "labelStores";
            this.labelStores.Size = new System.Drawing.Size(48, 17);
            this.labelStores.TabIndex = 20;
            this.labelStores.Text = "Склад";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(261, 109);
            this.buttonCancel.Margin = new System.Windows.Forms.Padding(4);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(96, 34);
            this.buttonCancel.TabIndex = 19;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(157, 109);
            this.buttonSave.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(96, 34);
            this.buttonSave.TabIndex = 18;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(102, 79);
            this.textBoxCount.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(255, 22);
            this.textBoxCount.TabIndex = 17;
            // 
            // comboBoxComponents
            // 
            this.comboBoxComponents.FormattingEnabled = true;
            this.comboBoxComponents.Location = new System.Drawing.Point(102, 47);
            this.comboBoxComponents.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxComponents.Name = "comboBoxComponents";
            this.comboBoxComponents.Size = new System.Drawing.Size(255, 24);
            this.comboBoxComponents.TabIndex = 16;
            // 
            // labelCount
            // 
            this.labelCount.AutoSize = true;
            this.labelCount.Location = new System.Drawing.Point(13, 82);
            this.labelCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelCount.Name = "labelCount";
            this.labelCount.Size = new System.Drawing.Size(86, 17);
            this.labelCount.TabIndex = 15;
            this.labelCount.Text = "Количество";
            // 
            // labelComponents
            // 
            this.labelComponents.AutoSize = true;
            this.labelComponents.Location = new System.Drawing.Point(13, 49);
            this.labelComponents.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelComponents.Name = "labelComponents";
            this.labelComponents.Size = new System.Drawing.Size(81, 17);
            this.labelComponents.TabIndex = 14;
            this.labelComponents.Text = "Компонент";
            // 
            // AddComponentInStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 152);
            this.Controls.Add(this.comboBoxStores);
            this.Controls.Add(this.labelStores);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.comboBoxComponents);
            this.Controls.Add(this.labelCount);
            this.Controls.Add(this.labelComponents);
            this.Name = "AddComponentInStore";
            this.Text = "Пополнение склада";
            this.Load += new System.EventHandler(this.FormCreateOrder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxStores;
        private System.Windows.Forms.Label labelStores;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.ComboBox comboBoxComponents;
        private System.Windows.Forms.Label labelCount;
        private System.Windows.Forms.Label labelComponents;
    }
}