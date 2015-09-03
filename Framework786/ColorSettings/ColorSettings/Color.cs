using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace ColorSettings
{
    public abstract class Color
    {

        public SolidColorBrush colorOfNotePad(String _UNIQUEID)
        {
            var WhatColor = new SolidColorBrush(Windows.UI.Colors.White);

            if (_UNIQUEID == "0")
            {
                WhatColor = new SolidColorBrush(Windows.UI.Colors.White);
            }
            else if (_UNIQUEID == "1")
            {
                WhatColor = new SolidColorBrush(Windows.UI.Colors.Aqua);
            }
            else if (_UNIQUEID == "2")
            {
                WhatColor = new SolidColorBrush(Windows.UI.Colors.Orange);
            }
            else if (_UNIQUEID == "3")
            {
                WhatColor = new SolidColorBrush(Windows.UI.Colors.Magenta);
            }
            else if (_UNIQUEID == "4")
            {
                WhatColor = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            else if (_UNIQUEID == "5")
            {
                WhatColor = new SolidColorBrush(Windows.UI.Colors.Gold);
            }
            else if (_UNIQUEID == "6")
            {
                WhatColor = new SolidColorBrush(Windows.UI.Colors.LimeGreen);
            }
            else if (_UNIQUEID == "7")
            {
                WhatColor = new SolidColorBrush(Windows.UI.Colors.Violet);
            }
            else if (_UNIQUEID == "8")
            {
                WhatColor = new SolidColorBrush(Windows.UI.Colors.Blue);
            }

            return WhatColor;
        }
    }
}
