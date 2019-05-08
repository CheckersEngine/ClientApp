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
    /* Settings form
     */
    public partial class FormSettings : Form, ILocalizable
    {
        public FormSettings()
        {
            InitializeComponent();

            LoadLocalizedText();
            LoadActualColors();
        }

        /* Set color picker colors to actual values
         */
        private void LoadActualColors()
        {
            cpActiveCell.Color = Helpers.Constants.colorScheme.ActiveCellColor;
            cpInactiveCell.Color = Helpers.Constants.colorScheme.PassiveCellColor;
            cpMarker.Color = Helpers.Constants.colorScheme.BoardMarkerColor;
            cpWhiteChecker.Color = Helpers.Constants.colorScheme.WhiteCheckerColor;
            cpBlackChecker.Color = Helpers.Constants.colorScheme.BlackCheckerColor;
            cpHighlightedCell.Color = Helpers.Constants.colorScheme.HighlightCellColor;
            cpHighlightedChecker.Color = Helpers.Constants.colorScheme.HighlightCheckerColor;
        }

        /* Set form text using current localization
         */
        public void LoadLocalizedText()
        {
            this.Text = Helpers.Constants.localized.textSettingsHeader;
            lblHeader.Text = Helpers.Constants.localized.textSettingsHeader;
            cpActiveCell.HeaderText = Helpers.Constants.localized.textSettingsActiveCellColor;
            cpInactiveCell.HeaderText = Helpers.Constants.localized.textSettingsPassiveCellColor;
            cpMarker.HeaderText = Helpers.Constants.localized.textSettingsBoardMarkerColor;
            cpWhiteChecker.HeaderText = Helpers.Constants.localized.textSettingsWhiteCheckerColor;
            cpBlackChecker.HeaderText = Helpers.Constants.localized.textSettingsBlackCheckerColor;
            cpHighlightedCell.HeaderText = Helpers.Constants.localized.textSettingsHighlightCellColor;
            cpHighlightedChecker.HeaderText = Helpers.Constants.localized.textSettingsHighlightCheckerColor;
            btnAccept.Text = Helpers.Constants.localized.textSettingsAccept;
            btnDecline.Text = Helpers.Constants.localized.textSettingsDecline;
            btnDefaults.Text = Helpers.Constants.localized.textSettingsDefault;
        }

        /* "Defaults" button click handler
         */
        private void BtnDefaults_Click(object sender, EventArgs e)
        {
            cpActiveCell.Color = Color.Black;
            cpInactiveCell.Color = Color.Bisque;
            cpMarker.Color = Color.Brown;
            cpWhiteChecker.Color = Color.Coral;
            cpBlackChecker.Color = Color.Crimson;
            cpHighlightedCell.Color = Color.LimeGreen;
            cpHighlightedChecker.Color = Color.Maroon;
        }
    }
}
