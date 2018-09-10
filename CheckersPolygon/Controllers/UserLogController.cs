using CheckersPolygon.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckersPolygon.Controllers
{
    /* The controller of writing messages to the information panel
     */
    sealed class UserLogController
    {
        private RichTextBox userLog; // Link to the information bar object

        public UserLogController(ref RichTextBox userLog)
        {
            this.userLog = userLog;
        }

        /* Printing a message in the information bar
         */
        public void WriteMessage(string message)
        {
            userLog.AppendText(message+'\n');
            ColorizeMessages();
        }

        /* Keyword highlighting
         */
        private void ColorizeMessages()
        {
            foreach (KeyValuePair<string, Color> pair in Constants.userLogKeywords)
            {
                // Keywords are represented in the form of regular expressions and are associated with color
                MatchCollection allMatch = Regex.Matches(userLog.Text, pair.Key); 
                foreach (Match word in allMatch)
                {
                    userLog.SelectionStart = word.Index;
                    userLog.SelectionLength = word.Length;
                    userLog.SelectionColor = pair.Value;
                }
            }
        }
    }
}
