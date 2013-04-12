using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using TweetSharp;
using System.Net;
using System.IO;

using RichTextBoxLinks;

namespace trc2
{
    class TwitterViewClass
    {
        static Dictionary<String, Bitmap> cachedUserImage = new Dictionary<string, Bitmap>();
        static Version ver = Environment.OSVersion.Version;

        public TwitterViewClass()
        {
        }

        public static ListViewItem GetRecordByStatus(TwitterStatus status, ref TwitterModelClass tmc)
        {
            ListViewItem item = new ListViewItem();
            item.Text = status.Id.ToString();
            item.Name = item.Text;
            item.SubItems.Add(status.User.ScreenName);
            item.SubItems.Add(WebUtility.HtmlDecode(GetTextWithReplacingURL(status)));
            item.Tag = status;
            if (isMentionToMe(status, ref tmc))
                item.ImageIndex = 0;
            else if (status.User.Id == tmc.MyID)
                item.ImageIndex = 1;
            else if (status.RetweetedStatus != null)
                item.ImageIndex = 2;
    
            if (!tmc.FollowerID.Contains(status.User.Id))
                item.ForeColor = Color.Blue;

            return item;
        }

        public static void PlaySoundOnTweet(TwitterStatus status, ref TwitterModelClass tmc)
        {
            if (isMentionToMe(status, ref tmc))
                new System.Media.SoundPlayer(@"C:\WINDOWS\Media\tada.wav").Play();
            else
                if ((ver.Major == 6 && ver.Minor >= 1) || ver.Major > 6)
                {
                    // Windows 7 の機能を使用 
                    new System.Media.SoundPlayer(@"C:\WINDOWS\Media\Windows Balloon.wav").Play();
                }
                else
                {
                    new System.Media.SoundPlayer(@"C:\WINDOWS\Media\Windows XP Balloon.wav").Play();
                }
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

        public static bool isMention(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            return status.InReplyToUserId != null;
        }

        public static bool isMentionToMe(TwitterStatus status, ref TwitterModelClass tmc)
        {
            if (status.InReplyToUserId == null)
                return false;

            foreach (TwitterMention mention in status.Entities.Mentions)
            {
                if (mention.Id == tmc.MyID)
                    return true;
            }
            return false;
        }

        public static void SetLinkToTextBox(RichTextBoxEx box, ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            if (status.RetweetedStatus != null)
                status = status.RetweetedStatus;

            string str = GetTextWithReplacingURL(status);
            box.Text = str;

            Stack<Tuple<int, string,string>> URLs = new Stack<Tuple<int,string,string>>();

            foreach (TwitterUrl url in status.Entities.Urls)
            {
                string ht = url.ExpandedValue;
                int index = box.Text.IndexOf(ht);

                if (index != -1)
                {
                    URLs.Push(new Tuple<int,string,string>(index, ht, url.Value));
                    box.Text = box.Text.Remove(index, ht.Length);
                }
            }

            while( URLs.Count != 0 ){
                Tuple<int, string, string > url = URLs.Pop();
                box.InsertLink(url.Item2, url.Item3, url.Item1);
            }

            foreach (TwitterMedia media in status.Entities.Media)
            {
                string ht = media.Url;
                int index = box.Text.IndexOf(ht);

                if (index != -1)
                {
                    box.Text = box.Text.Remove(index, ht.Length);
                    box.InsertLink(ht, index);
                }
            }
/*            foreach (TwitterHashTag tag in status.Entities.HashTags)
            {
                string ht = "#"+tag.Text;
                int index = box.Text.IndexOf(ht);

                if (index != -1)
                {
                    box.Text = box.Text.Remove(index, ht.Length);
                    box.InsertLink(ht, "https://twitter.com/search?q="+ht, index);
                }
            }
*/
        }

        private static string GetTextWithReplacingURL(TwitterStatus status)
        {
            string str = status.Text.Replace("\n", "\r\n");
            foreach (TwitterUrl url in status.Entities.Urls)
            {
                str = str.Replace(url.Value, url.ExpandedValue);
            }
            return str;
        }

        public static string GetToolTipDescription(ListViewItem item, ref TwitterModelClass tmc )
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            GetTweetOptions option = new GetTweetOptions();
            option.Id = status.InReplyToStatusId.Value;
            TwitterStatus repstatus = tmc.service.GetTweet(option);

            string str = status.InReplyToScreenName + " says:\r\n" + 
                repstatus.TextDecoded;

            return str;
        }
        public static Bitmap GetImageFromListItem(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            String ImageURL = (status.RetweetedStatus != null) ?
                status.RetweetedStatus.User.ProfileImageUrlHttps :
                status.User.ProfileImageUrlHttps;
            CacheBitmapFromURL(ImageURL);

            return cachedUserImage[ImageURL];
        }

        public static Bitmap GetRTImageFromListItem(ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            if(status.RetweetedStatus != null){
                String RTImageURL = status.User.ProfileImageUrlHttps;
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
            DateTime time = (status.RetweetedStatus != null) ?
               status.RetweetedStatus.CreatedDate :
               status.CreatedDate;

            return System.TimeZoneInfo.ConvertTimeFromUtc(time,TimeZoneInfo.Local);
        }

        public static void OfficialReTweet(ListViewItem item, ref TwitterModelClass tmc)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            RetweetOptions options = new RetweetOptions();
            options.Id = status.Id;
            tmc.service.Retweet(options);
            
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

        public static void SetMentionToTextBox(TextBox tb, ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            tb.Tag = item;
            tb.Text = "@" + status.User.ScreenName + " ";
            tb.Focus();
            tb.Select(tb.TextLength, 0);
        }
 
        public static void SetUORTToTextBox(TextBox tb, ListViewItem item)
        {
            TwitterStatus status = (TwitterStatus)item.Tag;
            tb.Tag = item;
            tb.Text = " RT @" + status.User.ScreenName + ":" + status.Text;

            tb.Focus();
            tb.Select(0, 0);
        }

        public static void Clear()
        {
            cachedUserImage.Clear();
        }

        public static void UpdateStatus(TextBox tb, ref TwitterModelClass tmc)
        {
            SendTweetOptions options = new SendTweetOptions();
            tmc.service.SendTweet(options);

            if (tb.Tag != null)
            {
                //  Mentionなら
                TwitterStatus mentionedStatus = (TwitterStatus)((ListViewItem)tb.Tag).Tag;
                options.InReplyToStatusId = mentionedStatus.Id;
            }
   
            options.Status = tb.Text;
            tmc.service.SendTweet(options);
            
            tb.Clear();
        }
 
    }
}
