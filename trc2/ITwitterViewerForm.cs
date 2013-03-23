using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TweetSharp;
using System.Windows.Forms;

namespace trc2
{
    interface ITwitterViewerForm
    {
        void InvokedTwitterStatus(TwitterStatus status);
    }

    public class TwitterViewerForm : Form, ITwitterViewerForm
    {
        public void InvokedTwitterStatus(TwitterStatus status) { }
        public void InvokedDeleteStatus(long id) { }
     }
}
