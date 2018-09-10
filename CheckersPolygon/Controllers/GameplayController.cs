using CheckersPolygon.Forms;
using CheckersPolygon.GameObjects;
using CheckersPolygon.Helpers;
using CheckersPolygon.Helpers.Enums;
using CheckersPolygon.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersPolygon.Controllers
{
    /* Class that controls the whole process of the game
     * Includes:
     * - The initial arrangement of checkers at the beginning of a new game
     * - Storing, changing, loading and saving the state of game objects (state)
     * - Calculation of the progress based on the data on the relative position of the checkers on the board
     * - Control of the progress of the player and computer
     * - Handling of events in accordance with the rules of the game
     */
    internal sealed class GameplayController
    {
        public BoardCell[,] board = new BoardCell[8, 8]; // Game board cells (active game object)
        public BoardMarker[,] markers = new BoardMarker[2, 8]; // Labels of the cells of the game board (1,2,3,4 ... / A, B, C, D ...)
        public GameState state = new GameState(); // The state of game objects (Checkers, etc.)
        public Size cellSize; // Cell size (to support scaling)
        private Panel gameBoard; // Link to the game board on the form


        /* The initial arrangement of all game objects
         * Register them in the drawing list
         */
        public GameplayController(ref Panel gameBoard, bool phase, bool aiAffected)
        {
            this.state.phase = phase;
            this.gameBoard = gameBoard;

            // If there is a game with a computer
            if (aiAffected)
            {
                state.isAiControlled = true;
                state.aiSide = phase == true ? CheckerSide.White : CheckerSide.Black;
            }

            // Setting the size of a single cell
            cellSize.Width = gameBoard.Size.Width / 8;
            cellSize.Height = gameBoard.Size.Height / 8;

            // Initializing the cells of the board
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    CheckersCoordinateSet position = new CheckersCoordinateSet(
                        new Point(j * cellSize.Width, i * cellSize.Height),
                        cellSize,
                        new Point(j, i));

                    board[j, i] = new BoardCell(((i + j) % 2 == 0) ? false : true,
                        position);

                    Game.drawingController.AddToDrawingList(board[j, i]);
                }
            }

            // Initialization of cell markers (A, B, C, D ...)
            for (int i = 0; i < 8; i++)
            {
                // Vertical markers
                CheckersCoordinateSet position = new CheckersCoordinateSet(
                        new Point(2, i * cellSize.Height + (cellSize.Height / 2) - (cellSize.Height / 6)),
                        new Size(12, 12),
                        new Point(0, i));
                markers[0, i] = new BoardMarker(Convert.ToChar('8' - i), position);
                Game.drawingController.AddToDrawingList(markers[0, i]);

                // Horizontal markers
                position = new CheckersCoordinateSet(
                        new Point(i * cellSize.Width + (cellSize.Width / 2) - (cellSize.Width / 6), 8 * cellSize.Height - 18),
                        new Size(12, 12),
                        new Point(i, 0));
                markers[1, i] = new BoardMarker(Convert.ToChar('A' + i), position);
                Game.drawingController.AddToDrawingList(markers[1, i]);
            }

            // Initial checkers placement
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    // Determination of the coordinates and dimensions of the checker on the top
                    CheckersCoordinateSet positionTop = new CheckersCoordinateSet(
                        new Point((2 * j + ((i + 1) % 2)) * cellSize.Width, i * cellSize.Height),
                        cellSize,
                        new Point(2 * j + ((i + 1) % 2), i));

                    // Determining the coordinates and the size of the checker from the bottom
                    CheckersCoordinateSet positionBottom = new CheckersCoordinateSet(
                        new Point(7 * cellSize.Width - (((2 * j + ((i + 1) % 2)) * cellSize.Width)),
                        7 * cellSize.Height - i * cellSize.Height),
                        cellSize,
                        new Point(7 - (2 * j + ((i + 1) % 2)), 7 - i));

                    // Initializing a White Checker
                    BaseChecker checker = new Pawn(CheckerSide.White,
                        phase == true ? CheckerMoveDirection.Downstairs : CheckerMoveDirection.Upstairs,
                        phase == true ? positionTop : positionBottom);
                    state.whiteCheckers.Add(checker);
                    Game.drawingController.AddToDrawingList(checker);

                    // Initializing the Black Checker
                    checker = new Pawn(CheckerSide.Black,
                        phase == false ? CheckerMoveDirection.Downstairs : CheckerMoveDirection.Upstairs,
                        phase == false ? positionTop : positionBottom);
                    state.blackCheckers.Add(checker);
                    Game.drawingController.AddToDrawingList(checker);
                }
            }

            // The turn of artificial intelligence
            Game.drawingController.PrioritizedDraw();
        }

        /* Change all sizes of all game objects
         * Resetting them on the board
         */
        public void OnResize()
        {
            // Setting the size of a single cell
            cellSize.Width = gameBoard.Size.Width / 8;
            cellSize.Height = gameBoard.Size.Height / 8;

            // Changing the size and coordinates of the cells of the board
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[j, i].Position.screenPosition = new Point(j * cellSize.Width, i * cellSize.Height);
                    board[j, i].Position.drawableSize = cellSize;
                }
            }

            // Changing the size and coordinates of the board markers
            for (int i = 0; i < 8; i++)
            {
                markers[0, i].Position.screenPosition = new Point(0,
                    i * cellSize.Height + (cellSize.Height / 2) - (cellSize.Height / 8));
                markers[1, i].Position.screenPosition = new Point(i * cellSize.Width + (cellSize.Width / 2) - (cellSize.Width / 8),
                    8 * cellSize.Height - 18);
            }

            // Changing the size and coordinates of white checkers
            foreach (BaseChecker checker in state.whiteCheckers)
            {
                checker.Position = new CheckersCoordinateSet(
                    new Point(checker.Position.boardPosition.X * cellSize.Width, checker.Position.boardPosition.Y * cellSize.Height),
                    cellSize,
                    new Point(checker.Position.boardPosition.X, checker.Position.boardPosition.Y));
            }

            // Change the size and coordinates of the black checkers
            foreach (BaseChecker checker in state.blackCheckers)
            {
                checker.Position = new CheckersCoordinateSet(
                    new Point(checker.Position.boardPosition.X * cellSize.Width, checker.Position.boardPosition.Y * cellSize.Height),
                    cellSize,
                    new Point(checker.Position.boardPosition.X, checker.Position.boardPosition.Y));
            }

            // Redrawing the image
            Game.drawingController.PrioritizedDraw();
        }

        #region Calculation of movement and "movement of checkers" process

        /* When you click on the game board, this method is called
         */
        public void OnClick(MouseEventArgs mouseArgs)
        {
            bool anyoneSelected = false;
            BaseChecker previouslySelectedChecker = null;
            BaseChecker currentSelectedChecker = null;

            // Trying to find the selected checker
            anyoneSelected = TryFindSelectedCheckers(mouseArgs, ref previouslySelectedChecker, ref currentSelectedChecker);

            // If the checker was not selected
            if (!anyoneSelected)
            {
                // If the checker was selected at the last step
                if (previouslySelectedChecker != null)
                {
                    // Attempt to move to a given position
                    Point toBoardPosition = new Point()
                    {
                        X = mouseArgs.X / cellSize.Width,
                        Y = mouseArgs.Y / cellSize.Height
                    };
                    DoMovement(previouslySelectedChecker, toBoardPosition);

                    // Checking the conditions of victory and defeat
                    CheckVictoryConditions();
                }
                else
                {
                    // Clicked on an empty field
                    DeselectAllCheckers();
                    state.activeChecker = null;
                    state.activeCheckerPathPoint = null;
                }
            }
            else
            {
                // The checker is selected
                // Prevent the move by another checker (other than the selected one)
                if (state.activeChecker != null)
                    if (state.activeChecker != currentSelectedChecker)
                    {
                        currentSelectedChecker = null;
                        return;
                    }

                List<BaseChecker> turnList = new List<BaseChecker>();
                bool aggressive = false;
                bool turnFound = false;
                foreach (BaseChecker checker in currentSelectedChecker.Side == CheckerSide.White ? state.whiteCheckers : state.blackCheckers)
                {
                    PathPoint turns = checker.GetPossibleTurns();
                    if (turns.IsDeadEnd()) continue;
                    if (turns.TryGetAggresiveDirections().Count > 0)
                    {
                        turnList.Add(checker);
                        aggressive = true;
                    }
                }

                // Filtering moves (if you can eat something, block "inedible" moves)
                if (!aggressive)
                {
                    ClearHighlighting();
                    HighlightPossibleTurns(currentSelectedChecker); // Highlight it
                }
                else
                {
                    foreach (BaseChecker checker in turnList)
                        if (checker == currentSelectedChecker)
                        {
                            turnFound = true;
                        }

                    ClearHighlighting();
                    if (turnFound)
                    {
                        HighlightPossibleTurns(currentSelectedChecker);
                    }
                    else
                    {
                        DeselectAllCheckers();
                    }
                }
            }

            // Redraw the scene
            Game.drawingController.PrioritizedDraw();

            // Clearing memory from objects generated by active computation of edible options
            GC.Collect();
        }

        /* The course of artificial intelligence
         * Calculation of moves, verification of conditions for winning and losing
         */
        public void AiTurn()
        {
            BaseChecker selectedChecker = null;
            PathPoint toPoint;
            AIController ai = new AIController(state.aiSide);

            // Trying to find the selected checker
            ai.GetAiTurn(out selectedChecker, out toPoint);

            // If the checker was selected
            if (selectedChecker != null)
            {
                // Attempt to move to a given position
                do
                {
                    PathPoint newPath = ai.RandomPath(toPoint);
                    if (newPath == null)
                    {
                        break;
                    }

                    Point toBoardPosition = newPath.Position;
                    DoMovement(selectedChecker, toBoardPosition);
                    toPoint = newPath;
                }
                while (state.activeChecker != null);
                // Checking the conditions of victory and defeat
                CheckVictoryConditions();

            }
            else
            {
                // Checking the conditions of victory and defeat
                CheckVictoryConditions();
            }

            // Redraw the scene
            Game.drawingController.PrioritizedDraw();

            // Clearing memory from objects generated by active computation of edible options
            GC.Collect();
        }

        /* Checking win / loss conditions for all sides
         */
        private void CheckVictoryConditions()
        {
            bool toilet = true;
            if (state.whiteCheckers.Count == 0)
            {
                // Black's victory
                Game.drawingController.PrioritizedDraw();
                Game.userLogController.WriteMessage(Constants.localized.logBlackWon);
                FormVictory victoryDialog = new FormVictory(false, false);
                victoryDialog.ShowDialog();
            }
            else if (state.blackCheckers.Count == 0)
            {
                // White's victory
                Game.drawingController.PrioritizedDraw();
                Game.userLogController.WriteMessage(Constants.localized.logWhiteWon);
                FormVictory victoryDialog = new FormVictory(true, false);
                victoryDialog.ShowDialog();
            }
            else
            {
                // Check for "toilet" (no more moves)
                foreach (BaseChecker checker in state.turn == CheckerSide.White ? state.whiteCheckers : state.blackCheckers)
                {
                    if (!checker.GetPossibleTurns().IsDeadEnd())
                        toilet = false;
                }

                // If no more moves
                if (toilet)
                {
                    if (state.turn == CheckerSide.White)
                    {
                        // White lose
                        Game.userLogController.WriteMessage(Constants.localized.logWhiteToilet);
                        FormVictory victoryDialog = new FormVictory(true, true);
                        victoryDialog.ShowDialog();
                    }
                    else
                    {
                        // Black lose
                        Game.userLogController.WriteMessage(Constants.localized.logBlackToilet);
                        FormVictory victoryDialog = new FormVictory(false, true);
                        victoryDialog.ShowDialog();
                    }
                }
            }
        }

        /* Trying to find the selected checkers after clicking the mouse button
         */
        private bool TryFindSelectedCheckers(MouseEventArgs mouseArgs, ref BaseChecker previouslySelectedChecker, ref BaseChecker currentSelectedChecker)
        {
            bool anyoneSelected = false;
            if (state.turn == CheckerSide.White) // Compliance with the order of the turn
            {
                foreach (BaseChecker whiteChecker in state.whiteCheckers)
                {
                    // Mark previously selected checker
                    if (whiteChecker.selected)
                        previouslySelectedChecker = whiteChecker;

                    // Marking the checker just selected
                    Rectangle checkerCoord = new Rectangle(whiteChecker.Position.screenPosition, whiteChecker.Position.drawableSize);
                    if (checkerCoord.Contains(mouseArgs.Location))
                    {
                        SelectChecker(whiteChecker);
                        currentSelectedChecker = whiteChecker;
                        anyoneSelected = true;
                    }
                }
            }
            else
            {
                foreach (BaseChecker blackChecker in state.blackCheckers)
                {
                    // Mark previously selected checker
                    if (blackChecker.selected)
                        previouslySelectedChecker = blackChecker;

                    // Marking the checker just selected
                    Rectangle checkerCoord = new Rectangle(blackChecker.Position.screenPosition, blackChecker.Position.drawableSize);
                    if (checkerCoord.Contains(mouseArgs.Location))
                    {
                        SelectChecker(blackChecker);
                        currentSelectedChecker = blackChecker;
                        anyoneSelected = true;
                    }
                }
            }
            return anyoneSelected;
        }


        /* Trying to move checker
         * Calculating the movement
         * ------------------------------------------------------------------------------
         */
        private void DoMovement(BaseChecker previouslySelectedChecker, Point toPosition)
        {
            string logMessage = Constants.localized.logTurn + ' ';
            PathPoint turns;
            // Erase highlighting on all checkers
            DeselectAllCheckers();

            // If the move does not start
            if (state.activeCheckerPathPoint == null)
            {
                turns = previouslySelectedChecker.GetPossibleTurns(); // Calculation of all possible chains of moves for a checker
                state.activeCheckerPathPoint = turns;
            }
            else
                turns = state.activeCheckerPathPoint; // Continuation of the movement

            // If the position is not a deadlock
            if (!turns.IsDeadEnd())
            {
                state.activeChecker = previouslySelectedChecker;

                // Movement
                for (int i = 0; i < 4; i++)
                    foreach (PathPoint point in turns[i])
                    {
                        if (toPosition == point.Position)
                        {
                            Point fromPosition = new Point()
                            {
                                X = previouslySelectedChecker.Position.boardPosition.X,
                                Y = previouslySelectedChecker.Position.boardPosition.Y
                            };
                            logMessage += previouslySelectedChecker.GetPrintablePosition() + $" {Constants.localized.logMoveTo} ";
                            // Moving the checker to the selected position
                            previouslySelectedChecker.MoveTo(new Point(toPosition.X, toPosition.Y));
                            logMessage += previouslySelectedChecker.GetPrintablePosition();
                            Game.userLogController.WriteMessage(logMessage);
                            // Defining the direction of movement
                            Point moveDirection = new Point()
                            {
                                X = toPosition.X - fromPosition.X > 0 ? 1 : -1,
                                Y = toPosition.Y - fromPosition.Y > 0 ? 1 : -1
                            };

                            // Determination of the eaten checkers
                            TryEatEnemyCheckers(previouslySelectedChecker, toPosition, fromPosition, moveDirection);

                            // If the checker can become a king
                            if (previouslySelectedChecker is Pawn && previouslySelectedChecker.Position.boardPosition.Y ==
                                (previouslySelectedChecker.Direction == CheckerMoveDirection.Upstairs ? 0 : 7))
                            {
                                // Replacement of a checker by a king
                                King newKing = new King(previouslySelectedChecker.Side, previouslySelectedChecker.Position);
                                if (previouslySelectedChecker.Side == CheckerSide.White)
                                {
                                    state.whiteCheckers.Remove(previouslySelectedChecker);
                                    state.whiteCheckers.Add(newKing);
                                }
                                else
                                {
                                    state.blackCheckers.Remove(previouslySelectedChecker);
                                    state.blackCheckers.Add(newKing);
                                }

                                // Adding to the list of drawings
                                Game.drawingController.AddToDrawingList(newKing);
                                previouslySelectedChecker.Destroy();

                                // Calculation of the further path for a king
                                state.activeChecker = newKing;
                                if (state.activeCheckerPathPoint.afterAggression)
                                {
                                    state.activeCheckerPathPoint = newKing.GetPossibleTurns(PathPoint.GetOppositeDirection(GetDirection(moveDirection)));
                                    if (state.activeCheckerPathPoint.TryGetAggresiveDirections().Count == 0)
                                    {
                                        // End of turn
                                        EndTurn();
                                        return;
                                    }
                                    else
                                    {
                                        DeselectAllCheckers();
                                        state.activeChecker.selected = true;
                                        HighlightPossibleTurns(state.activeChecker);
                                    }
                                }
                                else
                                {
                                    EndTurn();
                                    return;
                                }
                            }

                            // Find the point he went to
                            // Calculate whether it is possible to continue the movement
                            foreach (PathPoint waypoint in state.activeCheckerPathPoint[GetDirection(moveDirection)])
                            {
                                if (waypoint.Position == toPosition)
                                {
                                    if (!waypoint.IsOnlyFinalTraces())
                                    {
                                        // Continuation of the movement
                                        state.activeCheckerPathPoint = waypoint;
                                        DeselectAllCheckers();
                                        state.activeChecker.selected = true;
                                        HighlightPossibleTurns(state.activeChecker);
                                    }
                                    else
                                    {
                                        // End of turn
                                        EndTurn();
                                    }
                                }
                            }
                            return;
                        }
                    }
            }
            else
            {
                // Deadlock position, end of turn
                if (state.activeChecker != null || state.activeCheckerPathPoint != null)
                {
                    state.activeChecker = null;
                    state.activeCheckerPathPoint = null;
                }
            }
        }

        /* End of turn, change of moving player
         */
        private void EndTurn()
        {
            state.activeChecker = null;
            state.activeCheckerPathPoint = null;

            if (state.turn == CheckerSide.White)
            {
                state.turn = CheckerSide.Black;
                Game.userLogController.WriteMessage(Constants.localized.sideBlack);
            }
            else
            {
                state.turn = CheckerSide.White;
                Game.userLogController.WriteMessage(Constants.localized.sideWhite);
            }

            // If the game is against the computer
            if (state.isAiControlled)
            {
                Game.drawingController.PrioritizedDraw();
                // Computer's turn
                if (state.aiSide == state.turn)
                    AiTurn();
            }
        }

        /* Trying to eat all the checkers on the way
         * Control over the end result of the course
         */
        private void TryEatEnemyCheckers(BaseChecker previouslySelectedChecker, Point toPosition, Point fromPosition, Point moveDirection)
        {
            List<BaseChecker> eatenCheckers = new List<BaseChecker>(); // A list of all the eaten checkers
            for (int i = fromPosition.X + moveDirection.X; i != toPosition.X; i += moveDirection.X)
            {
                for (int j = fromPosition.Y + moveDirection.Y; j != toPosition.Y; j += moveDirection.Y)
                {
                    // Addition of all the opponent's checkers met in the course of the "eaten"
                    if (previouslySelectedChecker.Side == CheckerSide.Black)
                    {
                        foreach (BaseChecker whiteChecker in state.whiteCheckers)
                        {
                            if (whiteChecker.Position.boardPosition.X == i &&
                                whiteChecker.Position.boardPosition.Y == j)
                            {
                                eatenCheckers.Add(whiteChecker);
                            }
                        }
                    }
                    else
                    {
                        foreach (BaseChecker blackChecker in state.blackCheckers)
                        {
                            if (blackChecker.Position.boardPosition.X == i &&
                                blackChecker.Position.boardPosition.Y == j)
                            {
                                eatenCheckers.Add(blackChecker);
                            }
                        }
                    }
                }
            }

            // Destruction of all eaten checkers
            foreach (BaseChecker eatenChecker in eatenCheckers)
            {
                if (eatenChecker.Side == CheckerSide.White) state.whiteCheckers.Remove(eatenChecker);
                else state.blackCheckers.Remove(eatenChecker);

                eatenChecker.Destroy();
            }
            eatenCheckers.Clear();
        }

        /* Highlighting of all possible moves
         */
        private void HighlightPossibleTurns(BaseChecker currentSelectedChecker)
        {
            PathPoint turns = currentSelectedChecker.GetPossibleTurns();
            if (!turns.IsDeadEnd())
            {
                foreach (PathPoint point in turns[TurnDirection.TopLeft])
                    board[point.Position.X, point.Position.Y].Highlighted = true;
                foreach (PathPoint point in turns[TurnDirection.TopRight])
                    board[point.Position.X, point.Position.Y].Highlighted = true;
                foreach (PathPoint point in turns[TurnDirection.BottomLeft])
                    board[point.Position.X, point.Position.Y].Highlighted = true;
                foreach (PathPoint point in turns[TurnDirection.BottomRight])
                    board[point.Position.X, point.Position.Y].Highlighted = true;
            }
        }

        /* Clear cells highlighting
         */
        private void ClearHighlighting()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    board[j, i].Highlighted = false;
        }

        /* Select a checker (highlighting it)
         */
        private void SelectChecker(BaseChecker checker)
        {
            foreach (BaseChecker whiteChecker in state.whiteCheckers)
            {
                if (whiteChecker == checker)
                    whiteChecker.selected = true;
                else
                    whiteChecker.selected = false;
            }

            foreach (BaseChecker blackChecker in state.blackCheckers)
            {
                if (blackChecker == checker)
                    blackChecker.selected = true;
                else
                    blackChecker.selected = false;
            }
        }

        /* Undo checker selection
         */
        private void DeselectAllCheckers()
        {
            foreach (BaseChecker whiteChecker in state.whiteCheckers)
                whiteChecker.selected = false;

            foreach (BaseChecker blackChecker in state.blackCheckers)
                blackChecker.selected = false;

            ClearHighlighting();
        }

        /* Determination of the direction by vector
         */
        private TurnDirection GetDirection(Point directionVector)
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

        #endregion

        #region Saveing and loading the game

        /* Saving the game state to a file, serialization
         */
        public void SaveState(string filename)
        {
            FileStream stream = File.OpenWrite(filename);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, this.state);
            stream.Close();
        }

        /* Loading the state of the game from the file, deserialization
         */
        public void LoadState(string filename)
        {
            // Preliminary unbinding of all checkers from the drawing list
            // and removing them from the checkers list
            foreach (BaseChecker checker in state.whiteCheckers)
                checker.Destroy();
            foreach (BaseChecker checker in state.blackCheckers)
                checker.Destroy();
            state.whiteCheckers.Clear();
            state.blackCheckers.Clear();

            // Reading a state from a file
            FileStream stream = File.OpenRead(filename);
            BinaryFormatter formatter = new BinaryFormatter();
            this.state = (GameState)formatter.Deserialize(stream);
            stream.Close();

            // Adding checkers to the drawing list
            foreach (BaseChecker checker in state.whiteCheckers)
                Game.drawingController.AddToDrawingList(checker);
            foreach (BaseChecker checker in state.blackCheckers)
                Game.drawingController.AddToDrawingList(checker);

            // Redrawing the scene
            Game.drawingController.PrioritizedDraw();
        }
        #endregion
    }
}
