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
    /* Форма показывает диалоговое окно с сообщением о победе или поражении
     */
    public partial class FormVictory : Form
    {
        /* Передается в качестве параметра победившая сторона и поставлен ли "сортир"
         */
        public FormVictory(bool whiteSide, bool toilet)
        {
            InitializeComponent();
            if (!whiteSide)
            {
                if (!toilet) lblMessage.Text = "Черные победили!";
                else
                {
                    lblMessage.Font = new Font("Century Gothic", 24);
                    lblMessage.Text = "Позорное поражение белых";
                }
                lblMessage.ForeColor = Color.LawnGreen;
                btnOK.ForeColor = Color.LawnGreen;
            }
            else if (toilet)
            {
                lblMessage.Font = new Font("Century Gothic", 24);
                lblMessage.Text = "Позорное поражение черных";
            }

            
        }

        private void FormVictory_Load(object sender, EventArgs e)
        {

        }
    }
}
