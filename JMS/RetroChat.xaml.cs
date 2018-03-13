using CefSharp;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            retroChatWebBrowser.ExecuteScriptAsync(string.Format(@"print(""{0} \n"");", message));
        }
    }
}
