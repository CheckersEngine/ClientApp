using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersPolygon.Helpers
{
    public static class Constants
    {
        public static Color activeCellColor = Color.Black; // Color of active cell
        public static Color passiveCellColor = Color.Bisque; // Inactive cell color
        public static Color boardMarkerColor = Color.Brown; // Marker color
        public static Color whiteCheckerColor = Color.Coral; // Color of white-side checkers
        public static Color blackCheckerColor = Color.Crimson; // Color of black-side checkers
        public static Color highlightCellColor = Color.LimeGreen; // Highlight color
        public static Color highlightCheckerColor = Color.Maroon; // Highlight color checkers

        // Keywords for highlighting in the informational field
        public static Dictionary<string, Color> userLogKeywords = new Dictionary<string, Color>()
        {
            { "(?i)белы[а-я]", Color.White },
            { "(?i)черны[а-я]", Color.Black },
            { "(?i)загружена", Color.Lime },
            { "(?i)сохранена", Color.Red },
            { "(?i)победили", Color.Coral }
        };
    }
}
