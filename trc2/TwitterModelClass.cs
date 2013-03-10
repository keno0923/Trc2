using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitterizer;
using Twitterizer.Streaming;
using System.Windows.Forms;

namespace trc2
{
    class TwitterModelClass
    {
        TwitterViewerForm parentForm = null;

        class CachedUserData
        {
            private Decimal? myID = null;
            private TwitterUser ownUser = null;
            private UserIdCollection followerID = new UserIdCollection();
            private TwitterUserCollection cachedUser = new TwitterUserCollection();

            public void Clear()
            {
                myID = null;
                ownUser = null;
                followerID.Clear();
                cachedUser.Clear();
            }

            public Decimal? MyID
            {
                set { myID = value; }
                get { return myID; }
            }

            public TwitterUser OwnUser
            {
                set { ownUser = value; }
                get { return ownUser; }
            }

            public UserIdCollection FollowerID
            {
                set { followerID = value; }
                get { return followerID; }
            }

            public void AddCacheUser(TwitterUser user)
            {
                if (!cachedUser.Contains(user))
                    cachedUser.Add(user);
            }
        }

        private OAuthTokens tokens = new OAuthTokens();
        private CachedUserData cuData = new CachedUserData();
        private TwitterStream userStream = null;

        public TwitterModelClass(string act, string acts, string ck, string cs, TwitterViewerForm form )
        {
            tokens.AccessToken = act;
            tokens.AccessTokenSecret = acts;
            tokens.ConsumerKey = ck;
            tokens.ConsumerSecret = cs;
            userStream = new TwitterStream(tokens, "beta", new StreamOptions());
            parentForm = form;

            userStream.StartUserStream(
                null,
                new StreamStoppedCallback((StopReasons stopreason) =>
                {
                    parentForm.Invoke((MethodInvoker)delegate
                    {
                        MessageBox.Show(Enum.GetName(typeof(StopReasons), stopreason));
                    });
                }
                    ),
                new StatusCreatedCallback((TwitterStatus ts) =>
                {
                    parentForm.Invoke((MethodInvoker)delegate
                    {
                        ((Form1)parentForm).InvokedTwitterStatus(ts);
                    });
                }
                    ),
                null,
                null,
                null,
                null
            );
        }

        public void RefreshCache()
        {
            cuData.Clear();
        }

        public void StartUserStream()
        {
        }

        public void OfficialReTweet(TwitterStatus status)
        {
            if( MessageBox.Show("ReTweetしていいですか？","確認",MessageBoxButtons.OKCancel) 
                == DialogResult.OK)
                TwitterStatus.Retweet(Token, status.Id);
        }

        public UserIdCollection FollowerID
        {
            get
            {
                if (cuData.FollowerID.Count == 0)
                {
                    TwitterResponse<UserIdCollection> IDs = TwitterFriendship.FollowersIds(Token);
                    UtilityClass.CheckResult(IDs.Result, IDs.ErrorMessage);
                    cuData.FollowerID = IDs.ResponseObject;
                    cuData.FollowerID.Add((Decimal)cuData.MyID);
                }
                return cuData.FollowerID;
            }
        }

        public OAuthTokens Token
        {
            get
            {
                return tokens;
            }
        }

        public TwitterUser Me
        {
            get
            {
                if (cuData.OwnUser == null)
                {
                    TwitterResponse<TwitterUser> response = TwitterAccount.VerifyCredentials(tokens);
                    UtilityClass.CheckResult(response.Result, response.ErrorMessage);
                    cuData.OwnUser = response.ResponseObject;
                }
                return cuData.OwnUser;
            }
        }

        public decimal MyID
        {
            get
            {
                if( cuData.MyID == null )
                    cuData.MyID = Me.Id;
                return (decimal)cuData.MyID;
            }
        }
    }
}
