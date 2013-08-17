using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Documents;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using TweetSharp;
using System.IO;
using System.Net;

namespace trc2
{
    public partial class Form1 : TwitterViewerForm
    {
        TwitterModelClass tmc = null;

        public String AppAccessToken { get; private set; }
        public String AppAccessTokenSecret { get; private set; }
        public String UserAccessToken { get; private set; }
        public String UserAccessTokenSecret { get; private set; }

        public Timer Timer1
        {
            get { return timer1; }
            set { timer1 = value; }
        }



        new public void InvokedTwitterStatus(TwitterStatus status)
        {
            listView1.Items.Add(TwitterViewClass.GetRecordByStatus(status, ref tmc));
            if (TwitterViewClass.isMentionToMe(status, ref tmc))
            {
                listView2.Items.Add(TwitterViewClass.GetRecordByStatus(status, ref tmc));
            }
            TwitterViewClass.PlaySoundOnTweet(status, ref tmc);
        }

        new public void InvokedDeleteStatus(long statusid)
        {
            string str = statusid.ToString();
            ListViewItem[] deleteitems = listView1.Items.Find(str, false);
            if (deleteitems.Length != 0)
            {
                foreach (ListViewItem item in deleteitems)
                {
                    item.ForeColor = Color.Red;
                }
            }
            deleteitems = listView2.Items.Find(str, false);
            if (deleteitems.Length != 0)
            {
                foreach (ListViewItem item in deleteitems)
                {
                    item.ForeColor = Color.Red;
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            tabControl1.TabPages[0].Tag = listView1;
            tabControl1.TabPages[1].Tag = listView2;
            tabControl1.TabPages[2].Tag = listView3;

            StreamReader srApp = null;

            try {
                System.IO.Directory.SetCurrentDirectory(Application.StartupPath);
                srApp = new StreamReader("../../dataapp.pass", Encoding.Default);
                AppAccessToken = srApp.ReadLine();
                AppAccessTokenSecret = srApp.ReadLine();
            }
            catch (Exception)
            {
                String Err = "アプリケーションの秘密鍵ファイルが見当たりませんでした。" +
                    "\nSearchDirectory:" + System.IO.Directory.GetCurrentDirectory();
                MessageBox.Show(Err);
                Application.Exit();
            }

            StreamReader srUser = null;
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
                //  ユーザー認証を行う
                tmc = new TwitterModelClass( AppAccessToken, AppAccessTokenSecret,
                    UserAccessToken, UserAccessTokenSecret,
                    this );

                //  自分のタイムラインを取得する
                IEnumerable<TwitterStatus> pTimeline = tmc.service.ListTweetsOnHomeTimeline(
                    new ListTweetsOnHomeTimelineOptions());

                //  自分のタイムラインをリストに追加する
                foreach (TwitterStatus status in pTimeline)
                {
                    listView1.Items.Add(TwitterViewClass.GetRecordByStatus(status, ref tmc));
                }

                IEnumerable<TwitterStatus> mTimeline = tmc.service.ListTweetsMentioningMe(
                    new ListTweetsMentioningMeOptions());

                //  自分のタイムラインをリストに追加する
                foreach (TwitterStatus status in mTimeline)
                {
                    listView2.Items.Add(TwitterViewClass.GetRecordByStatus(status, ref tmc));
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }



        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BufferedListView view = (BufferedListView)sender;
            if (view.SelectedItems.Count == 0)
                return;
            textBox1.Tag = null;

            foreach (ListViewItem prevReplyStatusItem in view.Items)
            {
                if (prevReplyStatusItem.Font.Bold == true)
                {
                    prevReplyStatusItem.Font = new Font(prevReplyStatusItem.Font, FontStyle.Regular);
                    break;
                }
            }

            ListViewItem item = view.SelectedItems[0];
            UserImageBox.Image = TwitterViewClass.GetImageFromListItem(item);
            RTUserImageBox.Image = TwitterViewClass.GetRTImageFromListItem(item);
            RTUserImageBox.Visible = (RTUserImageBox.Image != null);

            ScreenNameLabel.Text = TwitterViewClass.GetScreenNamePair(item);
            ScreenNameLabel.Links.Clear();
            ScreenNameLabel.Links.Add(0, ScreenNameLabel.Text.Length,
                "https://twitter.com/" + TwitterViewClass.GetScreenName(item));

            RTScreenLabel.Text = TwitterViewClass.GetRTScreenNamePair(item);
            if (RTScreenLabel.Text != null)
            {
                RTScreenLabel.Visible = true;
                RTScreenLabel.Links.Clear();
                RTScreenLabel.Links.Add(0, RTScreenLabel.Text.Length,
                    "https://twitter.com/" + TwitterViewClass.GetRTScreenName(item));
            }
            else
            {
                RTScreenLabel.Visible = false;
            }
            TwitterViewClass.SetLinkToTextBox(richTextBox1, item);
            TimeLabel.Text = (TwitterViewClass.GetStatusCreatedDate(item)).ToString
                ("yyyy/MM/dd HH:mm:ss");

            decimal? id = TwitterViewClass.GetInReplyToStatusId(item);
            if (id.HasValue)
            {
                ListViewItem[] replyItem = view.Items.Find(id.Value.ToString(), false);
                if (replyItem.Length != 0)
                    replyItem[0].Font = new Font(replyItem[0].Font, FontStyle.Bold);
            }
        }

        private void ScreenNameLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Link.LinkData != null)
                System.Diagnostics.Process.Start((String)e.Link.LinkData);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            if (e.LinkText.Substring(0,3) == "htt")
                System.Diagnostics.Process.Start(e.LinkText);
        }

        private void デバグ１ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void 公式RTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView lView = (ListView)tabControl1.SelectedTab.Tag;
            if (MessageBox.Show("ReTweetしていいですか？", "確認", MessageBoxButtons.OKCancel)
                == DialogResult.OK)
                TwitterViewClass.OfficialReTweet(lView.SelectedItems[0], ref tmc);
        }

        private void mentionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BufferedListView view = (BufferedListView)tabControl1.SelectedTab.Tag;
            if (view.SelectedItems.Count == 0) return;
            ListViewItem currentItem = view.SelectedItems[0];
            TwitterViewClass.SetMentionToTextBox(textBox1, currentItem);
        }

        private void 全員へMentionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BufferedListView view = (BufferedListView)tabControl1.SelectedTab.Tag;
            if (view.SelectedItems.Count == 0) return;
            ListViewItem currentItem = view.SelectedItems[0];
            TwitterViewClass.SetAllMentionToTextBox(textBox1, currentItem,ref tmc);
        }


        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            BufferedListView view = sender as BufferedListView;
            if (view.SelectedItems.Count == 0) return;

            ListViewItem currentItem = view.SelectedItems[0];
            string currentName = TwitterViewClass.GetScreenName( currentItem );
            if (e.Control && e.KeyCode == Keys.Down)
            {
                foreach (ListViewItem item in view.Items)
                {
                    if (TwitterViewClass.GetScreenName(item) == currentName &&
                        Decimal.Parse(item.Text) < Decimal.Parse(currentItem.Text))
                    {
                        item.Selected = true;
                        item.Focused = true;
                        item.EnsureVisible();
                        break;
                    }
                }
                e.Handled = true;
            }else if (e.Control && e.KeyCode == Keys.Up)
            {
                for (int i = view.Items.Count - 1; i >= 0; i--)
                {
                    ListViewItem item = view.Items[i];
                    if (TwitterViewClass.GetScreenName(item) == currentName &&
                        Decimal.Parse(item.Text) > Decimal.Parse(currentItem.Text))
                    {
                        item.Selected = true;
                        item.Focused = true;
                        item.EnsureVisible();
                        break;
                    }
                }
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.M)
            {
                if (TwitterViewClass.isMention(currentItem))
                {
                    toolTip1.Show(TwitterViewClass.GetToolTipDescription(currentItem, ref tmc), this, Control.MousePosition, 1500);
                }
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                if (MessageBox.Show("ReTweetしていいですか？", "確認", MessageBoxButtons.OKCancel)
                    == DialogResult.OK)
                    TwitterViewClass.OfficialReTweet(view.SelectedItems[0], ref tmc);
            }
        }
        
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BufferedListView view = sender as BufferedListView;
            if (view.SelectedItems.Count != 0)
            {
                ListViewItem currentItem = view.SelectedItems[0];
                TwitterViewClass.SetMentionToTextBox(textBox1, currentItem);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            string text = tb.Text;

            if (Regex.IsMatch(text, @"https?://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+"))
            {
               text = Regex.Replace(text, @"http://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+", "http xxxx xxxx xxxx xx");
               text = Regex.Replace(text, @"https://[-_.!~*'()a-zA-Z0-9;/?:@&=+$,%#]+", "httpsxxxx xxxx xxxx xxx");
            }
            
            int Remain = 140 - text.Length;
            TextLengthLabel.Text = Remain.ToString();
        }
        
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                    TwitterViewClass.UpdateStatus(textBox1, ref tmc);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }
        }

        private void 非公式RTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BufferedListView view = (BufferedListView)tabControl1.SelectedTab.Tag;
            ListViewItem currentItem = view.SelectedItems[0];
            TwitterViewClass.SetUORTToTextBox(textBox1, currentItem);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
//          tmc.StopUserStream();
//          MessageBox.Show("ストリームを停止しました。");
        }

        private void oAuth認証ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OAuth form = new OAuth();
            form.Show(this);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void tweetを開くToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BufferedListView view = (BufferedListView)tabControl1.SelectedTab.Tag;
            if (view.SelectedItems.Count == 0) return;

            ListViewItem currentItem = view.SelectedItems[0];
            TwitterViewClass.openStatusPage(currentItem);
        }

    }
}
