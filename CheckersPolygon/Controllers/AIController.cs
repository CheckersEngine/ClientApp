using CheckersPolygon.GameObjects;
using CheckersPolygon.Helpers;
using CheckersPolygon.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersPolygon.Controllers
{
    /* Artificial Intelligence Controller
     * Responsible for calculating the computer's moves
     */
    class AIController
    {
        // The side played by the computer
        public CheckerSide side { get; private set; }

        public AIController(CheckerSide aiSide)
        {
            this.side = aiSide;
        }

        /* Calculating the computer's move
         */
        public void GetAiTurn(out BaseChecker selectedChecker, out PathPoint toPosition)
        {
            selectedChecker = null;
            toPosition = null;
            List<PathPoint> possibleTurns = new List<PathPoint>(); // List of possible moves

            // Building a list of possible moves
            foreach (BaseChecker checker in side == CheckerSide.White ? Game.gameplayController.state.whiteCheckers :
                Game.gameplayController.state.blackCheckers)
            {
                PathPoint turn = checker.GetPossibleTurns();
                if (!turn.IsDeadEnd()) // If not a dead-end point, add a route to the list
                    possibleTurns.Add(turn);
            }

            // If no moves are found - a loss
            if (possibleTurns.Count == 0)
            {
                selectedChecker = null;
                toPosition = null;
                return;
            }

            // Trying to find aggressive moves
            foreach (PathPoint chain in possibleTurns)
            {
                List<int> eatableDirections = chain.TryGetAggresiveDirections();
                if (eatableDirections.Count > 0)
                {
                    // Aggressive move found
                    toPosition = chain;

                    // We find out the path belonging to the checker and leave the method
                    foreach (BaseChecker checker in side == CheckerSide.White ? Game.gameplayController.state.whiteCheckers :
                        Game.gameplayController.state.blackCheckers)
                    {
                        PathPoint turn = checker.GetPossibleTurns();
                        if (turn.Position == chain.Position)
                        {
                            selectedChecker = checker;
                            return;
                        }
                    }
                }
            }
            // There are no "edible" moves, we are looking for peaceful ones in a random way
            Random rnd = new Random();
            toPosition = possibleTurns[rnd.Next(possibleTurns.Count - 1)];
            // Matching the move with the checker
            foreach (BaseChecker checker in side == CheckerSide.White ? Game.gameplayController.state.whiteCheckers :
                Game.gameplayController.state.blackCheckers)
            {
                PathPoint turn = checker.GetPossibleTurns();
                if (turn.Position == toPosition.Position)
                {
                    selectedChecker = checker;
                }
            }

            return;
        }

        /* Selects the random movement direction from available
         * null - if none of the options are available
         */
        public PathPoint RandomPath(PathPoint point)
        {
            PathPoint toPosition = null;
            Random rnd = new Random();

            for (int i = 0; i < 4; i++)
            {
                if (point[i].Count > 0)
                {
                    toPosition = point[i][rnd.Next(point[i].Count - 1)];
                    return toPosition;
                }
            }

            return toPosition;
        }
    }
}
