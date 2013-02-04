using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Twitterizer;
using System.IO;

namespace trc2
{
    public partial class Form1 : TwitterViewerForm
    {
        TwitterModelClass tatc = null;
        List<ListView> listViewList = new List<ListView>();

        new public void InvokedTwitterStatus(TwitterStatus status)
        {
            listView1.Items.Add(TwitterViewClass.GetRecordByStatus(status));
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StreamReader srApp = null;
            String AppAccessToken = null;
            String AppAccessTokenSecret = null;

            try {
                srApp = new StreamReader("../../dataapp.pass", Encoding.Default);
                AppAccessToken = srApp.ReadLine();
                AppAccessTokenSecret = srApp.ReadLine();
            }
            catch (Exception)
            {
                MessageBox.Show("アプリケーションの秘密鍵ファイルが見当たりませんでした。");
                Application.Exit();
            }

            StreamReader srUser = null;
            String UserAccessToken = null;
            String UserAccessTokenSecret = null;
            try
            {
                srUser = new StreamReader("../../datauser.pass", Encoding.Default);
                UserAccessToken = srUser.ReadLine();
                UserAccessTokenSecret = srUser.ReadLine();
            }
            catch (Exception)
            {
                MessageBox.Show("ユーザーの秘密鍵ファイルが見当たりませんでした。");
                Application.Exit();
            }

            try
            {
                tatc = new TwitterModelClass(AppAccessToken, AppAccessTokenSecret,
                    UserAccessToken, UserAccessTokenSecret,
                    this );

                TwitterResponse<TwitterStatusCollection> pTimeline
                    = TwitterTimeline.HomeTimeline(tatc.Token);
                UtilityClass.CheckResult(pTimeline.Result, pTimeline.ErrorMessage);

                foreach (TwitterStatus status in pTimeline.ResponseObject)
                {
                    listView1.Items.Add(TwitterViewClass.GetRecordByStatus(status));
                }
                listViewList.Add(listView1);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
                return;
            ListViewItem item = listView1.SelectedItems[0];
            UserImageBox.Image = TwitterViewClass.GetImageFromListItem(item);
            ScreenNameLabel.Text = TwitterViewClass.GetScreenNamePair(item);
            ScreenNameLabel.Links.Clear();
            ScreenNameLabel.Links.Add(0, ScreenNameLabel.Text.Length,
                "https://twitter.com/" + TwitterViewClass.GetScreenName(item));
            TextLabel.Text = TwitterViewClass.GetText(item);
            TextLabel.Links.Clear();
            TimeLabel.Text = (TwitterViewClass.GetStatusCreatedDate(item)).ToString
                ("yyyy/MM/dd HH:mm:ss");

        }

        private void ScreenNameLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Link.LinkData != null)
                System.Diagnostics.Process.Start((String)e.Link.LinkData);
        }
    }
}
