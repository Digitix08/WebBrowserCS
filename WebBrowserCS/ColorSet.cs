using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebBrowserCS
{
    class ColorSet
    {
        public static void SetColorIncludingChildren(Control parent, Type controlType, Color colorBack, Color colorFront)
        {
            Color backColor, textColor;
            if (colorBack == default) { backColor = Properties.Settings.Default.Elembgcolor; }
            else backColor = colorBack;
            if (colorFront == default) { textColor = Properties.Settings.Default.Textcolor; }
            else textColor = colorFront;
            if (backColor == Color.Transparent) backColor = Color.White;
            if (textColor == Color.Transparent) textColor = Color.White;
            if (textColor == backColor)
            {
                Color inv = Properties.Settings.Default.Textcolor;
                inv = Color.FromArgb(inv.A, (255 - inv.R), (255 - inv.G), (255 - inv.B));
                textColor = inv;
            }
            if (parent.GetType() == controlType)
            {
                parent.BackColor = backColor;
                parent.ForeColor = textColor;
            }

            foreach (Control child in parent.Controls)
            {
                SetColorIncludingChildren(child, controlType, default, default);
            }
        }
    }
}
