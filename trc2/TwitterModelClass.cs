using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TweetSharp;
using System.Windows.Forms;
using Hammock;

namespace trc2
{
    class TwitterModelClass
    {
        TwitterViewerForm parentForm = null;
        public readonly int Url_http;
        public readonly int Url_https;

        class CachedUserData
        {
            private TwitterUser ownUser = null;
            private List<long> followerID = new List<long>();

            public void Clear()
            {
                myID = null;
                ownUser = null;
            }

            public long? myID { set; get; }
            public string mySName { set; get; }

            public List<long> FollowerID
            {
                set { followerID = value; }
                get { return followerID; }
            }

            public TwitterUser OwnUser
            {
                set { ownUser = value; }
                get { return ownUser; }
            }
        }

        private CachedUserData cuData = new CachedUserData();
        private TwitterService tws;

        public TwitterService service
        {
            get { return tws; }
        }

                
        public TwitterModelClass(string ck, string cs, string act, string acts, TwitterViewerForm form)
        {
            /*
            tokens.AccessToken = act;
            tokens.AccessTokenSecret = acts;
            tokens.ConsumerKey = ck;
            tokens.ConsumerSecret = cs;
            */
            tws = new TwitterService(ck, cs, act, acts);

            parentForm = form;

            tws.StreamUser((streamEvent, response) =>
            {
                if (response.StatusCode == 0)
                {
                    if (streamEvent is TwitterUserStreamStatus)
                    {
                        TwitterStatus tweet = ((TwitterUserStreamStatus)streamEvent).Status;
                        parentForm.Invoke((MethodInvoker)delegate
                        {
                            ((Form1)parentForm).InvokedTwitterStatus(tweet);
                        });
                        
                    }else if( streamEvent is TwitterUserStreamDeleteStatus )
                    {
                        TwitterUserStreamDeleteStatus dstatus = streamEvent as TwitterUserStreamDeleteStatus;
                        parentForm.Invoke((MethodInvoker)delegate
                        {
                            ((Form1)parentForm).InvokedDeleteStatus(dstatus.StatusId);
                        });
                    }
                    else if (streamEvent is TwitterUserStreamEvent)
                    {
                        TwitterUserStreamEvent evt = streamEvent as TwitterUserStreamEvent;
                    }
                }
            }
            );

            tws.StreamFilter((streamEvent, response) =>
            {
                if (response.StatusCode == 0)
                {
                    new System.Media.SoundPlayer(@"C:\WINDOWS\Media\Windows Balloon.wav").Play();
                }
            }
            );


            RestClient client = tws._client;
            RestRequest req = tws.PrepareHammockQuery("help/configuration.json");
            RestResponse res = client.Request(req);
            Newtonsoft.Json.Linq.JContainer obj
                = Newtonsoft.Json.JsonConvert.DeserializeObject(res.Content)
                as Newtonsoft.Json.Linq.JContainer;

            int uh, uhs;

            int.TryParse(obj["short_url_length"].ToString(), out uh);
            int.TryParse(obj["short_url_length_https"].ToString(), out uhs);

            Url_http = uh;
            Url_https = uhs;

        }

        public void RefreshCache()
        {
            cuData.Clear();
        }

        public void StopUserStream()
        {
            tws.CancelStreaming();
        }

        public List<long> FollowerID
        {
            get
            {
                if (cuData.FollowerID.Count == 0)
                {
                    //  フォローされてる人のリストを取得する
                    List<long> IDs = service.ListFollowerIdsOf(new ListFollowerIdsOfOptions());
                    cuData.FollowerID = IDs;

                    //  自分のIDを追加する
                    cuData.FollowerID.Add(cuData.myID.Value);
                }
                return cuData.FollowerID;
            }
        }

        public TwitterUser Me
        {
            get
            {
                if (cuData.OwnUser == null)
                {
                    TwitterUser me = service.GetUserProfile(new GetUserProfileOptions());
                    cuData.OwnUser = me;
                }
                return cuData.OwnUser;
            }
        }

        public long MyID
        {
            get
            {
                if( cuData.myID == null )
                    cuData.myID = Me.Id;
                return cuData.myID.Value;
            }
        }

        public string MySName
        {
            get
            {
                if (cuData.mySName == null)
                    cuData.mySName = Me.ScreenName;
                return cuData.mySName;
            }
        }
    }
}
