using CheckersPolygon.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersPolygon.Forms
{
    /* The form shows a dialog box with a message about winning or losing
     */
    public partial class FormVictory : Form
    {
        /* Arguments are winner side and "enemy has no more moves" flag
         */
        public FormVictory(bool whiteSide, bool toilet)
        {
            InitializeComponent();
            if (!whiteSide)
            {
                if (!toilet) lblMessage.Text = Constants.localized.logBlackWon;
                else
                {
                    lblMessage.Font = new Font("Century Gothic", 24);
                    lblMessage.Text = Constants.localized.logWhiteToilet;
                }
                lblMessage.ForeColor = Color.LawnGreen;
                btnOK.ForeColor = Color.LawnGreen;
            }
            else
            {
                if (!toilet)
                    lblMessage.Text = Constants.localized.logWhiteWon;
                else
                {
                    lblMessage.Font = new Font("Century Gothic", 24);
                    lblMessage.Text = lblMessage.Text = Constants.localized.logBlackToilet;
                }
            }
        }

        private void FormVictory_Load(object sender, EventArgs e)
        {

        }
    }
}
