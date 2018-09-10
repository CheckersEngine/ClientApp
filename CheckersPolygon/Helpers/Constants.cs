using CheckersPolygon.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CheckersPolygon.Helpers
{
    public static class Constants
    {
        static Constants()
        {
            LoadLocalization((AvailableLanguages)Properties.Settings.Default.Language);
        }

        public static AvailableLanguages currentLanguage = AvailableLanguages.English;

        public static Color activeCellColor = Color.Black; // Color of active cell
        public static Color passiveCellColor = Color.Bisque; // Inactive cell color
        public static Color boardMarkerColor = Color.Brown; // Marker color
        public static Color whiteCheckerColor = Color.Coral; // Color of white-side checkers
        public static Color blackCheckerColor = Color.Crimson; // Color of black-side checkers
        public static Color highlightCellColor = Color.LimeGreen; // Highlight color
        public static Color highlightCheckerColor = Color.Maroon; // Highlight color checkers

        public static LocalizedStrings localized;

        // Keywords for highlighting in the informational field
        public static Dictionary<string, Color> userLogKeywords;

        // Localizations
        public static readonly Dictionary<string, AvailableLanguages> localizations = new Dictionary<string, AvailableLanguages>()
        {
            { "EN", AvailableLanguages.English },
            { "RU", AvailableLanguages.Russian }
        };

        /* Load localization for chosen language
         */
        public static void LoadLocalization(AvailableLanguages language)
        {
            string file;
            switch (language)
            {
                case AvailableLanguages.English:
                    file = Properties.Resources.English;
                    break;
                case AvailableLanguages.Russian:
                    file = Properties.Resources.Russian;
                    break;
                default:
                    file = Properties.Resources.English;
                    break;
            }

            XmlSerializer serializer = new XmlSerializer(typeof(LocalizedStrings));
            MemoryStream memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(file));
            localized = (LocalizedStrings)serializer.Deserialize(memoryStream);
            memoryStream.Close();

            userLogKeywords = new Dictionary<string, Color>()
            {
                { localized.regExWhite, Color.White },
                { localized.regExBlack, Color.Black },
                { localized.regExLoaded, Color.Lime },
                { localized.regExSaved, Color.Red },
                { localized.regExWon, Color.Coral }
            };

            currentLanguage = language;
        }
    }
}
