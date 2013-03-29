﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using TweetSharp;
using System.Net;
using System.IO;

namespace trc2
{
    class TwitterViewClass
    {
        static Dictionary<String, Bitmap> cachedUserImage = new Dictionary<string, Bitmap>();
        static Version ver = Environment.OSVersion.Version;

        public TwitterViewClass()
        {
        }

        public static bool isMentionToMe(TwitterStatus status, ref TwitterModelClass tmc)
        {
            if( status.InReplyToUserId == null )
                return false;
            return ( tmc.MyID == (long)status.InReplyToUserId );
        }

        public static ListViewItem GetRecordByStatus(TwitterStatus status, ref TwitterModelClass tmc)
        {
            ListViewItem item = new ListViewItem();
            item.Text = status.Id.ToString();
            item.Name = item.Text;
            item.SubItems.Add(status.User.ScreenName);
            item.SubItems.Add(WebUtility.HtmlDecode(status.Text));
            item.Tag = status;
            if (status.InReplyToUserId == tmc.MyID) item.ImageIndex = 0;
               else if (status.User.Id == tmc.MyID) item.ImageIndex = 1;
                else if (status.RetweetedStatus != null) item.ImageIndex = 2;
    
            if (!tmc.FollowerID.Contains(status.User.Id))
                item.ForeColor = Color.Blue;

            return item;
        }

        public static void PlaySoundOnTweet(TwitterStatus status, ref TwitterModelClass tmc)
        {
            if (status.InReplyToUserId == tmc.MyID)
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
