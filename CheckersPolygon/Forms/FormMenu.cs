using CheckersPolygon.Controllers;
using CheckersPolygon.Forms;
using CheckersPolygon.GameObjects;
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
    public partial class FormMenu : Form
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

        // Decorations
        Pawn decorativePawn = new Pawn(Helpers.Enums.CheckerSide.White, Helpers.Enums.CheckerMoveDirection.Downstairs,
                new Helpers.CheckersCoordinateSet()
                {
                    screenPosition = new Point(40, 20),
                    drawableSize = new Size(60, 60)
                });
        King decorativeKing = new King(Helpers.Enums.CheckerSide.Black,
                new Helpers.CheckersCoordinateSet()
                {
                    screenPosition = new Point(450, 20),
                    drawableSize = new Size(60, 60)
                });
        BoardCell[] decorativeBoardCell = new BoardCell[4]
            {
                new BoardCell(false,
                    new Helpers.CheckersCoordinateSet()
                {
                    screenPosition = new Point(20, 200),
                    drawableSize = new Size(60, 60)
                }),
                new BoardCell(true,
                    new Helpers.CheckersCoordinateSet()
                {
                    screenPosition = new Point(80, 260),
                    drawableSize = new Size(60, 60)
                }),
                new BoardCell(true,
                    new Helpers.CheckersCoordinateSet()
                {
                    screenPosition = new Point(400, 260),
                    drawableSize = new Size(60, 60)
                }),
                new BoardCell(false,
                    new Helpers.CheckersCoordinateSet()
                {
                    screenPosition = new Point(460, 200),
                    drawableSize = new Size(60, 60)
                })
            };


        public FormMenu(bool firstStarted)
        {
            InitializeComponent();
            this.FirstStarted = firstStarted;
        }

        public FormMenu()
        {
            InitializeComponent();
            this.FirstStarted = false;
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
                        formMain.Show();
                    }
                    this.Hide();
                    break;
                case DialogResult.Yes:
                    aiAffected = true;
                    if (firstStarted)
                    {
                        formMain = new FormMain(true);
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
    }
}
