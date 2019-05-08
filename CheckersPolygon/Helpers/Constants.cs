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
            LoadColorScheme();
        }

        // Current game language
        public static AvailableLanguages currentLanguage = AvailableLanguages.English;

        // Color scheme of visual elements
        public static ColorScheme colorScheme;

        // Current localized text
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

        /* Load color scheme from settings
         */
        private static void LoadColorScheme()
        {
            if (Properties.Settings.Default.ColorSchemeFile?.Length > 0)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ColorScheme));

                using (var stream = new MemoryStream(Encoding.Unicode.GetBytes(Properties.Settings.Default.ColorSchemeFile)))
                    colorScheme = (ColorScheme)serializer.Deserialize(stream);
            }
            else
                colorScheme = new ColorScheme();
        }

        /* Save color scheme to settings
         */
        public static void SaveColorScheme()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ColorScheme));
            string toWrite;
            using (MemoryStream stream = new MemoryStream())
            {
                serializer.Serialize(stream, colorScheme);

                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    toWrite = reader.ReadToEnd();
                    Properties.Settings.Default.ColorSchemeFile = toWrite;
                }
            }
            Properties.Settings.Default.Save();
        }
    }
}
