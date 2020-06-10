namespace AbstractShopView.UIForms
{
    partial class FormSettingManager
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
            this.labelTimePause = new System.Windows.Forms.Label();
            this.textBoxTimeWork = new System.Windows.Forms.TextBox();
            this.labelTimeWork = new System.Windows.Forms.Label();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxFIO = new System.Windows.Forms.TextBox();
            this.labelFIO = new System.Windows.Forms.Label();
            this.textBoxTimePause = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelTimePause
            // 
            this.labelTimePause.AutoSize = true;
            this.labelTimePause.Location = new System.Drawing.Point(22, 77);
            this.labelTimePause.Name = "labelTimePause";
            this.labelTimePause.Size = new System.Drawing.Size(94, 17);
            this.labelTimePause.TabIndex = 23;
            this.labelTimePause.Text = "Время паузы";
            // 
            // textBoxTimeWork
            // 
            this.textBoxTimeWork.Location = new System.Drawing.Point(132, 49);
            this.textBoxTimeWork.Name = "textBoxTimeWork";
            this.textBoxTimeWork.Size = new System.Drawing.Size(261, 22);
            this.textBoxTimeWork.TabIndex = 21;
            // 
            // labelTimeWork
            // 
            this.labelTimeWork.AutoSize = true;
            this.labelTimeWork.Location = new System.Drawing.Point(23, 49);
            this.labelTimeWork.Name = "labelTimeWork";
            this.labelTimeWork.Size = new System.Drawing.Size(103, 17);
            this.labelTimeWork.TabIndex = 20;
            this.labelTimeWork.Text = "Время работы";
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(175, 120);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(100, 30);
            this.buttonSave.TabIndex = 19;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(281, 120);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(100, 30);
            this.buttonCancel.TabIndex = 18;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxFIO
            // 
            this.textBoxFIO.Location = new System.Drawing.Point(132, 23);
            this.textBoxFIO.Name = "textBoxFIO";
            this.textBoxFIO.Size = new System.Drawing.Size(261, 22);
            this.textBoxFIO.TabIndex = 17;
            // 
            // labelFIO
            // 
            this.labelFIO.AutoSize = true;
            this.labelFIO.Location = new System.Drawing.Point(23, 23);
            this.labelFIO.Name = "labelFIO";
            this.labelFIO.Size = new System.Drawing.Size(42, 17);
            this.labelFIO.TabIndex = 16;
            this.labelFIO.Text = "ФИО";
            // 
            // textBoxTimePause
            // 
            this.textBoxTimePause.Location = new System.Drawing.Point(132, 77);
            this.textBoxTimePause.Name = "textBoxTimePause";
            this.textBoxTimePause.Size = new System.Drawing.Size(261, 22);
            this.textBoxTimePause.TabIndex = 24;
            // 
            // FormSettingManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 163);
            this.Controls.Add(this.textBoxTimePause);
            this.Controls.Add(this.labelTimePause);
            this.Controls.Add(this.textBoxTimeWork);
            this.Controls.Add(this.labelTimeWork);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxFIO);
            this.Controls.Add(this.labelFIO);
            this.Name = "FormSettingManager";
            this.Text = "Настройка менеджера";
            this.Load += new System.EventHandler(this.FormSettingManager_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTimePause;
        private System.Windows.Forms.TextBox textBoxTimeWork;
        private System.Windows.Forms.Label labelTimeWork;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TextBox textBoxFIO;
        private System.Windows.Forms.Label labelFIO;
        private System.Windows.Forms.TextBox textBoxTimePause;
    }
}