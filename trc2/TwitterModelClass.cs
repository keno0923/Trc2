using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TweetSharp;
using System.Windows.Forms;

namespace trc2
{
    class TwitterModelClass
    {
        TwitterViewerForm parentForm = null;

        class CachedUserData
        {
            private long? myID = null;
            private TwitterUser ownUser = null;
            private List<long> followerID = new List<long>();

            public void Clear()
            {
                myID = null;
                ownUser = null;
            }

            public long? MyID
            {
                set { myID = value; }
                get { return myID; }
            }

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
                parentForm.Invoke((MethodInvoker)delegate
                {
                    ((Form1)form).Timer1.Stop();
                    ((Form1)form).Timer1.Start();
                }); 
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
                    cuData.FollowerID.Add((long)cuData.MyID);
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
                if( cuData.MyID == null )
                    cuData.MyID = Me.Id;
                return (long)cuData.MyID;
            }
        }
    }
}
