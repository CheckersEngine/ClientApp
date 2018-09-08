using CheckersPolygon.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersPolygon.Interfaces
{
    /* Интерфейс всего, что нужно отрисовывать
     */
    interface IDrawable
    {
        // Индекс слоя отрисовки (0 - рисуется в первую очередь, 1 - во вторую, ... и т.д.)
        Byte ZOrder { get; set; }

        // Комбинированная позиция объекта
        CheckersCoordinateSet Position { get; set; }

        // Отрисовка объекта
        void Draw(Graphics graph);
    }
}
