using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TweetSharp;

namespace trc2
{
    public partial class OAuth : Form
    {
        TwitterService service = null;
        OAuthRequestToken requestToken = null;

        public OAuth()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 f = this.Owner as Form1;
            // Pass your credentials to the service
            service = new TwitterService(f.AppAccessToken, f.AppAccessTokenSecret);

            // Step 1 - Retrieve an OAuth Request Token
            requestToken = service.GetRequestToken();

            // Step 2 - Redirect to the OAuth Authorization URL
            Uri uri = service.GetAuthorizationUri(requestToken);
            System.Diagnostics.Process.Start(uri.ToString());
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (service == null)
                return;

           // Step 3 - Exchange the Request Token for an Access Token
            string verifier = textBox1.Text; // <-- This is input into your application by your user
            OAuthAccessToken access = service.GetAccessToken(requestToken, verifier);

            // Step 4 - User authenticates using the Access Token
            textBox2.Text = access.Token;
            textBox3.Text = access.TokenSecret;
        }
    }
}
