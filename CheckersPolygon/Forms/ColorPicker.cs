using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersPolygon.Forms
{
    /* Color picker ui element
     */
    public partial class ColorPicker : UserControl
    {
        // Text on top of picker
        public string HeaderText
        {
            get => lblText.Text;
            set => lblText.Text = value;
        }

        // Picked color
        public Color Color
        {
            get => cbColorButton.BackColor;
            set => cbColorButton.BackColor = value;
        }

        public ColorPicker()
        {
            InitializeComponent();
        }

        /* Pick checkbox click handler
         */
        private void CbColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Color = dialog.Color;
            }
        }
    }
}
