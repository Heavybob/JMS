using CefSharp;
using System;
using System.Drawing.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace JMS
{
    /// <summary>
    /// Interaction logic for RetroChat.xaml
    /// </summary>
    public partial class RetroChat : Window
    {
        public PrivateFontCollection pfc = new PrivateFontCollection();

        public RetroChat()
        {
            InitializeComponent();
            string curDir = Directory.GetCurrentDirectory();
            retroChatWebBrowser.Address = new Uri(String.Format("file:///{0}/Resources/index.html", curDir)).ToString();
        }

        public void PurgeChat()
        {
            retroChatWebBrowser.ExecuteScriptAsync(@"clear();");
        }

        public void AddChat(string message)
        {
            Regex.Escape(message);
            retroChatWebBrowser.ExecuteScriptAsync(string.Format(@"printConsole(""{0} \n"");", message));
        }
    }
}
