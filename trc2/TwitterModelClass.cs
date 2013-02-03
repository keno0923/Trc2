using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitterizer;
using Twitterizer.Streaming;

namespace trc2
{
    class TwitterModelClass
    {
        class CachedUserData
        {
            private UserIdCollection followerID = new UserIdCollection();
            private TwitterUserCollection cachedUser = new TwitterUserCollection();

            public void Clear()
            {
                followerID.Clear();
                cachedUser.Clear();
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

        public TwitterModelClass(string act, string acts)
        {
        }
            
        public TwitterModelClass(string act, string acts, string ck, string cs)
        {
            tokens.AccessToken = act;
            tokens.AccessTokenSecret = acts;
            tokens.ConsumerKey = ck;
            tokens.ConsumerSecret = cs;
            TwitterResponse<TwitterUser> response = TwitterAccount.VerifyCredentials(tokens);
            UtilityClass.CheckResult(response.Result, response.ErrorMessage);
            userStream = new TwitterStream(tokens, "beta", new StreamOptions());
        }

        public void RefreshCache()
        {
            cuData.Clear();
        }

        public void StartUserStream()
        {
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
    }
}
