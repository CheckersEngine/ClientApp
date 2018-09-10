using CheckersPolygon.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersPolygon.Controllers
{
    /* Controller, head of the management of the rendering of the game scene
     */
    internal sealed class DrawingController
    {
        private Panel gameBoard; // Reference to an object whose space will represent the game board
        private delegate void Draw(Graphics graph); // All "drawing" objects should have such a prototype of the drawing method

        /* The list of rendering game objects taking into account the priority (Z-Order)
         * The list contains the layers in the order in which they are drawn (at first everything is drawn with the index 0, then with the index 1, etc.)
         * Layers contain a list of methods for drawing objects */
        private List<List<Draw>> prioritizedDrawingList;


        public DrawingController(ref Panel gameBoard)
        {
            this.gameBoard = gameBoard;
            this.prioritizedDrawingList = new List<List<Draw>>();
            this.prioritizedDrawingList.Add(new List<Draw>()); // Adding a 0-level
        }


        /* Adding a drawing method to the drawing list according to the priority of the layer (Z-Order) of each object
         * Usually added after creating or loading an object
         */
        public void AddToDrawingList(IDrawable drawingEntity)
        {
            // Creating new layers (if they do not exist)
            while (drawingEntity.ZOrder > prioritizedDrawingList.Count - 1)
                this.prioritizedDrawingList.Add(new List<Draw>());

            // Adding a method to the desired layer
            prioritizedDrawingList[drawingEntity.ZOrder].Add(drawingEntity.Draw);
        }


        /* Removing from the drawing list
         * Usually it is called at the moment of destruction of the game object
         */
        public void DeleteFromDrawingList(IDrawable drawingEntity)
        {
            foreach (List<Draw> layer in prioritizedDrawingList)
            {
                foreach (Draw drawingCall in layer)
                {
                    if (drawingCall == drawingEntity.Draw) // Search for self-drawing method of removed object
                    {
                        layer.Remove(drawingCall);
                        return;
                    }
                }
            }
        }


        /* The main method of rendering
         * To redraw the game scene, use this method via "Game" object
         * Drawing is performed on Bitmap, after Bitmap itself is drawn
         * - this prevents the visual artifacts of the image.
         */
        public void PrioritizedDraw()
        {
            // If the window is minimized, no drawing is performed
            if (gameBoard.Width == 0 || gameBoard.Height == 0)
                return;

            Image buffer = new Bitmap(gameBoard.Width, gameBoard.Height); // Buffer has the size of the screen (Panel)
            Graphics graphicsAdapter = Graphics.FromImage(buffer);
            foreach (List<Draw> layer in prioritizedDrawingList) // Layered rendering of all game objects
            {
                foreach (Draw drawingCall in layer)
                {
                    drawingCall(graphicsAdapter); // Drawing game objects
                }
            }
            graphicsAdapter = gameBoard.CreateGraphics();
            graphicsAdapter.DrawImage(buffer, new Point(0, 0)); // Drawing the buffer on the screen
        }
    }
}
