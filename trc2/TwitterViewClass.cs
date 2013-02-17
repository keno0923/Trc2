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
 
        public static ListViewItem GetRecordByStatus(TwitterStatus status, ref TwitterModelClass tmc)
        {
            ListViewItem item = new ListViewItem();
            item.Text = status.StringId;
            item.SubItems.Add(status.User.ScreenName);
            item.SubItems.Add(status.Text);
            item.Tag = status;
            if (status.InReplyToUserId == tmc.MyID) item.ImageIndex = 0;
               else if (status.User.Id == tmc.MyID) item.ImageIndex = 1;
                else if (status.RetweetedStatus != null) item.ImageIndex = 2;
    
            if (!tmc.FollowerID.Contains(status.User.Id))
                item.ForeColor = Color.Blue;

            return item;
        }

        private static void CacheBitmapFromURL(String URL)
        {
            if (!cachedUserImage.ContainsKey(URL))
            {
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(URL);
                cachedUserImage.Add(URL, new Bitmap(stream));
                stream.Close();
            }
        }

        public static Bitmap GetImageFromListItem(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            String ImageURL = (status.RetweetedStatus != null) ?
                status.RetweetedStatus.User.ProfileImageSecureLocation :
                status.User.ProfileImageSecureLocation;
            CacheBitmapFromURL(ImageURL);

            return cachedUserImage[ImageURL];
        }

        public static Bitmap GetRTImageFromListItem(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            if(status.RetweetedStatus != null){
                String RTImageURL = status.User.ProfileImageSecureLocation;
                CacheBitmapFromURL(RTImageURL);
                return cachedUserImage[RTImageURL];
            }else{
                return null;
            }
        }

        public static decimal? GetInReplyToStatusId(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            return status.InReplyToStatusId;
        }

        public static String GetScreenName(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            return (status.RetweetedStatus != null) ?
               status.RetweetedStatus.User.ScreenName :
               status.User.ScreenName;
        }

        public static String GetRTScreenName(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            return (status.RetweetedStatus != null) ?
                status.User.ScreenName : null;
        }

        public static String GetText(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            return (status.RetweetedStatus != null) ?
              status.RetweetedStatus.Text :
              status.Text;
        }

        public static DateTime GetStatusCreatedDate(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            return (status.RetweetedStatus != null) ?
               status.RetweetedStatus.CreatedDate :
               status.CreatedDate;
        }

        public static void OfficialReTweet(ListViewItem item, ref TwitterModelClass tmc)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            tmc.OfficialReTweet(status);
        }

        public static String GetScreenNamePair(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            return (status.RetweetedStatus != null) ?
               status.RetweetedStatus.User.ScreenName + " / " + status.RetweetedStatus.User.Name :
               status.User.ScreenName + " / " + status.User.Name;
        }

        public static String GetRTScreenNamePair(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            return (status.RetweetedStatus != null) ?
                status.User.ScreenName + " / " + status.User.Name : null;
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
