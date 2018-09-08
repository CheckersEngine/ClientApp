using CheckersPolygon.Controllers;
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
    /* Главная игровая форма
     */
    public partial class FormMain : Form
    {
        bool phase = false; // Фаза игры, от нее зависит, какие шашки будут вверху, а какие внизу
        public bool aiAffected = false;

        public FormMain(bool aiAffected)
        {
            InitializeComponent();
            Game.InitEngine(ref this.gameBoard, ref this.rtbUserLog, phase, aiAffected); // Инициализация игрового движка
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //Game.InitEngine(ref this.gameBoard, ref this.rtbUserLog, phase, aiAffected); // Инициализация игрового движка
        }

        private void gameBoard_Click(object sender, EventArgs e)
        {

        }

        /* Перерисовка изображения
         */
        private void gameBoard_Paint(object sender, PaintEventArgs e)
        {
            Game.drawingController.PrioritizedDraw();
        }

        private void gameBoard_Resize(object sender, EventArgs e)
        {

        }

        /* Перерисовка изображения при изменении размеров формы
         */
        private void FormMain_Resize(object sender, EventArgs e)
        {
            Game.gameplayController.OnResize();
        }

        /* Обработка нажатия клавиши мыши на игровом поле
         */
        private void gameBoard_MouseClick(object sender, MouseEventArgs e)
        {
            Game.gameplayController.OnClick(e);
        }

        /* Закрытие главного меню в случае закрытия этой формы
         */
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.mainMenu.Close();
        }

        /* Вызов меню по щелчку на кнопке
         */
        private void btnMenu_Click(object sender, EventArgs e)
        {
            FormMenu menu = new FormMenu(false);
            DialogResult result = menu.ShowDialog();

            // Обработка результата
            switch (result)
            {
                // Продолжить
                case DialogResult.Cancel:
                    break;
                
                // Новая игра
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

                // Сохранить
                case DialogResult.Abort:
                    SaveFileDialog saveDialog = new SaveFileDialog();
                    saveDialog.Filter = "Polygon checkers save file|*.plchckr";
                    if (saveDialog.ShowDialog() == DialogResult.OK)
                    {
                        Game.Save(saveDialog.FileName);
                    }
                    break;

                // Загрузить
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
    }
}
