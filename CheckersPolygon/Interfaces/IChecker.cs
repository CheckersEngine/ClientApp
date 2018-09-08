using CheckersPolygon.Helpers;
using CheckersPolygon.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersPolygon.Interfaces
{
    /* Интерфейс всего, что является шашкой
     */
    interface IChecker
    {
        // Сторона шашки (Черные/Белые)
        CheckerSide Side { get; set; }

        // Направление хода (Вверх/Вниз/В обе стороны)
        CheckerMoveDirection Direction { get; set; }
        
        // Максимальная длина хода в клетках
        byte TurnRange { get; set; }

        // -----------------------------------------------------------------------------------------------------
        // Получение списка всех возможных ходов
        PathPoint GetPossibleTurns(TurnDirection? bannedDirection);

        // Размещение на указанной позиции на доске
        void MoveTo(Point position);

        // Уничтожение объекта
        void Destroy();
    }
}
