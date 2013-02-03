using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Twitterizer;
using Twitterizer.Streaming;

namespace trc2
{
    class TwitterControllerClass
    {
        public static List<Decimal> GetFollowerID( TwitterModelClass TATC )
        {
            List<Decimal> list = new List<decimal>();

            UserIdCollection IDs = TATC.FollowerID;
            foreach( Decimal id in IDs )
            {
                list.Add(id);
            }
            return list;
        }

        public static string GetScreenNameByID(Decimal ID)
        {
            TwitterResponse<TwitterUser> user = TwitterUser.Show(ID);
            UtilityClass.CheckResult(user.Result, user.ErrorMessage);
            return user.ResponseObject.ScreenName;
        }
    }
}
