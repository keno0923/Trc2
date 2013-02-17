using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace trc2
{
    class RichTextBoxEx : RichTextBox
    {
        const uint EM_GETLINECOUNT = 0x00BA;

        [DllImport("USER32.dll")]
        private extern static IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wp, IntPtr lp);

        public void debug()
        {
            IntPtr ret = SendMessage((IntPtr)this.Handle, EM_GETLINECOUNT,
                (IntPtr)0, (IntPtr)0);
            MessageBox.Show(ret.ToString());
        }

    }
}
