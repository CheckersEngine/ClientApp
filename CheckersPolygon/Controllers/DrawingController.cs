using CheckersPolygon.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersPolygon.Controllers
{
    /* Контроллер, заведующий управлением отрисовкой игровой сцены
     */
    internal sealed class DrawingController
    {
        private Panel gameBoard; // Ссылка на объект, пространство которого будет представлять игровую доску
        private delegate void Draw(Graphics graph); // Все "рисующиеся" объекты должны иметь такой прототип метода отрисовки

        /* Список отрисовки игровых обьектов с учетом приоритета (Z-Order)
         * Список содержит слои в порядке их отрисовки (сначала рисуется все с индексом 0, потом с индексом 1 и т.д)
         * Слои содержат список методов отрисовки обьектов */
        private List<List<Draw>> prioritizedDrawingList;


        public DrawingController(ref Panel gameBoard)
        {
            this.gameBoard = gameBoard;
            this.prioritizedDrawingList = new List<List<Draw>>();
            this.prioritizedDrawingList.Add(new List<Draw>()); // Добавление нулевого слоя
        }


        /* Добавление метода отрисовки в список отрисовки согласно приоритету слоя (Z-Order) каждого объекта
         * Обычно добавляется после создания или загрузки объекта
         */
        public void AddToDrawingList(IDrawable drawingEntity)
        {
            // Создание новых слоев (если не существуют)
            while (drawingEntity.ZOrder > prioritizedDrawingList.Count - 1)
                this.prioritizedDrawingList.Add(new List<Draw>());

            // Добавление метода в нужный слой
            prioritizedDrawingList[drawingEntity.ZOrder].Add(drawingEntity.Draw);
        }


        /* Удаление из списка отрисовки
         * Обычно вызывается в момент уничтожения игрового объекта
         */
        public void DeleteFromDrawingList(IDrawable drawingEntity)
        {
            foreach (List<Draw> layer in prioritizedDrawingList)
            {
                foreach (Draw drawingCall in layer)
                {
                    if (drawingCall == drawingEntity.Draw) // Поиск (не)нужного метода
                    {
                        layer.Remove(drawingCall);
                        return;
                    }
                }
            }
        }


        /* Главный метод отрисовки
         * Для перерисовки игровой сцены используют этот метод через Game
         * Отрисовка выполняется на Bitmap, после рисуется сам Bitmap
         * - это устраняет "дерганье" изображения.
         */
        public void PrioritizedDraw()
        {
            // Если окно свернуто, отрисовка не производится
            if (gameBoard.Width == 0 || gameBoard.Height == 0)
                return;

            Image buffer = new Bitmap(gameBoard.Width, gameBoard.Height); // буфер размером с экран (Panel)
            Graphics graphicsAdapter = Graphics.FromImage(buffer);
            foreach (List<Draw> layer in prioritizedDrawingList) // Послойная отрисовка всех игровых объектов
            {
                foreach (Draw drawingCall in layer)
                {
                    drawingCall(graphicsAdapter); // Отрисовка игровых объектов
                }
            }
            graphicsAdapter = gameBoard.CreateGraphics();
            graphicsAdapter.DrawImage(buffer, new Point(0, 0)); // Отрисовка буфера на экран
        }
    }
}
