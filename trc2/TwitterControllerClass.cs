using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TweetSharp;

namespace trc2
{
    class TwitterControllerClass
    {
        public static bool isUserProtected(TwitterUser User)
        {
            if (User.IsProtected.HasValue && User.IsProtected.Value)
                return true;
            return false;
        }
    }
}
