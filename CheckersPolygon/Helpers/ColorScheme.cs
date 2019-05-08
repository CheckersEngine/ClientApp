using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CheckersPolygon.Helpers
{
    [Serializable]
    public class ColorScheme
    {
        [XmlElement(Type=typeof(XmlColor))]
        public Color ActiveCellColor { get; set; } = Color.Black; // Color of active cell

        [XmlElement(Type = typeof(XmlColor))]
        public Color PassiveCellColor { get; set; } = Color.Bisque; // Inactive cell color

        [XmlElement(Type = typeof(XmlColor))]
        public Color BoardMarkerColor { get; set; } = Color.Brown; // Marker color

        [XmlElement(Type = typeof(XmlColor))]
        public Color WhiteCheckerColor { get; set; } = Color.Coral; // Color of white-side checkers

        [XmlElement(Type = typeof(XmlColor))]
        public Color BlackCheckerColor { get; set; } = Color.Crimson; // Color of black-side checkers

        [XmlElement(Type = typeof(XmlColor))]
        public Color HighlightCellColor { get; set; } = Color.LimeGreen; // Highlight color

        [XmlElement(Type = typeof(XmlColor))]
        public Color HighlightCheckerColor { get; set; } = Color.Maroon; // Highlight color checkers
    }
}
