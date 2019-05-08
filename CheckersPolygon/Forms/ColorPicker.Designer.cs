namespace CheckersPolygon.Forms
{
    partial class ColorPicker
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbColorButton = new System.Windows.Forms.ComboBox();
            this.lblText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbColorButton
            // 
            this.cbColorButton.BackColor = System.Drawing.SystemColors.HotTrack;
            this.cbColorButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cbColorButton.FormattingEnabled = true;
            this.cbColorButton.Location = new System.Drawing.Point(0, 18);
            this.cbColorButton.Name = "cbColorButton";
            this.cbColorButton.Size = new System.Drawing.Size(136, 21);
            this.cbColorButton.TabIndex = 0;
            this.cbColorButton.Click += new System.EventHandler(this.CbColorButton_Click);
            // 
            // lblText
            // 
            this.lblText.AutoSize = true;
            this.lblText.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblText.Location = new System.Drawing.Point(0, 0);
            this.lblText.Name = "lblText";
            this.lblText.Size = new System.Drawing.Size(59, 13);
            this.lblText.TabIndex = 1;
            this.lblText.Text = "Name here";
            this.lblText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ColorPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.cbColorButton);
            this.Name = "ColorPicker";
            this.Size = new System.Drawing.Size(136, 39);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbColorButton;
        private System.Windows.Forms.Label lblText;
    }
}
