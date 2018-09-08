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
        public static Color activeCellColor = Color.Black; // Цвет активной ячейки
        public static Color passiveCellColor = Color.Bisque; // Цвет неактивной ячейки
        public static Color boardMarkerColor = Color.Brown; // Цвет маркера
        public static Color whiteCheckerColor = Color.Coral; // Цвет белых шашек
        public static Color blackCheckerColor = Color.Crimson; // Цвет черных шашек
        public static Color highlightCellColor = Color.LimeGreen; // Цвет подсветки ячейки
        public static Color highlightCheckerColor = Color.Maroon; // Цвет подсветки шашки

        // Ключевые слова для выделения их в поле информирования
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
