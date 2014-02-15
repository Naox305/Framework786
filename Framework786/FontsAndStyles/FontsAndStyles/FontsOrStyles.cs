using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace FontsAndStyles
{
    public abstract class FontsOrStyles
    {
        static private int NextFontElement;
        private String NewFont;
        private static readonly List<String> AllFonts = new List<String> { "Times New Roman", "Segeo UI", "Comic Sans MS", "Courier New", "Arial", "Verdana", "Segoe Script" };

        public string FontsPicker(RichEditBox ThisSelectedText, List<string> AvalibleFonts = null)
        {
            if (AvalibleFonts == null)
                AvalibleFonts = AllFonts;

            if (NextFontElement >= AvalibleFonts.Count() - 1)
                NextFontElement = 0;
            else
                NextFontElement++;

            try
            {
                NewFont = AvalibleFonts.ElementAt(NextFontElement);
                ThisSelectedText.FontFamily = new FontFamily(NewFont);
            }
            catch
            {
                NewFont = "OutofRange";
            };

            return NewFont + '\n' + (NextFontElement + 1).ToString() + " of " + AllFonts.Count.ToString();  
  
        }

        public void Bold(RichEditBox ThisSelectedText)
        {
            if (ThisSelectedText.Document.Selection.CharacterFormat.Weight == Windows.UI.Text.FontWeights.ExtraBold.Weight)
                ThisSelectedText.Document.Selection.CharacterFormat.Weight = Windows.UI.Text.FontWeights.Normal.Weight;
            else
                ThisSelectedText.Document.Selection.CharacterFormat.Weight = Windows.UI.Text.FontWeights.ExtraBold.Weight;
        }

        public void Italic(RichEditBox ThisSelectedText)
        {
            if (ThisSelectedText.Document.Selection.CharacterFormat.FontStyle == Windows.UI.Text.FontStyle.Italic)
                ThisSelectedText.Document.Selection.CharacterFormat.FontStyle = Windows.UI.Text.FontStyle.Normal;
            else
                ThisSelectedText.Document.Selection.CharacterFormat.FontStyle = Windows.UI.Text.FontStyle.Italic;
        }

        public void Underline(RichEditBox ThisSelectedText)
        {
            if (ThisSelectedText.Document.Selection.CharacterFormat.Underline == Windows.UI.Text.UnderlineType.Single)
                ThisSelectedText.Document.Selection.CharacterFormat.Underline = Windows.UI.Text.UnderlineType.None;
            else
                ThisSelectedText.Document.Selection.CharacterFormat.Underline = Windows.UI.Text.UnderlineType.Single;
        }
    }
}
