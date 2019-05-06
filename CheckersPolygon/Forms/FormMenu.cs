using CheckersPolygon.Controllers;
using CheckersPolygon.Forms;
using CheckersPolygon.GameObjects;
using CheckersPolygon.Helpers;
using CheckersPolygon.Helpers.Enums;
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

namespace CheckersPolygon
{
    public partial class FormMenu : Form, ILocalizable
    {
        public bool aiAffected; // Does AI influence the game?
        private bool firstStarted; // Is the form open for the first time?
        public bool FirstStarted
        {
            get { return firstStarted; }
            set
            {
                // If the form is opened for the first time - block the buttons "continue" and "save"
                if (value == true)
                {
                    btnContinue.Enabled = false;
                    btnSaveGame.Enabled = false;
                }
                else
                {
                    btnContinue.Enabled = true;
                    btnSaveGame.Enabled = true;
                }
                firstStarted = value;
            }
        }

        // Redraw all text on every form
        public event Action OnLocalizationChanged;

        // Decorations
        Pawn decorativePawn = new Pawn(Helpers.Enums.CheckerSide.White, Helpers.Enums.CheckerMoveDirection.Downstairs,
                new Helpers.CheckersCoordinateSet()
                {
                    ScreenPosition = new Point(40, 20),
                    DrawableSize = new Size(60, 60)
                });
        King decorativeKing = new King(Helpers.Enums.CheckerSide.Black,
                new Helpers.CheckersCoordinateSet()
                {
                    ScreenPosition = new Point(450, 20),
                    DrawableSize = new Size(60, 60)
                });
        BoardCell[] decorativeBoardCell = new BoardCell[4]
            {
                new BoardCell(false,
                    new Helpers.CheckersCoordinateSet()
                {
                    ScreenPosition = new Point(20, 200),
                    DrawableSize = new Size(60, 60)
                }),
                new BoardCell(true,
                    new Helpers.CheckersCoordinateSet()
                {
                    ScreenPosition = new Point(80, 260),
                    DrawableSize = new Size(60, 60)
                }),
                new BoardCell(true,
                    new Helpers.CheckersCoordinateSet()
                {
                    ScreenPosition = new Point(400, 260),
                    DrawableSize = new Size(60, 60)
                }),
                new BoardCell(false,
                    new Helpers.CheckersCoordinateSet()
                {
                    ScreenPosition = new Point(460, 200),
                    DrawableSize = new Size(60, 60)
                })
            };


        public FormMenu(bool firstStarted = false)
        {
            InitializeComponent();
            OnLocalizationChanged += LoadLocalizedText;
            this.FirstStarted = firstStarted;
            AddLocalizations();
            LoadLocalizedText();
        }

        /* Adding the text locales
         */
        private void AddLocalizations()
        {
            cbLanguage.Items.Clear();
            foreach (string lang in Helpers.Constants.localizations.Keys)
                cbLanguage.Items.Add(lang);
            cbLanguage.SelectedIndex = (int)Helpers.Constants.currentLanguage;
        }

        /* Reload the text on buttons
         */
        public void LoadLocalizedText()
        {
            btnContinue.Text = Helpers.Constants.localized.textMenuContinue;
            btnLoadGame.Text = Helpers.Constants.localized.textMenuLoadGame;
            btnNewGame.Text = Helpers.Constants.localized.textMenuNewGame;
            btnSaveGame.Text = Helpers.Constants.localized.textMenuSaveGame;
            btnExit.Text = Helpers.Constants.localized.textMenuExit;
            this.Text = Helpers.Constants.localized.textMenuTitle;
            lblCheckers.Text = Helpers.Constants.localized.textMenuTitle;
        }

        /* The button "new game" is pressed
         */
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            FormModeMenu formMode = new FormModeMenu();
            DialogResult result = formMode.ShowDialog();
            FormMain formMain;
            switch (result)
            {
                case DialogResult.OK:
                    aiAffected = false;
                    if (firstStarted)
                    {
                        formMain = new FormMain(false);
                        OnLocalizationChanged += formMain.LoadLocalizedText;
                        formMain.FormClosing += delegate { OnLocalizationChanged -= formMain.LoadLocalizedText; };
                        formMain.Show();
                    }
                    this.Hide();
                    break;
                case DialogResult.Yes:
                    aiAffected = true;
                    if (firstStarted)
                    {
                        formMain = new FormMain(true);
                        OnLocalizationChanged += formMain.LoadLocalizedText;
                        formMain.FormClosing += delegate { OnLocalizationChanged -= formMain.LoadLocalizedText; };
                        formMain.Show();
                    }
                    this.Hide();
                    break;
                default:
                    break;
            }
        }

        /* The "exit" button is pressed.
         */
        private void btnExit_Click(object sender, EventArgs e)
        {
            Program.mainMenu.Close();
        }

        /* Redrawing the form
         */
        private void FormMenu_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = this.CreateGraphics();
            decorativePawn.Draw(graphics);
            decorativeKing.Draw(graphics);
            for (int i = 0; i < 4; i++) decorativeBoardCell[i].Draw(graphics);
        }

        /* The "load game" button is pressed
         */
        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            if (firstStarted)
            {
                FormMain formMain = new FormMain(false);
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = "Polygon checkers save file|*.plchckr";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    Game.Load(openDialog.FileName);
                    formMain.Show();
                    this.Hide();
                }
            }
        }

        /* Language is selected
         */
        private void cbLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            Helpers.Constants.LoadLocalization(Helpers.Constants.localizations[cbLanguage.Items[cbLanguage.SelectedIndex].ToString()]);
            OnLocalizationChanged();
            Properties.Settings.Default.Language = (int)Helpers.Constants.currentLanguage;
            Properties.Settings.Default.Save();
            //LoadLocalizedText();
        }
    }
}
