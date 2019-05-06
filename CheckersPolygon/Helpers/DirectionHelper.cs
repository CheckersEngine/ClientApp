using CheckersPolygon.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersPolygon.Helpers
{
    /* Helper class for making operations with turn directions
     */
    class DirectionHelper
    {
        /* Returns all of the directions
         */
        public static IEnumerable<TurnDirection> GetAllDirections()
        {
            yield return TurnDirection.TopLeft;
            yield return TurnDirection.TopRight;
            yield return TurnDirection.BottomLeft;
            yield return TurnDirection.BottomRight;
        }

        /* Determination of the direction by vector
         */
        public static TurnDirection GetDirection(Point directionVector)
        {
            if (directionVector.Y < 0)
            {
                if (directionVector.X < 0) return TurnDirection.TopLeft;
                else return TurnDirection.TopRight;
            }
            else
            {
                if (directionVector.X < 0) return TurnDirection.BottomLeft;
                else return TurnDirection.BottomRight;
            }
        }

        /* Determining the opposite direction
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

        /* Obtaining the direction vector by its enum type
         */
        public static Point GetDirectionSigns(TurnDirection direction)
        {
            switch (direction)
            {
                case TurnDirection.TopLeft:
                    return new Point(-1, -1);
                case TurnDirection.TopRight:
                    return new Point(1, -1);
                case TurnDirection.BottomLeft:
                    return new Point(-1, 1);
                case TurnDirection.BottomRight:
                default:
                    return new Point(1, 1);
            }
        }

        /* Determining the opposite direction
         */
        public static TurnDirection OppositeDirection(TurnDirection direction)
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
