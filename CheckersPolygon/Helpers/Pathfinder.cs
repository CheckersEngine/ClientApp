using CheckersPolygon.Controllers;
using CheckersPolygon.GameObjects;
using CheckersPolygon.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersPolygon.Helpers
{
    /* Class responsible for finding possible moves for the checkers
     */
    class Pathfinder
    {
        /* Getting a list of available moves for the checker in the indicated position
         * A recursive method!
         */
        public PathPoint GetTurns(Point boardPosition, byte turnRange, CheckerSide side, TurnDirection? bannedDirection = null)
        {
            // The initial list of moves for this position
            PathPoint turnPathPoint = new PathPoint(boardPosition);

            // Position
            Point position = boardPosition;
            bool nonPacifist = false; // "There are edible options" flag
            Point[] directionSigns = new Point[4]
                {
                    DirectionHelper.GetDirectionSigns(TurnDirection.TopLeft),
                    DirectionHelper.GetDirectionSigns(TurnDirection.TopRight),
                    DirectionHelper.GetDirectionSigns(TurnDirection.BottomLeft),
                    DirectionHelper.GetDirectionSigns(TurnDirection.BottomRight)
                };

            // Trying to find a path with blocked directions
            if (bannedDirection == null)
            {
                // Tracing in all directions to find out if there are "aggressive" moves
                PathPoint[] result = new PathPoint[4];
                List<TurnDirection> directions = DirectionHelper.GetAllDirections().ToList();
                for (int i = 0; i < 4; i++)
                {
                    result[i] = TraceSide(turnRange, side, position, directions[i]);
                }

                // Are there "aggressive options"?
                for (int i = 0; i < 4; i++)
                    if (!result[i].IsOnlyFinalTraces())
                        nonPacifist = true;

                // Filtering moves with regard to their possible "aggressiveness"
                for (int i = 0; i < 4; i++)
                    if (!nonPacifist)
                    {
                        if (!result[i].IsDeadEnd()) // All moves
                            turnPathPoint[i].AddRange(result[i][i]);
                    }
                    else
                    {
                        if (!result[i].IsOnlyFinalTraces(i)) // Only aggressive moves
                            turnPathPoint[i].AddRange(result[i][i]);
                    }
            }
            else
            {
                List<PathPoint> paths = new List<PathPoint>();

                // Tracing unblocked directions to find out if there are "aggressive" moves
                foreach (TurnDirection direction in DirectionHelper.GetAllDirections())
                {
                    if (bannedDirection != direction)
                        paths.Add(TraceSide(turnRange, side, position, direction));
                }

                // Are there "aggressive options"
                foreach (PathPoint point in paths)
                    if (!point.IsOnlyFinalTraces())
                        nonPacifist = true;

                // Filtering of moves taking into account their possible "aggressiveness" and blocked directions
                foreach (TurnDirection direction in DirectionHelper.GetAllDirections())
                {
                    if (bannedDirection != direction)
                    {
                        PathPoint result = TraceSide(turnRange, side, position, direction);
                        if (!nonPacifist)
                        {
                            if (!result.IsDeadEnd()) // All moves
                                turnPathPoint[direction].AddRange(result[direction]);
                        }
                        else
                        {
                            if (!result.IsOnlyFinalTraces()) // Only aggressive moves
                                turnPathPoint[direction].AddRange(result[direction]);
                        }
                    }
                }
            }

            return turnPathPoint;
        }


        /* Tracing a diagonal from a specific position
         */
        private PathPoint TraceSide(byte turnRange, CheckerSide side, Point position, TurnDirection direction)
        {
            PathPoint path = new PathPoint(position);
            Point directionSigns = DirectionHelper.GetDirectionSigns(direction); // Direction vector
            // Flags
            bool pacifistFlag = true; // There are only "peaceful" moves
            bool eatableFlag = false; // Found edible
            bool obstacleFlag = false; // Found an obstacle
            bool notFinished = false; // The edible sword is not obstructed
            Point eatablePosition = new Point(-1, -1); // The position of the edible sword
            byte turnLength = 1; // The length of the trajectory

            // Main trace loop
            // The position must be kept within the board and not exceed the length of the checker's move
            while (turnLength <= turnRange &&
                                position.X + turnLength * directionSigns.X >= 0 && position.X + turnLength * directionSigns.X < 8 &&
                                position.Y + turnLength * directionSigns.Y >= 0 && position.Y + turnLength * directionSigns.Y < 8)
            {
                // Position taking into account the correction of coordinates
                Point newPosition = new Point(position.X + turnLength * directionSigns.X, position.Y + turnLength * directionSigns.Y);

                // Trying to find an obstacle in a new position
                // Obstacle -> friendly pawn
                foreach (BaseChecker checker in side == CheckerSide.White ?
                    Game.gameplayController.state.WhiteCheckers :
                    Game.gameplayController.state.BlackCheckers)
                {
                    if (newPosition == checker.Position.BoardPosition)
                    {
                        obstacleFlag = true; // If an obstacle is found, immediately set flag true
                        break;
                    }
                }

                // An attempt to find the enemy in a new position
                foreach (BaseChecker checker in side == CheckerSide.White ?
                    Game.gameplayController.state.BlackCheckers :
                    Game.gameplayController.state.WhiteCheckers)
                {
                    if (newPosition == checker.Position.BoardPosition)
                    {
                        if (eatableFlag)
                        {
                            // If 2 enemies in a row - then inedible
                            obstacleFlag = true;
                            break;
                        }
                        else
                        {
                            // If an enemy is found, immediately set flag true
                            eatableFlag = true;
                            break;
                        }
                    }
                }

                // If trace come across an obstacle - we immediately stop looking
                if (obstacleFlag)
                {
                    break;
                }

                // If you find an edible checker
                if (eatableFlag)
                {
                    // The position next to the edible cell
                    Point tracePoint = new Point(newPosition.X + directionSigns.X, newPosition.Y + directionSigns.Y);
                    if (tracePoint.X >= 0 && tracePoint.X < 8 &&
                        tracePoint.Y >= 0 && tracePoint.Y < 8)
                    {
                        // Check next for edible cell for checkers
                        foreach (BaseChecker checker in Game.gameplayController.state.AllCheckers)
                        {
                            if (tracePoint == checker.Position.BoardPosition)
                            {
                                notFinished = true;
                            }
                        }

                        // If there is no next checker - checker is edible
                        if (!notFinished)
                        {
                            // Tracing from the "edible" position in order to determine the possibility of continuing the current move
                            // All directions are considered except the direction opposite to the vector of the current stroke
                            eatablePosition = tracePoint;
                            PathPoint possiblePath = GetTurns(eatablePosition, turnRange, side, DirectionHelper.OppositeDirection(direction));
                            possiblePath.afterAggression = true;
                            path[direction].Add(possiblePath);
                            pacifistFlag = false; // The move ceases to be "peaceful" - an edible variant is found
                        }
                    }
                }
                turnLength++;
            }

            // Finding "peaceful" options for the move
            // Only if "aggressive" moves are not detected
            // The search is in the opposite direction - from the obstacle to the walking checker
            turnLength--;
            if (pacifistFlag)
            {
                int reverseLength = turnLength; // Length of opposite move
                if (reverseLength > 1 && eatableFlag) // If the current position corresponds to an obstacle
                    reverseLength--; // Reduce the length of the opposite move

                // Trying to find all possible positions for the move
                while (turnLength - reverseLength != turnLength
                    && position.X + reverseLength * directionSigns.X >= 0 && position.X + reverseLength * directionSigns.X < 8 &&
                                position.Y + reverseLength * directionSigns.Y >= 0 && position.Y + reverseLength * directionSigns.Y < 8)
                {
                    // New position
                    Point newPosition = new Point(position.X + reverseLength * directionSigns.X, position.Y + reverseLength * directionSigns.Y);
                    // An attempt to find the correspondence of any white checker to a given position
                    foreach (BaseChecker checker in Game.gameplayController.state.AllCheckers)
                    {
                        if (newPosition == checker.Position.BoardPosition)
                        {
                            return path;
                        }
                    }

                    // Adding a new position for the move, if not collision with another checker is updated
                    path[direction].Add(new PathPoint(newPosition, true));
                    reverseLength--;
                }
            }

            return path;
        }
    }
}
