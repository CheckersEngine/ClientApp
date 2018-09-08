namespace CheckersPolygon
{
    partial class FormMain
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.gameBoard = new System.Windows.Forms.Panel();
            this.rtbUserLog = new System.Windows.Forms.RichTextBox();
            this.btnMenu = new System.Windows.Forms.Button();
            this.panelLeftDock = new System.Windows.Forms.Panel();
            this.panelLeftDock.SuspendLayout();
            this.SuspendLayout();
            // 
            // gameBoard
            // 
            this.gameBoard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gameBoard.BackColor = System.Drawing.Color.Maroon;
            this.gameBoard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gameBoard.Location = new System.Drawing.Point(12, 12);
            this.gameBoard.Name = "gameBoard";
            this.gameBoard.Size = new System.Drawing.Size(566, 438);
            this.gameBoard.TabIndex = 0;
            this.gameBoard.Click += new System.EventHandler(this.gameBoard_Click);
            this.gameBoard.Paint += new System.Windows.Forms.PaintEventHandler(this.gameBoard_Paint);
            this.gameBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gameBoard_MouseClick);
            this.gameBoard.Resize += new System.EventHandler(this.gameBoard_Resize);
            // 
            // rtbUserLog
            // 
            this.rtbUserLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbUserLog.BackColor = System.Drawing.Color.Gray;
            this.rtbUserLog.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbUserLog.ForeColor = System.Drawing.Color.PeachPuff;
            this.rtbUserLog.Location = new System.Drawing.Point(5, 3);
            this.rtbUserLog.Name = "rtbUserLog";
            this.rtbUserLog.ReadOnly = true;
            this.rtbUserLog.Size = new System.Drawing.Size(125, 366);
            this.rtbUserLog.TabIndex = 1;
            this.rtbUserLog.Text = "";
            // 
            // btnMenu
            // 
            this.btnMenu.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnMenu.ForeColor = System.Drawing.Color.DarkRed;
            this.btnMenu.Location = new System.Drawing.Point(5, 396);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(125, 32);
            this.btnMenu.TabIndex = 2;
            this.btnMenu.Text = "Меню";
            this.btnMenu.UseVisualStyleBackColor = true;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // panelLeftDock
            // 
            this.panelLeftDock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLeftDock.Controls.Add(this.btnMenu);
            this.panelLeftDock.Controls.Add(this.rtbUserLog);
            this.panelLeftDock.Location = new System.Drawing.Point(586, 13);
            this.panelLeftDock.Name = "panelLeftDock";
            this.panelLeftDock.Size = new System.Drawing.Size(138, 437);
            this.panelLeftDock.TabIndex = 3;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(728, 462);
            this.Controls.Add(this.gameBoard);
            this.Controls.Add(this.panelLeftDock);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "FormMain";
            this.Text = "Checkers Polygon 2017.5";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormMain_FormClosed);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.panelLeftDock.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel gameBoard;
        private System.Windows.Forms.RichTextBox rtbUserLog;
        private System.Windows.Forms.Button btnMenu;
        private System.Windows.Forms.Panel panelLeftDock;
    }
}

