namespace CheckersPolygon.Forms
{
    partial class FormSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSettings));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.cpActiveCell = new CheckersPolygon.Forms.ColorPicker();
            this.cpInactiveCell = new CheckersPolygon.Forms.ColorPicker();
            this.cpMarker = new CheckersPolygon.Forms.ColorPicker();
            this.cpWhiteChecker = new CheckersPolygon.Forms.ColorPicker();
            this.cpBlackChecker = new CheckersPolygon.Forms.ColorPicker();
            this.cpHighlightedCell = new CheckersPolygon.Forms.ColorPicker();
            this.cpHighlightedChecker = new CheckersPolygon.Forms.ColorPicker();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDefaults = new System.Windows.Forms.Button();
            this.btnDecline = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 148F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(902, 579);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(896, 80);
            this.panel1.TabIndex = 3;
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Century Gothic", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblHeader.ForeColor = System.Drawing.Color.Gold;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(896, 80);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "Настройки";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.cpActiveCell);
            this.flowLayoutPanel1.Controls.Add(this.cpInactiveCell);
            this.flowLayoutPanel1.Controls.Add(this.cpMarker);
            this.flowLayoutPanel1.Controls.Add(this.cpWhiteChecker);
            this.flowLayoutPanel1.Controls.Add(this.cpBlackChecker);
            this.flowLayoutPanel1.Controls.Add(this.cpHighlightedCell);
            this.flowLayoutPanel1.Controls.Add(this.cpHighlightedChecker);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 89);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(896, 338);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // cpActiveCell
            // 
            this.cpActiveCell.Color = System.Drawing.Color.Black;
            this.cpActiveCell.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cpActiveCell.HeaderText = "Active cell color";
            this.cpActiveCell.Location = new System.Drawing.Point(10, 10);
            this.cpActiveCell.Margin = new System.Windows.Forms.Padding(10);
            this.cpActiveCell.Name = "cpActiveCell";
            this.cpActiveCell.Size = new System.Drawing.Size(138, 53);
            this.cpActiveCell.TabIndex = 0;
            // 
            // cpInactiveCell
            // 
            this.cpInactiveCell.Color = System.Drawing.Color.Bisque;
            this.cpInactiveCell.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cpInactiveCell.HeaderText = "Inactive cell color";
            this.cpInactiveCell.Location = new System.Drawing.Point(168, 10);
            this.cpInactiveCell.Margin = new System.Windows.Forms.Padding(10);
            this.cpInactiveCell.Name = "cpInactiveCell";
            this.cpInactiveCell.Size = new System.Drawing.Size(168, 53);
            this.cpInactiveCell.TabIndex = 1;
            // 
            // cpMarker
            // 
            this.cpMarker.Color = System.Drawing.Color.Brown;
            this.cpMarker.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cpMarker.HeaderText = "Marker color";
            this.cpMarker.Location = new System.Drawing.Point(356, 10);
            this.cpMarker.Margin = new System.Windows.Forms.Padding(10);
            this.cpMarker.Name = "cpMarker";
            this.cpMarker.Size = new System.Drawing.Size(152, 53);
            this.cpMarker.TabIndex = 2;
            // 
            // cpWhiteChecker
            // 
            this.cpWhiteChecker.Color = System.Drawing.Color.Coral;
            this.cpWhiteChecker.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cpWhiteChecker.HeaderText = "White checker color";
            this.cpWhiteChecker.Location = new System.Drawing.Point(528, 10);
            this.cpWhiteChecker.Margin = new System.Windows.Forms.Padding(10);
            this.cpWhiteChecker.Name = "cpWhiteChecker";
            this.cpWhiteChecker.Size = new System.Drawing.Size(173, 53);
            this.cpWhiteChecker.TabIndex = 3;
            // 
            // cpBlackChecker
            // 
            this.cpBlackChecker.Color = System.Drawing.Color.Crimson;
            this.cpBlackChecker.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cpBlackChecker.HeaderText = "Black checker color";
            this.cpBlackChecker.Location = new System.Drawing.Point(10, 83);
            this.cpBlackChecker.Margin = new System.Windows.Forms.Padding(10);
            this.cpBlackChecker.Name = "cpBlackChecker";
            this.cpBlackChecker.Size = new System.Drawing.Size(171, 53);
            this.cpBlackChecker.TabIndex = 4;
            // 
            // cpHighlightedCell
            // 
            this.cpHighlightedCell.Color = System.Drawing.Color.LimeGreen;
            this.cpHighlightedCell.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cpHighlightedCell.HeaderText = "Highlighted cell color";
            this.cpHighlightedCell.Location = new System.Drawing.Point(201, 83);
            this.cpHighlightedCell.Margin = new System.Windows.Forms.Padding(10);
            this.cpHighlightedCell.Name = "cpHighlightedCell";
            this.cpHighlightedCell.Size = new System.Drawing.Size(203, 53);
            this.cpHighlightedCell.TabIndex = 5;
            // 
            // cpHighlightedChecker
            // 
            this.cpHighlightedChecker.Color = System.Drawing.Color.Maroon;
            this.cpHighlightedChecker.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.cpHighlightedChecker.HeaderText = "Highlighted checker color";
            this.cpHighlightedChecker.Location = new System.Drawing.Point(424, 83);
            this.cpHighlightedChecker.Margin = new System.Windows.Forms.Padding(10);
            this.cpHighlightedChecker.Name = "cpHighlightedChecker";
            this.cpHighlightedChecker.Size = new System.Drawing.Size(204, 53);
            this.cpHighlightedChecker.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDefaults);
            this.panel2.Controls.Add(this.btnDecline);
            this.panel2.Controls.Add(this.btnAccept);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 433);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(896, 143);
            this.panel2.TabIndex = 4;
            // 
            // btnDefaults
            // 
            this.btnDefaults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDefaults.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDefaults.Location = new System.Drawing.Point(45, 45);
            this.btnDefaults.Name = "btnDefaults";
            this.btnDefaults.Size = new System.Drawing.Size(136, 58);
            this.btnDefaults.TabIndex = 2;
            this.btnDefaults.Text = "Defaults";
            this.btnDefaults.UseVisualStyleBackColor = true;
            this.btnDefaults.Click += new System.EventHandler(this.BtnDefaults_Click);
            // 
            // btnDecline
            // 
            this.btnDecline.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDecline.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDecline.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnDecline.Location = new System.Drawing.Point(478, 45);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(136, 58);
            this.btnDecline.TabIndex = 1;
            this.btnDecline.Text = "Decline";
            this.btnDecline.UseVisualStyleBackColor = true;
            // 
            // btnAccept
            // 
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccept.ForeColor = System.Drawing.Color.GreenYellow;
            this.btnAccept.Location = new System.Drawing.Point(660, 45);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(136, 58);
            this.btnAccept.TabIndex = 0;
            this.btnAccept.Text = "Accept";
            this.btnAccept.UseVisualStyleBackColor = true;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(902, 579);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FormSettings";
            this.Text = "Настройки";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnDefaults;
        private System.Windows.Forms.Button btnDecline;
        public ColorPicker cpActiveCell;
        public ColorPicker cpInactiveCell;
        public ColorPicker cpMarker;
        public ColorPicker cpWhiteChecker;
        public ColorPicker cpBlackChecker;
        public ColorPicker cpHighlightedCell;
        public ColorPicker cpHighlightedChecker;
    }
}