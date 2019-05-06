using CheckersPolygon.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersPolygon.Helpers
{
    /* List of possible moves for the checker from a certain position
     */
    [Serializable]
    public class PathPoint
    {
        public Point Position { get; private set; } // Current position
        public bool finalPoint = false; // Is the end point in the chain of trace?
        public bool afterAggression = false; // The point on which checker can stand after eating the enemy

        protected List<PathPoint>[] traces = new List<PathPoint>[4]; // Traces in 4 directions

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

        /* If the position has only peaceful continuations or there are none at all
         */
        public bool IsOnlyFinalTraces()
        {
            if (this.IsDeadEnd()) // If the position is deadlock (no continuation)
                return true;

            for (int i = 0; i < 4; i++)
            {
                if (traces[i].Count > 0)
                    foreach (PathPoint point in traces[i])
                    {
                        if (!point.finalPoint) // If there is at least one non-finite extension
                            return false;
                    }
            }

            return true;
        }

        /* If the position has only peaceful continuations or there are none at all in the given direction
         */
        public bool IsOnlyFinalTraces(int index)
        {
            if (this.IsDeadEnd()) // If the position is deadlock (no continuation)
                return true;

            foreach (PathPoint point in traces[index])
            {
                if (!point.finalPoint) // If there is at least one non-finite extension
                    return false;
            }

            return true;
        }

        /* Is the position deadlock (not having continuations)
         */
        public bool IsDeadEnd()
        {
            if (traces[0].Count() == 0 && traces[1].Count() == 0 && traces[2].Count() == 0 && traces[3].Count() == 0)
                return true;
            else
                return false;
        }

        /* Returning and setting the value on an integer index (indexer)
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

        /* Returning and setting the value by the index of the enumeration (indexer)
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

        /* Conversion of the direction of move to an integer index
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

        /* Trying to find a direction with edible checkers
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
    }
}
