using CheckersPolygon.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersPolygon.Helpers
{
    /* Список возможных ходов для шашки с определенной позиции
     */
    [Serializable]
    class PathPoint
    {
        public Point Position { get; private set; } // Текущая позиция
        public bool finalPoint = false; // Конечная ли точка в цепочке хода
        public bool afterAggression = false; // Точка, на которую можно встать после съеденной шашки

        protected List<PathPoint>[] traces = new List<PathPoint>[4]; // Цепочки ходов по 4 направлениям

        public PathPoint(Point position, bool finalPoint)
        {
            this.Position = position;
            this.finalPoint = finalPoint;
            for (int i = 0; i < 4; i++)
                traces[i] = new List<PathPoint>();
        }

        public PathPoint(Point position)
        {
            this.Position = position;
            for (int i = 0; i < 4; i++)
                traces[i] = new List<PathPoint>();
        }

        /* Если у позиции есть только мирные продолжения или их нет совсем
         */
        public bool IsOnlyFinalTraces()
        {
            if (this.IsDeadEnd()) // Если позиция тупиковая (нет продолжений)
                return true;

            for (int i = 0; i < 4; i++)
            {
                if (traces[i].Count > 0)
                    foreach (PathPoint point in traces[i])
                    {
                        if (!point.finalPoint) // Если есть хоть одно не конечное продолжение
                            return false;
                    }
            }

            return true;
        }

        /* Если у позиции есть только мирные продолжения или их вовсе нет за исключением заблокированного направления
         */
        public bool IsOnlyFinalTracesExcept(TurnDirection bannedDirection)
        {
            if (this.IsDeadEnd()) // Если позиция тупиковая (нет продолжений)
                return true;

            for (int i = 0; i < 4; i++)
            {
                if (i != PathPoint.IndexOfDirection(bannedDirection))
                    foreach (PathPoint point in traces[i])
                    {
                        if (!point.finalPoint) // Если есть хоть одно не конечное продолжение
                            return false;
                    }
            }

            return true;
        }

        /* Если у позиции есть только мирные продолжения или их нет совсем на заданном направлении
         */
        public bool IsOnlyFinalTraces(int index)
        {
            if (this.IsDeadEnd()) // Если позиция тупиковая (нет продолжений)
                return true;

            foreach (PathPoint point in traces[index])
            {
                if (!point.finalPoint) // Если есть хоть одно не конечное продолжение
                    return false;
            }

            return true;
        }

        /* Является ли позиция тупиковой (не имеющей продолжений)
         */
        public bool IsDeadEnd()
        {
            if (traces[0].Count() == 0 && traces[1].Count() == 0 && traces[2].Count() == 0 && traces[3].Count() == 0)
                return true;
            else
                return false;
        }

        /* Возврат и установка значения по целочисленному индексу (индексатор)
         */
        public List<PathPoint> this[int index]
        {
            get
            {
                if (index < 0 || index > 3) throw new IndexOutOfRangeException("Index of PathPoint must be from 0 to 3");
                return traces[index];
            }

            set
            {
                if (index < 0 || index > 3) throw new IndexOutOfRangeException("Index of PathPoint must be from 0 to 3");
                traces[index] = value;
            }
        }

        /* Возврат и установка значения по индексу перечисления (индексатор)
         */
        public List<PathPoint> this[TurnDirection direction]
        {
            get
            {
                switch (direction)
                {
                    case TurnDirection.TopLeft:
                        return traces[0];
                    case TurnDirection.TopRight:
                        return traces[1];
                    case TurnDirection.BottomLeft:
                        return traces[2];
                    case TurnDirection.BottomRight:
                        return traces[3];
                    default:
                        throw new InvalidOperationException("Incorrect direction");
                }
            }

            set
            {
                switch (direction)
                {
                    case TurnDirection.TopLeft:
                        traces[0] = value;
                        break;
                    case TurnDirection.TopRight:
                        traces[1] = value;
                        break;
                    case TurnDirection.BottomLeft:
                        traces[2] = value;
                        break;
                    case TurnDirection.BottomRight:
                        traces[3] = value;
                        break;
                    default:
                        throw new InvalidOperationException("Incorrect direction");
                }
            }
        }

        /* Возврат и установка значения по строковому индексу (индексатор)
         */
        public List<PathPoint> this[string direction]
        {
            get
            {
                switch (direction.ToLower())
                {
                    case "topleft":
                    case "lefttop":
                    case "tl":
                    case "lt":
                        return traces[0];
                    case "topright":
                    case "righttop":
                    case "tr":
                    case "rt":
                        return traces[1];
                    case "bottomleft":
                    case "leftbottom":
                    case "bl":
                    case "lb":
                        return traces[2];
                    case "bottomright":
                    case "rightbottom":
                    case "br":
                    case "rb":
                        return traces[3];
                    default:
                        throw new IndexOutOfRangeException("No such index. Use tl/tr/bl/br index.");
                }
            }

            set
            {
                switch (direction.ToLower())
                {
                    case "topleft":
                    case "tl":
                        traces[0] = value;
                        break;
                    case "topright":
                    case "tr":
                        traces[1] = value;
                        break;
                    case "bottomleft":
                    case "bl":
                        traces[2] = value;
                        break;
                    case "bottomright":
                    case "br":
                        traces[3] = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("No such index. Use tl/tr/bl/br index.");
                }
            }
        }

        /* Преобразование направления хода в целочисленный индекс
         */
        public static int IndexOfDirection(TurnDirection direction)
        {
            switch (direction)
            {
                case TurnDirection.TopLeft: return 0;
                case TurnDirection.TopRight: return 1;
                case TurnDirection.BottomLeft: return 2;
                case TurnDirection.BottomRight: return 3;
                default: throw new InvalidOperationException("Incorrect direction");
            }
        }

        /* Попытка найти направление со съедобными шашками
         */
        public List<int> TryGetAggresiveDirections()
        {
            List<int> aggresiveDirections = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                foreach (PathPoint point in traces[i])
                {
                    if (point.afterAggression)
                    {
                        aggresiveDirections.Add(i);
                        break;
                    }
                }
            }

            return aggresiveDirections;
        }

        /* Определение противоположного направления
         */
        public static TurnDirection GetOppositeDirection(TurnDirection direction)
        {
            switch (direction)
            {
                case TurnDirection.TopLeft:
                    return TurnDirection.BottomRight;
                case TurnDirection.TopRight:
                    return TurnDirection.BottomLeft;
                case TurnDirection.BottomLeft:
                    return TurnDirection.TopRight;
                case TurnDirection.BottomRight:
                default:
                    return TurnDirection.TopLeft;
            }
        }
    }
}
