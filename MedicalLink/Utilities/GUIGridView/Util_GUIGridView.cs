using DevExpress.XtraGrid.Views.Base;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalLink.Utilities.GUIGridView
{
    public class Util_GUIGridView
    {
        public static Point CalcPosition(RowCellCustomDrawEventArgs e, Image img)
        {
            Point p = new Point();
            p.X = e.Bounds.Location.X + (e.Bounds.Width - img.Width) / 2;
            p.Y = e.Bounds.Location.Y + (e.Bounds.Height - img.Height) / 2;
            return p;
        }
    }
}
