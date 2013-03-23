namespace trc2
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.listView1 = new trc2.BufferedListView();
            this.ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.UserName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.iconList = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new trc2.RichTextBoxEx();
            this.RTScreenLabel = new System.Windows.Forms.LinkLabel();
            this.RTUserImageBox = new System.Windows.Forms.PictureBox();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.ScreenNameLabel = new System.Windows.Forms.LinkLabel();
            this.UserImageBox = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ツールToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.デバグ１ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tweetTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.公式RTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.非公式RTToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mentionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.TextLengthLabel = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RTUserImageBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserImageBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ID,
            this.UserName,
            this.ColumnText});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(739, 192);
            this.listView1.SmallImageList = this.iconList;
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Descending;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.listView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listView1_KeyDown);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // ID
            // 
            this.ID.Text = "ID";
            this.ID.Width = 20;
            // 
            // UserName
            // 
            this.UserName.Text = "UserName";
            this.UserName.Width = 100;
            // 
            // ColumnText
            // 
            this.ColumnText.Text = "ColumnText";
            this.ColumnText.Width = 1200;
            // 
            // iconList
            // 
            this.iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("iconList.ImageStream")));
            this.iconList.TransparentColor = System.Drawing.Color.Transparent;
            this.iconList.Images.SetKeyName(0, "blue.png");
            this.iconList.Images.SetKeyName(1, "green.png");
            this.iconList.Images.SetKeyName(2, "pink.png");
            this.iconList.Images.SetKeyName(3, "red.png");
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 144);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(753, 224);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(745, 198);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(745, 198);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Controls.Add(this.RTScreenLabel);
            this.panel1.Controls.Add(this.RTUserImageBox);
            this.panel1.Controls.Add(this.TimeLabel);
            this.panel1.Controls.Add(this.ScreenNameLabel);
            this.panel1.Controls.Add(this.UserImageBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 26);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(752, 112);
            this.panel1.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Control;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.richTextBox1.Location = new System.Drawing.Point(66, 30);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(681, 54);
            this.richTextBox1.TabIndex = 6;
            this.richTextBox1.Text = "";
            this.richTextBox1.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.richTextBox1_LinkClicked);
            // 
            // RTScreenLabel
            // 
            this.RTScreenLabel.AutoSize = true;
            this.RTScreenLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RTScreenLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.RTScreenLabel.LinkColor = System.Drawing.Color.Black;
            this.RTScreenLabel.Location = new System.Drawing.Point(36, 87);
            this.RTScreenLabel.Name = "RTScreenLabel";
            this.RTScreenLabel.Size = new System.Drawing.Size(69, 15);
            this.RTScreenLabel.TabIndex = 5;
            this.RTScreenLabel.TabStop = true;
            this.RTScreenLabel.Text = "linkLabel1";
            this.RTScreenLabel.VisitedLinkColor = System.Drawing.Color.Black;
            this.RTScreenLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ScreenNameLabel_LinkClicked);
            // 
            // RTUserImageBox
            // 
            this.RTUserImageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.RTUserImageBox.Location = new System.Drawing.Point(11, 87);
            this.RTUserImageBox.Name = "RTUserImageBox";
            this.RTUserImageBox.Size = new System.Drawing.Size(19, 20);
            this.RTUserImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.RTUserImageBox.TabIndex = 4;
            this.RTUserImageBox.TabStop = false;
            this.RTUserImageBox.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // TimeLabel
            // 
            this.TimeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TimeLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TimeLabel.Location = new System.Drawing.Point(603, 11);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(149, 18);
            this.TimeLabel.TabIndex = 3;
            this.TimeLabel.Text = "0000/00/00 00:00:00";
            // 
            // ScreenNameLabel
            // 
            this.ScreenNameLabel.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ScreenNameLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.ScreenNameLabel.LinkColor = System.Drawing.Color.Black;
            this.ScreenNameLabel.Location = new System.Drawing.Point(65, 8);
            this.ScreenNameLabel.Name = "ScreenNameLabel";
            this.ScreenNameLabel.Size = new System.Drawing.Size(302, 18);
            this.ScreenNameLabel.TabIndex = 1;
            this.ScreenNameLabel.TabStop = true;
            this.ScreenNameLabel.Text = "linkLabel1";
            this.ScreenNameLabel.VisitedLinkColor = System.Drawing.Color.Black;
            this.ScreenNameLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ScreenNameLabel_LinkClicked);
            // 
            // UserImageBox
            // 
            this.UserImageBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.UserImageBox.Location = new System.Drawing.Point(11, 11);
            this.UserImageBox.Name = "UserImageBox";
            this.UserImageBox.Size = new System.Drawing.Size(48, 48);
            this.UserImageBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.UserImageBox.TabIndex = 0;
            this.UserImageBox.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ツールToolStripMenuItem,
            this.tweetTToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(752, 26);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ツールToolStripMenuItem
            // 
            this.ツールToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.デバグ１ToolStripMenuItem});
            this.ツールToolStripMenuItem.Name = "ツールToolStripMenuItem";
            this.ツールToolStripMenuItem.Size = new System.Drawing.Size(56, 22);
            this.ツールToolStripMenuItem.Text = "ツール";
            // 
            // デバグ１ToolStripMenuItem
            // 
            this.デバグ１ToolStripMenuItem.Name = "デバグ１ToolStripMenuItem";
            this.デバグ１ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.デバグ１ToolStripMenuItem.Text = "デバグ１";
            this.デバグ１ToolStripMenuItem.Click += new System.EventHandler(this.デバグ１ToolStripMenuItem_Click);
            // 
            // tweetTToolStripMenuItem
            // 
            this.tweetTToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.公式RTToolStripMenuItem,
            this.非公式RTToolStripMenuItem,
            this.mentionToolStripMenuItem});
            this.tweetTToolStripMenuItem.Name = "tweetTToolStripMenuItem";
            this.tweetTToolStripMenuItem.Size = new System.Drawing.Size(74, 22);
            this.tweetTToolStripMenuItem.Text = "Tweet(&T)";
            // 
            // 公式RTToolStripMenuItem
            // 
            this.公式RTToolStripMenuItem.Name = "公式RTToolStripMenuItem";
            this.公式RTToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.公式RTToolStripMenuItem.Text = "公式RT";
            this.公式RTToolStripMenuItem.Click += new System.EventHandler(this.公式RTToolStripMenuItem_Click);
            // 
            // 非公式RTToolStripMenuItem
            // 
            this.非公式RTToolStripMenuItem.Name = "非公式RTToolStripMenuItem";
            this.非公式RTToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.非公式RTToolStripMenuItem.Text = "非公式RT";
            this.非公式RTToolStripMenuItem.Click += new System.EventHandler(this.非公式RTToolStripMenuItem_Click);
            // 
            // mentionToolStripMenuItem
            // 
            this.mentionToolStripMenuItem.Name = "mentionToolStripMenuItem";
            this.mentionToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.mentionToolStripMenuItem.Text = "Mention";
            this.mentionToolStripMenuItem.Click += new System.EventHandler(this.mentionToolStripMenuItem_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(7, 374);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(703, 19);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // TextLengthLabel
            // 
            this.TextLengthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.TextLengthLabel.AutoSize = true;
            this.TextLengthLabel.Location = new System.Drawing.Point(716, 378);
            this.TextLengthLabel.Name = "TextLengthLabel";
            this.TextLengthLabel.Size = new System.Drawing.Size(23, 12);
            this.TextLengthLabel.TabIndex = 5;
            this.TextLengthLabel.Text = "140";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 399);
            this.Controls.Add(this.TextLengthLabel);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RTUserImageBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UserImageBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ColumnHeader ID;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ColumnHeader ColumnText;
        private System.Windows.Forms.PictureBox UserImageBox;
        private System.Windows.Forms.ColumnHeader UserName;
        private System.Windows.Forms.LinkLabel ScreenNameLabel;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.ImageList iconList;
        private System.Windows.Forms.PictureBox RTUserImageBox;
        private System.Windows.Forms.LinkLabel RTScreenLabel;
        private BufferedListView listView1;
        private RichTextBoxEx richTextBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ツールToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem デバグ１ToolStripMenuItem;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label TextLengthLabel;
        private System.Windows.Forms.ToolStripMenuItem tweetTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 公式RTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mentionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 非公式RTToolStripMenuItem;
    }
}

