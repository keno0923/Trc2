using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Twitterizer;
using Twitterizer.Streaming;
using System.Net;
using System.IO;

namespace trc2
{
    class TwitterViewClass
    {
        static Dictionary<String, Bitmap> cachedUserImage = new Dictionary<string, Bitmap>();
 
        public static ListViewItem GetRecordByStatus(TwitterStatus status)
        {
            ListViewItem item = new ListViewItem();
            item.Text = status.StringId;
            item.SubItems.Add(status.User.ScreenName);
            item.SubItems.Add(status.Text);
            item.SubItems.Add(status.InReplyToStatusId.ToString());
            item.Tag = status;
            return item;
        }

        public static Bitmap GetImageFromListItem(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            String ImageURL = status.User.ProfileImageSecureLocation;
            if( !cachedUserImage.ContainsKey(ImageURL) )
            {
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(ImageURL);
                cachedUserImage.Add(ImageURL, new Bitmap(stream));
                stream.Close();
            }

            return cachedUserImage[ImageURL];
        }

        public static String GetScreenName(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            return status.User.ScreenName;
        }

        public static String GetText(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            return status.Text;
        }

        public static DateTime GetStatusCreatedDate(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            return status.CreatedDate;
        }

        public static String GetScreenNamePair(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            return status.User.ScreenName + " / " + status.User.Name;
        }

        public static void SetItemColorByFollowedUser(TwitterModelClass tmclass, ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            if (!tmclass.FollowerID.Contains(status.Id))
            {

            }
        } 

        public static void Clear()
        {
            cachedUserImage.Clear();
        }
 
    }
}
