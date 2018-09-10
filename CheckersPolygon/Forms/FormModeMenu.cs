using CheckersPolygon.Interfaces;
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
    public partial class FormModeMenu : Form, ILocalizable
    {
        public FormModeMenu()
        {
            InitializeComponent();
            LoadLocalizedText();
        }

        /* Reload localized text
        */
        public void LoadLocalizedText()
        {
            btnPlayer.Text = Helpers.Constants.localized.textModePlayer;
            btnComputer.Text = Helpers.Constants.localized.textModeAI;
            btnCancel.Text = Helpers.Constants.localized.textModeCancel;
            this.Text = Helpers.Constants.localized.textModeTitle;
        }
    }
}
