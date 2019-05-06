using CheckersPolygon.Helpers;
using CheckersPolygon.Helpers.Enums;
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
        public FormVictory(CheckerSide winSide, bool toilet)
        {
            InitializeComponent();
            CustomizeMessageBox(winSide, toilet);
        }

        private void CustomizeMessageBox(CheckerSide winSide, bool toilet)
        {
            switch (winSide)
            {
                case CheckerSide.White:
                    {
                        if (!toilet)
                            lblMessage.Text = Constants.localized.logWhiteWon;
                        else
                        {
                            lblMessage.Font = new Font("Century Gothic", 24);
                            lblMessage.Text = Constants.localized.logWhiteToilet;
                        }
                        break;
                    }
                case CheckerSide.Black:
                    {
                        if (!toilet) lblMessage.Text = Constants.localized.logBlackWon;
                        else
                        {
                            lblMessage.Font = new Font("Century Gothic", 24);
                            lblMessage.Text = Constants.localized.logBlackToilet;
                        }
                        lblMessage.ForeColor = Color.LawnGreen;
                        btnOK.ForeColor = Color.LawnGreen;
                        break;
                    }
            }
        }
    }
}
