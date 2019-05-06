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
        private BoardCell[,] board = new BoardCell[8, 8]; // Game board cells (active game object)
        private BoardMarker[,] markers = new BoardMarker[2, 8]; // Labels of the cells of the game board (1,2,3,4 ... / A, B, C, D ...)
        public GameState state = new GameState(); // The state of game objects (Checkers, etc.)
        public Size cellSize; // Cell size (to support scaling)
        private Panel gameBoard; // Link to the game board on the form


        /* The initial arrangement of all game objects
         * Register them in the drawing list
         */
        public GameplayController(ref Panel gameBoard, bool phase, bool aiAffected)
        {
            this.state.Phase = phase;
            this.gameBoard = gameBoard;

            // If there is a game with a computer
            if (aiAffected)
            {
                state.IsAiControlled = true;
                state.AiSide = phase == true ? CheckerSide.White : CheckerSide.Black;
            }

            // Setting the size of a single cell
            cellSize.Width = gameBoard.Size.Width / 8;
            cellSize.Height = gameBoard.Size.Height / 8;

            // Initializing the cells of the board
            InitializeCells();

            // Initialization of cell markers (A, B, C, D ...)
            InitializeMarkers();

            // Initial checkers placement
            InitializeCheckers(phase);

            // The turn of artificial intelligence
            Game.drawingController.PrioritizedDraw();
        }

        #region Game initialization

        /* Sets the initial checker placement
         */
        private void InitializeCheckers(bool phase)
        {
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
                    state.AllCheckers.Add(checker);
                    Game.drawingController.AddToDrawingList(checker);

                    // Initializing the Black Checker
                    checker = new Pawn(CheckerSide.Black,
                        phase == false ? CheckerMoveDirection.Downstairs : CheckerMoveDirection.Upstairs,
                        phase == false ? positionTop : positionBottom);
                    state.AllCheckers.Add(checker);
                    Game.drawingController.AddToDrawingList(checker);
                }
            }
        }

        /* Initializes cells
         */
        private void InitializeCells()
        {
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
        }

        /* Initializes markers (A, B, C, D...)
         */
        private void InitializeMarkers()
        {
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
        }

        #endregion

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
                    board[j, i].Position.ScreenPosition = new Point(j * cellSize.Width, i * cellSize.Height);
                    board[j, i].Position.DrawableSize = cellSize;
                }
            }

            // Changing the size and coordinates of the board markers
            for (int i = 0; i < 8; i++)
            {
                markers[0, i].Position.ScreenPosition = new Point(0,
                    i * cellSize.Height + (cellSize.Height / 2) - (cellSize.Height / 8));
                markers[1, i].Position.ScreenPosition = new Point(i * cellSize.Width + (cellSize.Width / 2) - (cellSize.Width / 8),
                    8 * cellSize.Height - 18);
            }

            // Changing the size and coordinates of checkers
            foreach (BaseChecker checker in state.AllCheckers)
            {
                checker.Position = new CheckersCoordinateSet(
                    new Point(checker.Position.BoardPosition.X * cellSize.Width, checker.Position.BoardPosition.Y * cellSize.Height),
                    cellSize,
                    new Point(checker.Position.BoardPosition.X, checker.Position.BoardPosition.Y));
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
                    state.ActiveChecker = null;
                    state.ActiveCheckerPathPoint = null;
                }
            }
            else
            {
                // The checker is selected
                // Prevent the move by another checker (other than the selected one)
                if (state.ActiveChecker != null)
                    if (state.ActiveChecker != currentSelectedChecker)
                    {
                        currentSelectedChecker = null;
                        return;
                    }

                List<BaseChecker> turnList = new List<BaseChecker>();
                bool aggressive = false;
                bool turnFound = false;
                foreach (BaseChecker checker in currentSelectedChecker.Side == CheckerSide.White ? state.WhiteCheckers : state.BlackCheckers)
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
            AIController ai = new AIController(state.AiSide);

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
                while (state.ActiveChecker != null);
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
            if (state.WhiteCheckers.Count == 0)
            {
                // Black's victory
                Game.drawingController.PrioritizedDraw();
                Game.userLogController.WriteMessage(Constants.localized.logBlackWon);
                FormVictory victoryDialog = new FormVictory(CheckerSide.Black, false);
                victoryDialog.ShowDialog();
            }
            else if (state.BlackCheckers.Count == 0)
            {
                // White's victory
                Game.drawingController.PrioritizedDraw();
                Game.userLogController.WriteMessage(Constants.localized.logWhiteWon);
                FormVictory victoryDialog = new FormVictory(CheckerSide.White, false);
                victoryDialog.ShowDialog();
            }
            else
            {
                // Check for "toilet" (no more moves)
                foreach (BaseChecker checker in state.TurnOwner == CheckerSide.White ? state.WhiteCheckers : state.BlackCheckers)
                {
                    if (!checker.GetPossibleTurns().IsDeadEnd())
                        toilet = false;
                }

                // If no more moves
                if (toilet)
                {
                    if (state.TurnOwner == CheckerSide.White)
                    {
                        // White lose
                        Game.userLogController.WriteMessage(Constants.localized.logWhiteToilet);
                        FormVictory victoryDialog = new FormVictory(CheckerSide.White, true);
                        victoryDialog.ShowDialog();
                    }
                    else
                    {
                        // Black lose
                        Game.userLogController.WriteMessage(Constants.localized.logBlackToilet);
                        FormVictory victoryDialog = new FormVictory(CheckerSide.Black, true);
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
            foreach (BaseChecker checker in state.AllCheckers)
            {
                // Mark previously selected checker
                if (checker.selected)
                    previouslySelectedChecker = checker;

                // Marking the checker just selected
                Rectangle checkerCoord = new Rectangle(checker.Position.ScreenPosition, checker.Position.DrawableSize);
                if (checkerCoord.Contains(mouseArgs.Location))
                {
                    SelectChecker(checker);
                    currentSelectedChecker = checker;
                    anyoneSelected = true;
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
            if (state.ActiveCheckerPathPoint == null)
            {
                turns = previouslySelectedChecker.GetPossibleTurns(); // Calculation of all possible chains of moves for a checker
                state.ActiveCheckerPathPoint = turns;
            }
            else
                turns = state.ActiveCheckerPathPoint; // Continuation of the movement

            // If the position is not a deadlock
            if (!turns.IsDeadEnd())
            {
                state.ActiveChecker = previouslySelectedChecker;

                // Movement
                for (int i = 0; i < 4; i++)
                    foreach (PathPoint point in turns[i])
                    {
                        if (toPosition == point.Position)
                        {
                            Point fromPosition = new Point()
                            {
                                X = previouslySelectedChecker.Position.BoardPosition.X,
                                Y = previouslySelectedChecker.Position.BoardPosition.Y
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
                            if (previouslySelectedChecker is Pawn && previouslySelectedChecker.Position.BoardPosition.Y ==
                                (previouslySelectedChecker.Direction == CheckerMoveDirection.Upstairs ? 0 : 7))
                            {
                                // Replacement of a checker by a king
                                King newKing = new King(previouslySelectedChecker.Side, previouslySelectedChecker.Position);
                                state.AllCheckers.Remove(previouslySelectedChecker);
                                state.AllCheckers.Add(newKing);

                                // Adding to the list of drawings
                                Game.drawingController.AddToDrawingList(newKing);
                                previouslySelectedChecker.Destroy();

                                // Calculation of the further path for a king
                                state.ActiveChecker = newKing;
                                if (state.ActiveCheckerPathPoint.afterAggression)
                                {
                                    state.ActiveCheckerPathPoint = newKing.GetPossibleTurns(DirectionHelper.GetOppositeDirection(DirectionHelper.GetDirection(moveDirection)));
                                    if (state.ActiveCheckerPathPoint.TryGetAggresiveDirections().Count == 0)
                                    {
                                        // End of turn
                                        EndTurn();
                                        return;
                                    }
                                    else
                                    {
                                        DeselectAllCheckers();
                                        state.ActiveChecker.selected = true;
                                        HighlightPossibleTurns(state.ActiveChecker);
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
                            foreach (PathPoint waypoint in state.ActiveCheckerPathPoint[DirectionHelper.GetDirection(moveDirection)])
                            {
                                if (waypoint.Position == toPosition)
                                {
                                    if (!waypoint.IsOnlyFinalTraces())
                                    {
                                        // Continuation of the movement
                                        state.ActiveCheckerPathPoint = waypoint;
                                        DeselectAllCheckers();
                                        state.ActiveChecker.selected = true;
                                        HighlightPossibleTurns(state.ActiveChecker);
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
                if (state.ActiveChecker != null || state.ActiveCheckerPathPoint != null)
                {
                    state.ActiveChecker = null;
                    state.ActiveCheckerPathPoint = null;
                }
            }
        }

        /* End of turn, change of moving player
         */
        private void EndTurn()
        {
            state.ActiveChecker = null;
            state.ActiveCheckerPathPoint = null;

            if (state.TurnOwner == CheckerSide.White)
            {
                state.TurnOwner = CheckerSide.Black;
                Game.userLogController.WriteMessage(Constants.localized.sideBlack);
            }
            else
            {
                state.TurnOwner = CheckerSide.White;
                Game.userLogController.WriteMessage(Constants.localized.sideWhite);
            }

            // If the game is against the computer
            if (state.IsAiControlled)
            {
                Game.drawingController.PrioritizedDraw();
                // Computer's turn
                if (state.AiSide == state.TurnOwner)
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
                    foreach (BaseChecker checker in previouslySelectedChecker.Side == CheckerSide.Black? state.WhiteCheckers: state.BlackCheckers)
                    {
                        if (checker.Position.BoardPosition.X == i &&
                            checker.Position.BoardPosition.Y == j)
                        {
                            eatenCheckers.Add(checker);
                        }
                    }
                }
            }

            // Destruction of all eaten checkers
            foreach (BaseChecker eatenChecker in eatenCheckers)
            {
                state.AllCheckers.Remove(eatenChecker);

                eatenChecker.Destroy();
            }
        }

        /* Highlighting of all possible moves
         */
        private void HighlightPossibleTurns(BaseChecker currentSelectedChecker)
        {
            PathPoint turns = currentSelectedChecker.GetPossibleTurns();
            if (!turns.IsDeadEnd())
            {
                foreach (TurnDirection direction in DirectionHelper.GetAllDirections())
                {
                    foreach (PathPoint point in turns[direction])
                        board[point.Position.X, point.Position.Y].Highlighted = true;
                }
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
        private void SelectChecker(BaseChecker selectedChecker)
        {
            foreach (BaseChecker checker in state.AllCheckers)
            {
                if (checker == selectedChecker)
                    checker.selected = true;
                else
                    checker.selected = false;
            }
        }

        /* Undo checker selection
         */
        private void DeselectAllCheckers()
        {
            foreach (BaseChecker checker in state.AllCheckers)
                checker.selected = false;

            ClearHighlighting();
        }

        #endregion

        #region Saving and loading the game

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
            foreach (BaseChecker checker in state.AllCheckers)
                checker.Destroy();
            state.AllCheckers.Clear();

            // Reading a state from a file
            FileStream stream = File.OpenRead(filename);
            BinaryFormatter formatter = new BinaryFormatter();
            this.state = (GameState)formatter.Deserialize(stream);
            stream.Close();

            // Adding checkers to the drawing list
            foreach (BaseChecker checker in state.AllCheckers)
                Game.drawingController.AddToDrawingList(checker);

            // Redrawing the scene
            Game.drawingController.PrioritizedDraw();
        }
        #endregion
    }
}
