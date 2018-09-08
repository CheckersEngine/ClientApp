using CheckersPolygon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CheckersPolygon.Helpers;
using CheckersPolygon.Helpers.Enums;
using CheckersPolygon.Controllers;

namespace CheckersPolygon.GameObjects
{
    /* Базовый класс, задающий общее поведение шашки
     */
    [Serializable]
    class BaseChecker : IDrawable, IChecker
    {
        /* Определяется:
         * - Стороной (Черные/Белые)
         * - Направлением хода (Вверх/Вниз/В обе стороны (у дамки))
         * - Комбинированной позицией (Позиция на экране, размер на экране, позиция на доске)
         */
        public BaseChecker(CheckerSide side, CheckerMoveDirection direction, CheckersCoordinateSet position)
        {
            this.Side = side;
            this.Position = position;
            this.ZOrder = 1;
            this.Direction = direction;
        }

        public byte ZOrder { get; set; } // Слой отрисовки
        public CheckersCoordinateSet Position { get; set; } // Комбинированная позиция шашки
        public CheckerSide Side { get; set; } // Сторона шашки (ч/б)
        public CheckerMoveDirection Direction { get; set; } // Направление хода шашки
        public byte TurnRange { get; set; } // Максимальное расстояние хода шашки
        public bool selected; // Выбрана ли шашка

        /* Уничтожение шашки, отписка от списка для отрисовки
         */
        public void Destroy()
        {
            Game.drawingController.DeleteFromDrawingList(this);
        }

        /* Отрисовка шашки
         */
        public virtual void Draw(Graphics graph)
        {
            graph.FillEllipse(new SolidBrush(Side == CheckerSide.White ? Constants.whiteCheckerColor : Constants.blackCheckerColor),
                Position.screenPosition.X, Position.screenPosition.Y,
                Position.drawableSize.Width, Position.drawableSize.Height);

            if (selected) // Выделение шашки, если она выбрана
            graph.DrawEllipse(new Pen(Constants.highlightCheckerColor, 4),
                Position.screenPosition.X, Position.screenPosition.Y,
                Position.drawableSize.Width, Position.drawableSize.Height);
        }

        /* Перемещение шашки на заданную позицию
         */
        public void MoveTo(Point boardPosition)
        {
            this.Position.boardPosition = boardPosition;
            this.Position.drawableSize = Game.gameplayController.cellSize;
            this.Position.screenPosition.X = this.Position.drawableSize.Width * boardPosition.X;
            this.Position.screenPosition.Y = this.Position.drawableSize.Height * boardPosition.Y;
        }

        /* Позиция в координатах меток на доске (пр. C8, A2, H5, ...)
         */
        public string GetPrintablePosition()
        {
            return (String.Format("{1}{0}", 
                (char)('8' - Convert.ToByte(this.Position.boardPosition.Y)),
                (char)('A' + Convert.ToByte(this.Position.boardPosition.X))));
        }

        /* Список возможных ходов для шашки
         */
        public PathPoint GetPossibleTurns()
        {
            return GetPossibleTurns(null);
        }

        /* Список возможных ходов для шашки с отсеченной диагональю
         */
        public virtual PathPoint GetPossibleTurns(TurnDirection? bannedDirection)
        {
            Pathfinder pathfinder = new Pathfinder();
            PathPoint turns = pathfinder.GetTurns(Position.boardPosition, this.TurnRange, Side, bannedDirection);

            return turns;
        }
    }
}
