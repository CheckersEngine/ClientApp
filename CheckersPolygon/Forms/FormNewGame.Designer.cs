namespace CheckersPolygon.Forms
{
    partial class FormNewGame
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbPlayerAI = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbPlayerHuman = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbPlayerTcpIp = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rbTcpIpHost = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rbTcpIpJoin = new System.Windows.Forms.RadioButton();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.tbSelfAddress = new System.Windows.Forms.TextBox();
            this.lblSelfIp = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblNewGame = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19.79522F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80.20478F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 82F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(889, 433);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.groupBox1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 72);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(883, 275);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbPlayerAI);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 269);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // rbPlayerAI
            // 
            this.rbPlayerAI.AutoSize = true;
            this.rbPlayerAI.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbPlayerAI.Location = new System.Drawing.Point(3, 23);
            this.rbPlayerAI.Name = "rbPlayerAI";
            this.rbPlayerAI.Size = new System.Drawing.Size(282, 25);
            this.rbPlayerAI.TabIndex = 1;
            this.rbPlayerAI.TabStop = true;
            this.rbPlayerAI.Text = "Computer";
            this.rbPlayerAI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbPlayerAI.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbPlayerHuman);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(297, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(288, 269);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // rbPlayerHuman
            // 
            this.rbPlayerHuman.AutoSize = true;
            this.rbPlayerHuman.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbPlayerHuman.Location = new System.Drawing.Point(3, 23);
            this.rbPlayerHuman.Name = "rbPlayerHuman";
            this.rbPlayerHuman.Size = new System.Drawing.Size(282, 25);
            this.rbPlayerHuman.TabIndex = 1;
            this.rbPlayerHuman.TabStop = true;
            this.rbPlayerHuman.Text = "Hotseat game";
            this.rbPlayerHuman.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbPlayerHuman.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.rbPlayerTcpIp);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(591, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(289, 269);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            // 
            // rbPlayerTcpIp
            // 
            this.rbPlayerTcpIp.AutoSize = true;
            this.rbPlayerTcpIp.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbPlayerTcpIp.Location = new System.Drawing.Point(3, 23);
            this.rbPlayerTcpIp.Name = "rbPlayerTcpIp";
            this.rbPlayerTcpIp.Size = new System.Drawing.Size(283, 25);
            this.rbPlayerTcpIp.TabIndex = 1;
            this.rbPlayerTcpIp.TabStop = true;
            this.rbPlayerTcpIp.Text = "TCP/IP game";
            this.rbPlayerTcpIp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbPlayerTcpIp.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.lblSelfIp);
            this.groupBox4.Controls.Add(this.tbSelfAddress);
            this.groupBox4.Controls.Add(this.numericUpDown1);
            this.groupBox4.Controls.Add(this.rbTcpIpHost);
            this.groupBox4.Location = new System.Drawing.Point(6, 57);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(135, 187);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            // 
            // rbTcpIpHost
            // 
            this.rbTcpIpHost.AutoSize = true;
            this.rbTcpIpHost.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbTcpIpHost.Location = new System.Drawing.Point(3, 23);
            this.rbTcpIpHost.Name = "rbTcpIpHost";
            this.rbTcpIpHost.Size = new System.Drawing.Size(129, 25);
            this.rbTcpIpHost.TabIndex = 0;
            this.rbTcpIpHost.TabStop = true;
            this.rbTcpIpHost.Text = "Host";
            this.rbTcpIpHost.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbTcpIpHost.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.textBox1);
            this.groupBox5.Controls.Add(this.numericUpDown2);
            this.groupBox5.Controls.Add(this.rbTcpIpJoin);
            this.groupBox5.Location = new System.Drawing.Point(148, 57);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(135, 187);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            // 
            // rbTcpIpJoin
            // 
            this.rbTcpIpJoin.AutoSize = true;
            this.rbTcpIpJoin.Dock = System.Windows.Forms.DockStyle.Top;
            this.rbTcpIpJoin.Location = new System.Drawing.Point(3, 23);
            this.rbTcpIpJoin.Name = "rbTcpIpJoin";
            this.rbTcpIpJoin.Size = new System.Drawing.Size(129, 25);
            this.rbTcpIpJoin.TabIndex = 0;
            this.rbTcpIpJoin.TabStop = true;
            this.rbTcpIpJoin.Text = "Join";
            this.rbTcpIpJoin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rbTcpIpJoin.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(8, 134);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(120, 27);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            10100,
            0,
            0,
            0});
            // 
            // tbSelfAddress
            // 
            this.tbSelfAddress.Location = new System.Drawing.Point(6, 81);
            this.tbSelfAddress.Name = "tbSelfAddress";
            this.tbSelfAddress.ReadOnly = true;
            this.tbSelfAddress.Size = new System.Drawing.Size(123, 27);
            this.tbSelfAddress.TabIndex = 2;
            this.tbSelfAddress.Text = "xxx.xxx.xxx.xxx";
            // 
            // lblSelfIp
            // 
            this.lblSelfIp.AutoSize = true;
            this.lblSelfIp.Location = new System.Drawing.Point(4, 57);
            this.lblSelfIp.Name = "lblSelfIp";
            this.lblSelfIp.Size = new System.Drawing.Size(128, 21);
            this.lblSelfIp.TabIndex = 3;
            this.lblSelfIp.Text = "Your IP address";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 21);
            this.label1.TabIndex = 4;
            this.label1.Text = "Port";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 21);
            this.label2.TabIndex = 8;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(126, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "Host IP address";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 81);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(123, 27);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "xxx.xxx.xxx.xxx";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(8, 134);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(120, 27);
            this.numericUpDown2.TabIndex = 5;
            this.numericUpDown2.Value = new decimal(new int[] {
            10100,
            0,
            0,
            0});
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblNewGame);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(883, 63);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 353);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(883, 77);
            this.panel2.TabIndex = 2;
            // 
            // lblNewGame
            // 
            this.lblNewGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNewGame.Font = new System.Drawing.Font("Century Gothic", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblNewGame.ForeColor = System.Drawing.Color.Orange;
            this.lblNewGame.Location = new System.Drawing.Point(0, 0);
            this.lblNewGame.Name = "lblNewGame";
            this.lblNewGame.Size = new System.Drawing.Size(883, 63);
            this.lblNewGame.TabIndex = 0;
            this.lblNewGame.Text = "New game";
            this.lblNewGame.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.LimeGreen;
            this.button1.Location = new System.Drawing.Point(742, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 49);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(10, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 49);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // FormNewGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(889, 433);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FormNewGame";
            this.Text = "FormNewGame";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rbTcpIpJoin;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbSelfAddress;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.RadioButton rbTcpIpHost;
        private System.Windows.Forms.RadioButton rbPlayerTcpIp;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbPlayerHuman;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbPlayerAI;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSelfIp;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNewGame;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}