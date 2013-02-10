using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace trc2
{
    class BufferedListView : System.Windows.Forms.ListView
    {
        protected override bool DoubleBuffered { get { return true; } set { } }
    }
}
