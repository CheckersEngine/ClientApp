using CheckersPolygon.Controllers;
using CheckersPolygon.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersPolygon
{
    /* Main game form
     */
    public partial class FormMain : Form, ILocalizable
    {
        bool phase = false; // The phase of the game, it depends on it, which checkers will be at the top and which ones at the bottom
        public bool aiAffected = false;

        public FormMain(bool aiAffected)
        {
            InitializeComponent();
            LoadLocalizedText();
            Game.InitEngine(ref this.gameBoard, ref this.rtbUserLog, phase, aiAffected); // Initializing the game engine
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //Game.InitEngine(ref this.gameBoard, ref this.rtbUserLog, phase, aiAffected); // Initializing the game engine
        }

        private void gameBoard_Click(object sender, EventArgs e)
        {

        }

        /* Redrawing the image
         */
        private void gameBoard_Paint(object sender, PaintEventArgs e)
        {
            Game.drawingController.PrioritizedDraw();
        }

        private void gameBoard_Resize(object sender, EventArgs e)
        {

        }

        /* Redraw the image when changing the size of the form
         */
        private void FormMain_Resize(object sender, EventArgs e)
        {
            Game.gameplayController.OnResize();
        }

        /* Handling the mouse button on the game board
         */
        private void gameBoard_MouseClick(object sender, MouseEventArgs e)
        {
            Game.gameplayController.OnClick(e);
        }

        /* Closing the main menu when this form is closed
         */
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.mainMenu.Close();
        }

        /* Calling the menu by clicking on the button
         */
        private void btnMenu_Click(object sender, EventArgs e)
        {
            FormMenu menu = new FormMenu(false);
            DialogResult result = menu.ShowDialog();

            // Processing the result
            switch (result)
            {
                // Continue
                case DialogResult.Cancel:
                    LoadLocalizedText();
                    break;

                // A new game
                case DialogResult.OK:
                    phase = !phase;
                    Game.InitEngine(ref gameBoard, ref rtbUserLog, phase, menu.aiAffected);
                    if (Game.gameplayController.state.isAiControlled)
                    {
                        if (Game.gameplayController.state.aiSide == Helpers.Enums.CheckerSide.White)
                            Game.gameplayController.AiTurn();
                    }
                    GC.Collect();
                    break;

                // Save game
                case DialogResult.Abort:
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Polygon checkers save file|*.plchckr";
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        Game.Save(saveDialog.FileName);
                    }
                    break;

                // Load game
                case DialogResult.Retry:
                    OpenFileDialog openDialog = new OpenFileDialog();
                    openDialog.Filter = "Polygon checkers save file|*.plchckr";
                    if (openDialog.ShowDialog() == DialogResult.OK)
                    {
                        Game.Load(openDialog.FileName);
                    }
                    break;
                default:
                    break;
            }
        }

        /* Reload localized text
         */
        public void LoadLocalizedText()
        {
            btnMenu.Text = Helpers.Constants.localized.textMainMenu;
        }
    }
}
