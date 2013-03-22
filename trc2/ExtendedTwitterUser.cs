using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using TweetSharp;

namespace trc2
{
    class ExtendedTwitterUser : TwitterUser 
    {
        Bitmap UserIconBitmap = null;

        public Bitmap UserIcon
        {
            get{return UserIconBitmap;}
            set{UserIconBitmap=value;}
        }
    }
}
