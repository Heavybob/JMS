using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Media;
using System.Windows.Documents;

using SharpTalk;
using CSCore;
using CSCore.SoundOut;

using TwitchLib;
using TwitchLib.Events.Client;
using TwitchLib.Enums;
using TwitchLib.Models.Client;
using CSCore.Codecs.RAW;
using Cookie;
using System.Windows.Interop;

namespace JMS
{
    public partial class MainWindow : Window
    {
        delegate void StringArgReturningVoidDelegate(string text);
        delegate void ChatArgReturningVoidDelegate(string text, string colorHex);
        public ConnectionCredentials credentials;
        public CookieClient cleverbotSession = new CookieClient(new CookieConfig(){ CleverbotKey = "CC73yeuOpWCGkSvKOfijz5paHLQ" });
        public TwitchClient client;
        public RetroChat childForm;
        public FonixTalkEngine tts = new FonixTalkEngine();
        public List<WaveOut> ttsWavs = new List<WaveOut>();
        public string lastChatMessage = "";
        public string queuedMessage = "";
        public string[,] filter = {
            // Emotes
            {":)", "Happy Face"},
            {":D", "Extremely Happy Face"},
            {":(", "Sad Face"},
            {":o", "Surprised face"},
            {":z", "Tired Face"},
            {"B)", "Cool Face"},
            {":/", "Concerned Face"},
            {";)", "Winkie Face"},
            {":p", "Winkie Tounge"},
            {"R)", "​YAAARRR﻿"},
            {";p", "Tongue"},
            {"o_O", "​uhhhhh﻿"},
            {">(", "L O L"},
            //{"<3", "HARTEEEEEEEE﻿﻿﻿"},
            {":|", "CONfusION"},
            {"omg", "OOMG﻿"},
            // Channel Emotes
            {"bobheaJMS", "John Madden"},
            {"clawRM", "RIGGED"},
            {"useles2Duck", "Useless Duck Company"},
            {"SnickersHype", "hype"}, // Had to block this because it sounds like nigger hype
            // Voices
            {"[:hl]", "[:dv ap 10]"},
            {"[:sv]", "[:dv ap 900]"},
            {"[:sm]", "[:dv pr 500]"},
            {"[:tm]", "[:dv gv 65 pr 1 hs 130]"},
            {"[:bp]", "[:dv gv 65 pr 1 hs 125]"}
        };

        public List<List<string>> tones = new List<List<string>> {
            new List<string> {"1","65"},
            new List<string> {"!","69"},
            new List<string> {"2","73"},
            new List<string> {"@","78"},
            new List<string> {"3","82"},
            new List<string> {"4","87"},
            new List<string> {"$","93"},
            new List<string> {"5","98"},
            new List<string> {"%","104"},
            new List<string> {"6","110"},
            new List<string> {"^","117"},
            new List<string> {"7","123"},
            new List<string> {"8","131"},
            new List<string> {"*","139"},
            new List<string> {"9","147"},
            new List<string> {"(","156"},
            new List<string> {"0","165"},
            new List<string> {"q","175"},
            new List<string> {"Q","185"},
            new List<string> {"w","196"},
            new List<string> {"W","208"},
            new List<string> {"e","220"},
            new List<string> {"E","233"},
            new List<string> {"r","247"},
            new List<string> {"t","262"},
            new List<string> {"T","277"},
            new List<string> {"y","294"},
            new List<string> {"Y","311"},
            new List<string> {"u","330"},
            new List<string> {"i","349"},
            new List<string> {"I","370"},
            new List<string> {"o","392"},
            new List<string> {"O","415"},
            new List<string> {"p","440"},
            new List<string> {"P","466"},
            new List<string> {"a","494"},
            new List<string> {"s","523"},
            new List<string> {"S","554"},
            new List<string> {"d","587"},
            new List<string> {"D","622"},
            new List<string> {"f","659"},
            new List<string> {"g","698"},
            new List<string> {"G","740"},
            new List<string> {"h","784"},
            new List<string> {"H","831"},
            new List<string> {"j","880"},
            new List<string> {"J","932"},
            new List<string> {"k","988"},
            new List<string> {"l","1047"},
            new List<string> {"L","1109"},
            new List<string> {"z","1175"},
            new List<string> {"Z","1245"},
            new List<string> {"x","1319"},
            new List<string> {"c","1397"},
            new List<string> {"C","1480"},
            new List<string> {"v","1568"},
            new List<string> {"V","1661"},
            new List<string> {"b","1760"},
            new List<string> {"B","1865"},
            new List<string> {"n","1976"},
            new List<string> {"m","2093"},
            new List<string> {"{",".5"},
            new List<string> {"}","1"},
            new List<string> {" ","0"},
            new List<string> {"|", "0"},
        };
        public int moderators = 0;
        public static List<Tuple<string, string>> buffer = new List<Tuple<string, string>>();


        public string[] regexPattern = {
            @"\[:rate([^\]]+)\]",
            @"\[:comma([^\]]+)\]",
            @"\[:period([^\]]+)\]",
            @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)",
            @"\[:dial\]",
            @"\[:(?:tone|t)\]"
        };

        public bool playNext = true;

        public MainWindow()
        {
            InitializeComponent();
            GenerateDeviceList();
            LoadSettings();
            if ((client == null || !client.IsConnected) && usernameTextBox.Text != "" && OAuthTextBox.Password != "" && (bool)autoConnectCheckBox.IsChecked)
            {
                try
                {
                    credentials = new ConnectionCredentials(usernameTextBox.Text, OAuthTextBox.Password);
                    client = new TwitchClient(credentials);
                    //cleverbotSession = CleverbotSession.NewSession("jOPGMvjNhM10VZIB", "AzHz9wUZbkGXjbrUIdRD7fO0chmfZPQD");
                }
                catch (Exception)
                {
                    return;
                }
                SetStatus("Connecting...");


                client.OnMessageReceived += new EventHandler<OnMessageReceivedArgs>(OnChatMessageReceived);
                client.OnConnected += new EventHandler<OnConnectedArgs>(OnConnected);
                client.OnDisconnected += new EventHandler<OnDisconnectedArgs>(OnDisconnected);
                client.OnModeratorsReceived += new EventHandler<OnModeratorsReceivedArgs>(OnModeratorsReceived);
                client.OnChatCommandReceived += new EventHandler<OnChatCommandReceivedArgs>(OnCommandReceived);

                SaveSettings();

                client.Connect();
            }
        }

        public void GenerateDeviceList()
        {
            // Look for device to output sound to
            foreach (WaveOutDevice device in WaveOutDevice.EnumerateDevices())
            {
                deviceComboBox.Items.Add(device.Name);
            }
            deviceComboBox.SelectedIndex = 0;
        }

        private void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            if ((client == null || !client.IsConnected) && usernameTextBox.Text != "" && OAuthTextBox.Password != "")
            {
                try
                {
                    credentials = new ConnectionCredentials(usernameTextBox.Text, OAuthTextBox.Password);
                    client = new TwitchClient(credentials);
                    //cleverbotSession = CleverbotSession.NewSession("jOPGMvjNhM10VZIB", "AzHz9wUZbkGXjbrUIdRD7fO0chmfZPQD");
                }
                catch (Exception)
                {
                    return;
                }
                SetStatus("Connecting...");


                client.OnMessageReceived += new EventHandler<OnMessageReceivedArgs>(OnChatMessageReceived);
                client.OnConnected += new EventHandler<OnConnectedArgs>(OnConnected);
                client.OnDisconnected += new EventHandler<OnDisconnectedArgs>(OnDisconnected);
                client.OnModeratorsReceived += new EventHandler<OnModeratorsReceivedArgs>(OnModeratorsReceived);
                client.OnChatCommandReceived += new EventHandler<OnChatCommandReceivedArgs>(OnCommandReceived);

                SaveSettings();

                client.Connect();
            }
        }

        private void DiconnectButton_Click(object sender, RoutedEventArgs e)
        {
            if (client != null && client.IsConnected)
            {
                SetStatus("Disconnecting...");
                client.LeaveChannel(Properties.Settings.Default.Username);

                client.Disconnect();
            }
        }

        private void SendCommandButton_Click(object sender, RoutedEventArgs e)
        {
            //client.SendMessage(chatCommandBox.Text);
            string text = chatCommandBox.Text;
            AddChat(chatCommandBox.Text);
            chatCommandBox.Text = "";
            new Thread(delegate () { TTS(text); }).Start();
        }

        private void ChatCommandBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string text = chatCommandBox.Text;
                AddChat(chatCommandBox.Text);
                chatCommandBox.Text = "";
                e.Handled = true;
                new Thread(delegate () { TTS(text); }).Start();
            }
        }

        private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            SaveSettings();
        }

        private void AddChat(string text)
        {
            if (!chatTextBox.CheckAccess())
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(AddChat);
                Dispatcher.Invoke(d, new object[] { text });
            }
            else
            {
                chatTextBox.AppendText(text + "\r\n", "#000000");
                chatTextBox.ScrollToEnd();
                /*foreach (char c in text.ToCharArray())
                {
                    buffer.Add(new Tuple<string, string>(c.ToString(), "#000000"));
                }

                buffer.Add(new Tuple<string, string>("\r\n", "#000000"));*/
                if (childForm != null)
                {
                    childForm.AddChat(text);
                }
            }
        }

        private void AddChat(string text, string colorHex)
        {
            if (!chatTextBox.CheckAccess())
            {
                ChatArgReturningVoidDelegate d = new ChatArgReturningVoidDelegate(AddChat);
                Dispatcher.Invoke(d, new object[] { text, colorHex });
            }
            else
            {
                chatTextBox.AppendText(text, colorHex);
                chatTextBox.ScrollToEnd();
                /*foreach (char c in text.ToCharArray())
                {
                    buffer.Add(new Tuple<string, string>(c.ToString(), colorHex));
                }*/
                if (childForm != null)
                {
                    childForm.AddChat(text);
                }
            }
        }

        private void SetStatus(string text)
        {
            if (!statusLabel.CheckAccess())
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(SetStatus);
                Dispatcher.Invoke(d, new object[] { text });
            }
            else
            {
                statusLabel.Content = "Status: " + text;
            }
        }

        private int GetDeviceID()
        {
            if (!deviceComboBox.CheckAccess())
            {
                return deviceComboBox.Dispatcher.Invoke(
                    new Func<int>(() => GetDeviceID())
                );
            }
            else
            {
                return deviceComboBox.SelectedIndex;
            }
        }

        private int GetChatModeID()
        {
            if (!chatModeComboBox.CheckAccess())
            {
                return chatModeComboBox.Dispatcher.Invoke(
                    new Func<int>(() => GetChatModeID())
                );
            }
            else
            {
                return chatModeComboBox.SelectedIndex;
            }
        }

        private int GetVoiceID()
        {
            if (!voiceComboBox.CheckAccess())
            {
                return voiceComboBox.Dispatcher.Invoke(
                    new Func<int>(() => GetVoiceID())
                );
            }
            else
            {
                return voiceComboBox.SelectedIndex;
            }
        }

        private bool GetCleverbotEnabled()
        {
            if (!voiceComboBox.CheckAccess())
            {
                return cleverBotCheckBox.Dispatcher.Invoke(
                    new Func<bool>(() => GetCleverbotEnabled())
                );
            }
            else
            {
                return (bool)cleverBotCheckBox.IsChecked;
            }
        }

        private bool GetDialTonesEnabled()
        {
            if (!dialToneCheckBox.CheckAccess())
            {
                return dialToneCheckBox.Dispatcher.Invoke(
                    new Func<bool>(() => GetDialTonesEnabled())
                );
            }
            else
            {
                return (bool)dialToneCheckBox.IsChecked;
            }
        }

        private bool GetModeratorsEnabled()
        {
            if (!moderatorsIgnoreCheckBox.CheckAccess())
            {
                return moderatorsIgnoreCheckBox.Dispatcher.Invoke(
                    new Func<bool>(() => GetModeratorsEnabled())
                );
            }
            else
            {
                return (bool)moderatorsIgnoreCheckBox.IsChecked;
            }
        }

        private bool GetBitsEnabled()
        {
            if (!bitsIgnoreCheckBox.CheckAccess())
            {
                return bitsIgnoreCheckBox.Dispatcher.Invoke(
                    new Func<bool>(() => GetBitsEnabled())
                );
            }
            else
            {
                return (bool)bitsIgnoreCheckBox.IsChecked;
            }
        }

        private bool GetSubscribersEnabled()
        {
            if (!subsIgnoreCheckBox.CheckAccess())
            {
                return subsIgnoreCheckBox.Dispatcher.Invoke(
                    new Func<bool>(() => GetSubscribersEnabled())
                );
            }
            else
            {
                return (bool)subsIgnoreCheckBox.IsChecked;
            }
        }

        private bool GetUsersEnabled()
        {
            if (!usersIgnoreCheckBox.CheckAccess())
            {
                return usersIgnoreCheckBox.Dispatcher.Invoke(
                    new Func<bool>(() => GetDialTonesEnabled())
                );
            }
            else
            {
                return (bool)usersIgnoreCheckBox.IsChecked;
            }
        }

        private int GetBitThreshold()
        {
            if (!bitsThreshold.CheckAccess())
            {
                return bitsThreshold.Dispatcher.Invoke(
                    new Func<int>(() => GetBitThreshold())
                );
            }
            else
            {
                return (int)bitsThreshold.Value;
            }
        }

        private int GetVPTempo()
        {
            if (!vpTempo.CheckAccess())
            {
                return vpTempo.Dispatcher.Invoke(
                    new Func<int>(() => GetVPTempo())
                );
            }
            else
            {
                return (int)vpTempo.Value;
            }
        }

        public void OnConnected(object sender, OnConnectedArgs e)
        {
            //CheckForIllegalCrossThreadCalls = false;
            client.JoinChannel(Properties.Settings.Default.Username);
            AddChat("<< Connected to chat server >>");
            SetStatus("Connected to chat server");
        }

        public void OnDisconnected(object sender, OnDisconnectedArgs e)
        {
            //CheckForIllegalCrossThreadCalls = false;
            AddChat("<< Disconnected from channel >>");
            SetStatus("Disconnected");
        }

        private void OnModeratorsReceived(object sender, OnModeratorsReceivedArgs e)
        {
            moderators = e.Moderators.Count;
        }

        private void OnCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            switch (e.Command.CommandText.ToLower())
            {
                case "stop":
                    if (e.Command.ChatMessage.UserType == UserType.Broadcaster ||
                        e.Command.ChatMessage.UserType == UserType.Admin ||
                        e.Command.ChatMessage.UserType == UserType.Staff ||
                        e.Command.ChatMessage.UserType == UserType.GlobalModerator ||
                        e.Command.ChatMessage.IsModerator)
                    {
                        client.SendMessage($"@{e.Command.ChatMessage.Username} stopped all sounds!");
                        foreach (WaveOut wav in ttsWavs)
                        {
                            if (wav.PlaybackState == PlaybackState.Playing)
                                wav.Stop();
                        }
                        ttsWavs.Clear();
                        queuedMessage = "";
                    }
                    break;

                case "purge":
                    if (e.Command.ChatMessage.UserType == UserType.Broadcaster ||
                        e.Command.ChatMessage.UserType == UserType.Admin ||
                        e.Command.ChatMessage.UserType == UserType.Staff ||
                        e.Command.ChatMessage.UserType == UserType.GlobalModerator ||
                        e.Command.ChatMessage.IsModerator)
                    {
                        client.SendMessage($"@{e.Command.ChatMessage.Username} purged chat!");

                        if (childForm != null || !WindowHelper.IsDisposed(childForm))
                        {
                            buffer.Clear();
                            childForm.PurgeChat();
                        }
                    }
                    break;

                case "nuke":
                    if (e.Command.ChatMessage.UserType == UserType.Broadcaster ||
                        e.Command.ChatMessage.UserType == UserType.Admin ||
                        e.Command.ChatMessage.UserType == UserType.Staff ||
                        e.Command.ChatMessage.UserType == UserType.GlobalModerator ||
                        e.Command.ChatMessage.IsModerator)
                    {
                        client.SendMessage($"@{e.Command.ChatMessage.Username} nuked it all!");
                        AddChat("<< Chat has been nukes >>");
                        foreach (WaveOut wav in ttsWavs)
                        {
                            if (wav.PlaybackState == PlaybackState.Playing)
                                wav.Stop();
                        }
                        ttsWavs.Clear();
                        queuedMessage = "";

                        if (childForm != null || !WindowHelper.IsDisposed(childForm))
                        {
                            buffer.Clear();
                            childForm.PurgeChat();
                        }
                    }
                    break;
            }
        }

        private void OnChatMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            File.AppendAllText("chatlog.txt", "\n" + DateTime.Now.ToString() + " - " + e.ChatMessage.DisplayName + ": " + e.ChatMessage.Message);
            /*if (e.ChatMessage.Color != null)
            {
                AddChat(e.ChatMessage.DisplayName, e.ChatMessage.ColorHex);
                AddChat(": " + e.ChatMessage.Message);
            }
            else
            {
                AddChat(e.ChatMessage.DisplayName + ": " + e.ChatMessage.Message);
            }*/
            AddChat(e.ChatMessage.DisplayName + ": " + e.ChatMessage.Message);

            if (!e.ChatMessage.Message.StartsWith("!") && (
                e.ChatMessage.UserType == UserType.Broadcaster ||
                e.ChatMessage.UserType == UserType.Admin ||
                e.ChatMessage.UserType == UserType.GlobalModerator ||
                e.ChatMessage.UserType == UserType.Staff ||
                e.ChatMessage.IsModerator && GetModeratorsEnabled() ||
                e.ChatMessage.Bits >= GetBitThreshold() && GetBitsEnabled() ||
                e.ChatMessage.IsSubscriber && GetSubscribersEnabled() ||
                e.ChatMessage.UserType == UserType.Viewer && GetUsersEnabled())
                )
            {
                switch (GetChatModeID())
                {
                    case 0:
                        if (playNext)
                        {
                            playNext = false;
                            new Thread(delegate () { TTS(e); }).Start();
                        }
                        break;

                    case 1:
                        if (playNext)
                        {
                            playNext = false;
                            string input = queuedMessage + e.ChatMessage.Message;
                            new Thread(delegate () { TTS(input); }).Start();
                            queuedMessage = "";
                        }
                        else
                        {
                            queuedMessage += e.ChatMessage.Message + "\r\n";
                        }
                        break;

                    case 2:
                        new Thread(delegate () { TTS(e); }).Start();
                        break;

                    default:
                        break;
                }
            }
        }

        private void OnDonationPageClick(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://ggs.sx/jms");
        }

        private void OnSettingsChanged(object sender, EventArgs e)
        {
            //SaveSettings();
        }

        private void SaveSettings()
        {
            Properties.Settings.Default.Username = usernameTextBox.Text;
            Properties.Settings.Default.OAuth_Token = OAuthTextBox.Password;
            Properties.Settings.Default.DeviceID = deviceComboBox.SelectedIndex;
            Properties.Settings.Default.ChatModeID = chatModeComboBox.SelectedIndex;
            Properties.Settings.Default.EnableDialTones = (bool)dialToneCheckBox.IsChecked;
            Properties.Settings.Default.EnableModerators = (bool)moderatorsIgnoreCheckBox.IsChecked;
            Properties.Settings.Default.EnableBits = (bool)bitsIgnoreCheckBox.IsChecked;
            Properties.Settings.Default.EnableSubs = (bool)subsIgnoreCheckBox.IsChecked;
            Properties.Settings.Default.EnableUsers = (bool)usersIgnoreCheckBox.IsChecked;
            Properties.Settings.Default.BitsThreshold = (int)bitsThreshold.Value;
            Properties.Settings.Default.Voice = voiceComboBox.SelectedIndex;
            //Properties.Settings.Default.RetroChatColorID = (bool)retroChatColorComboBox.SelectedIndex;
            //Properties.Settings.Default.RetroChatFontSize = (float)retroChatFontSize.Value;
            Properties.Settings.Default.VPTempo = (int)vpTempo.Value;
            Properties.Settings.Default.AutoReboot = (bool)autoConnectCheckBox.IsChecked;
            Properties.Settings.Default.OpenChatOnStart = (bool)openChatOnConnectCheckBox.IsChecked;
            Properties.Settings.Default.EnableCleverbot = (bool)cleverBotCheckBox.IsChecked;
            Properties.Settings.Default.Save();
        }

        private void LoadSettings()
        {
            usernameTextBox.Text = Properties.Settings.Default.Username;
            OAuthTextBox.Password = Properties.Settings.Default.OAuth_Token;
            deviceComboBox.SelectedIndex = Properties.Settings.Default.DeviceID;
            chatModeComboBox.SelectedIndex = Properties.Settings.Default.ChatModeID;
            dialToneCheckBox.IsChecked = Properties.Settings.Default.EnableDialTones;
            moderatorsIgnoreCheckBox.IsChecked = Properties.Settings.Default.EnableModerators;
            bitsIgnoreCheckBox.IsChecked = Properties.Settings.Default.EnableBits;
            subsIgnoreCheckBox.IsChecked = Properties.Settings.Default.EnableSubs;
            usersIgnoreCheckBox.IsChecked = Properties.Settings.Default.EnableUsers;
            bitsThreshold.Value = Properties.Settings.Default.BitsThreshold;
            voiceComboBox.SelectedIndex = Properties.Settings.Default.Voice;
            //retroChatColorComboBox.SelectedIndex = Properties.Settings.Default.RetroChatColorID;
            //retroChatFontSize.Value = (decimal)Properties.Settings.Default.RetroChatFontSize;
            vpTempo.Value = Properties.Settings.Default.VPTempo;
            autoConnectCheckBox.IsChecked = Properties.Settings.Default.AutoReboot;
            openChatOnConnectCheckBox.IsChecked = Properties.Settings.Default.OpenChatOnStart;
            cleverBotCheckBox.IsChecked = Properties.Settings.Default.EnableCleverbot;
        }

        private void TTS(OnMessageReceivedArgs e)
        {
            TTS(e.ChatMessage.Message);
        }

        private void TTS(string currentMessage)
        {
            TTS(currentMessage, GetVoiceID(), false);
        }

        private void TTS(string currentMessage, int voiceID, bool cleverBot)
        {
            if (lastChatMessage != currentMessage)
            {
                lastChatMessage = currentMessage;
                Regex rgx;
                List<byte> ttsArray = new List<byte>();

                foreach (string s in regexPattern)
                {
                    rgx = new Regex(s);
                    currentMessage = rgx.Replace(currentMessage, "");
                }

                for (int x = 0; x <= filter.GetUpperBound(0); x++)
                {
                    currentMessage = currentMessage.Replace(filter[x, 0], filter[x, 1]);
                }

                if (!GetDialTonesEnabled())
                {
                    rgx = new Regex(@"\[:dial([^\]]+)\]");
                    currentMessage = rgx.Replace(currentMessage, "");
                }

                rgx = new Regex(@"(\[(?:[a-zA-Z]+)<[\d,]+>\])");
                string[] splitMessages = rgx.Split(currentMessage);
                currentMessage = "";
                foreach (string message in splitMessages)
                {
                    rgx = new Regex(@"\[(?:[a-zA-Z]+)<([\d,]+)>\]");
                    var match = rgx.Match(message);
                    if (match.Success)
                    {
                        int value = 0;
                        Int32.TryParse(match.Groups[1].ToString().Split(',')[0], out value);
                        if (value <= 2000 && value != 0)
                        {
                            currentMessage += message;
                        }
                    }
                    else
                    {
                        currentMessage += message;
                    }
                }

                rgx = new Regex(@"(\[vp\])");
                if (rgx.Match(currentMessage).Success)
                {
                    string songString = currentMessage.Substring(4);
                    rgx = new Regex(@"(\[[ \da-zA-Z]+\])");
                    splitMessages = rgx.Split(songString);
                    foreach (string message in splitMessages)
                    {
                        rgx = new Regex(@"\[([ \da-zA-Z]+)\]");
                        var match = rgx.Match(message);
                        int frequency = 0;
                        double multiplier = 1;
                        double milis = GetVPTempo();

                        if (match.Success)
                        {
                            List<List<short>> superSine = new List<List<short>>();
                            try
                            {
                                foreach (char c in match.Groups[1].ToString())
                                {
                                    List<string> tone = tones.FirstOrDefault(stringToCheck => stringToCheck.Contains(c.ToString()));

                                    if (tone[0] == "{" || tone[0] == "}")
                                    {
                                        Double.TryParse(tone[1], out multiplier);
                                    }
                                    else
                                    {
                                        Int32.TryParse(tone[1], out frequency);
                                        superSine.Add(GenerateSineRaw(frequency, milis * multiplier));
                                    }
                                }
                                ttsArray.AddRange(CombineSineWaves(superSine));
                            }
                            catch (Exception)
                            {

                            }
                        }
                        else
                        {
                            foreach (char c in message)
                            {
                                List<string> tone = tones.FirstOrDefault(stringToCheck => stringToCheck.Contains(c.ToString()));
                                Console.WriteLine(tone);
                                try
                                {
                                    if (tone[0] == "{" || tone[0] == "}")
                                    {
                                        Double.TryParse(tone[1], out multiplier);
                                    }
                                    else
                                    {
                                        Int32.TryParse(tone[1], out frequency);
                                        ttsArray.AddRange(GenerateSine(frequency, milis * multiplier));
                                    }
                                }
                                catch (Exception)
                                {

                                }
                            }
                        }
                    }
                }
                else
                {
                    rgx = new Regex(@"([^ \dA-Za-z!""#$%&()*0,\-./:;<=>?@[\\\]^_`'{|}~\n\r])+");
                    var match = rgx.Match(currentMessage);
                    if (!match.Success)
                    {
                        rgx = new Regex(@"(\[:(?:tone|t)[ \d,]+\])");
                        splitMessages = rgx.Split(currentMessage);
                        foreach (string message in splitMessages)
                        {
                            rgx = new Regex(@"\[:(?:tone|t)([ \d,]+)\]");
                            match = rgx.Match(message);
                            if (match.Success)
                            {
                                int frequency = 0;
                                int milis = 0;
                                string[] values = match.Groups[1].ToString().Split(',');
                                if (values.Length > 1)
                                {
                                    Int32.TryParse(values[0], out frequency);
                                    Int32.TryParse(values[1], out milis);
                                }

                                if (frequency != 0 && milis != 0)
                                {
                                    ttsArray.AddRange(GenerateSine(frequency, milis));
                                }
                            }
                            else
                            {
                                tts.Voice = (TtsVoice)voiceID;
                                ttsArray.AddRange(tts.SpeakToMemory(message));
                            }
                        }
                    }
                }

                using (MemoryStream wavStream = new MemoryStream(Generate(ttsArray.ToArray())))
                {
                    try
                    {
                        using (var waveOut = new WaveOut { Device = new WaveOutDevice(GetDeviceID()) })
                        {
                            using (var waveSource = new RawDataReader(wavStream, new WaveFormat(11025, 16, 1)))
                            {
                                waveOut.Initialize(waveSource);
                                waveOut.Play();
                                ttsWavs.Add(waveOut);
                                waveOut.WaitForStopped();
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            if (GetCleverbotEnabled() && !cleverBot)
            {
                if (currentMessage.Length >= 1)
                {
                    SendToCleverbotAsync(currentMessage);
                }
            }

            if (GetChatModeID() == 1 && queuedMessage.Length > 0)
            {
                string input = queuedMessage;
                new Thread(delegate () { TTS(input); }).Start();
                queuedMessage = "";
            }
            else
            {
                playNext = true;
            }
        }

        public static byte[] Generate(byte[] input)
        {
            List<byte> wavStream = new List<byte>();

            const int headerSize = 44;
            const int formatChunkSize = 16;
            const short waveAudioFormat = 1;
            const short numChannels = 1;
            const int sampleRate = 11025;
            const short bitsPerSample = 16;
            const int byteRate = (numChannels * bitsPerSample * sampleRate) / 8;
            const short blockAlign = numChannels * bitsPerSample / 8;
            var sizeInBytes = input.Length;

            wavStream.AddRange(Encoding.ASCII.GetBytes("RIFF"));
            wavStream.AddRange(BitConverter.GetBytes(sizeInBytes + headerSize - 8));
            wavStream.AddRange(Encoding.ASCII.GetBytes("WAVE"));
            wavStream.AddRange(Encoding.ASCII.GetBytes("fmt "));
            wavStream.AddRange(BitConverter.GetBytes(formatChunkSize));
            wavStream.AddRange(BitConverter.GetBytes(waveAudioFormat));
            wavStream.AddRange(BitConverter.GetBytes(numChannels));
            wavStream.AddRange(BitConverter.GetBytes(sampleRate));
            wavStream.AddRange(BitConverter.GetBytes(byteRate));
            wavStream.AddRange(BitConverter.GetBytes(blockAlign));
            wavStream.AddRange(BitConverter.GetBytes(bitsPerSample));
            wavStream.AddRange(Encoding.ASCII.GetBytes("data"));
            wavStream.AddRange(BitConverter.GetBytes(sizeInBytes));
            wavStream.AddRange(input);
            return wavStream.ToArray();
        }

        public static byte[] GenerateSine(int frequency, double msDuration, UInt16 volume = 16383)
        {
            List<byte> wavStream = new List<byte>();

            const short numChannels = 1;
            const int sampleRate = 11025;
            int samples = sampleRate * (int)msDuration / 1000;

            double theta = (Math.PI * 2 * frequency) / (sampleRate * numChannels);
            double amp = volume >> 2;
            for (int i = 0; i < samples; i++)
            {
                short s = Convert.ToInt16(amp * Math.Sin(theta * i));
                wavStream.AddRange(BitConverter.GetBytes(s));
            }

            return wavStream.ToArray();
        }

        public static List<short> GenerateSineRaw(int frequency, double msDuration, UInt16 volume = 16383)
        {
            List<short> wavStream = new List<short>();

            const short numChannels = 1;
            const int sampleRate = 11025;
            int samples = sampleRate * (int)msDuration / 1000;

            double theta = (Math.PI * 2 * frequency) / (sampleRate * numChannels);
            double amp = volume >> 2;
            for (int i = 0; i < samples; i++)
            {
                short s = Convert.ToInt16(amp * Math.Sin(theta * i));
                wavStream.Add(s);
            }

            return wavStream;
        }

        public static byte[] CombineSineWaves(List<List<short>> sineList)
        {
            List<byte> wavStream = new List<byte>();

            int longestList = Int32.MinValue;
            foreach (List<short> sine in sineList)
            {
                if (sine.Count > longestList)
                {
                    longestList = sine.Count;
                }
            }

            List<short> sineSuperposition = new List<short>(new short[longestList]);
            //sineSuperposition.Capacity = longestList;
            for (int i = 0; i <= longestList; i++)
            {
                foreach (List<short> sine in sineList)
                {
                    if (sine.Count > i)
                    {
                        sineSuperposition[i] += sine[i];
                    }
                }
            }

            return sineSuperposition.SelectMany(BitConverter.GetBytes).ToArray();
        }

        private void ShowRetroChat_Click(object sender, RoutedEventArgs e)
        {
            if (childForm == null || WindowHelper.IsDisposed(childForm))
            {
                childForm = new RetroChat();
                //childForm.Owner = GetWindow(this);
                childForm.Show();
            }
        }

        private async void SendToCleverbotAsync(string message)
        {
            if (cleverbotSession != null)
            {
                var response = await cleverbotSession.Cleverbot.TalkAsync(message).ConfigureAwait(false);
                AddChat("John Madden" + ": " + response.CleverOutput);
                
                new Thread(delegate () { TTS(response.CleverOutput, 2, true); }).Start();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if ((bool)openChatOnConnectCheckBox.IsChecked)
            {
                ShowRetroChat_Click(null, null);
            }
        }
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, string color)
        {
            BrushConverter bc = new BrushConverter();
            TextRange tr = new TextRange(box.Document.ContentEnd, box.Document.ContentEnd);
            tr.Text = text;
            try
            {
                tr.ApplyPropertyValue(TextElement.ForegroundProperty,
                    bc.ConvertFromString(color));
            }
            catch (FormatException) { }
        }
    }

    public static class WindowHelper
    {
        public static Boolean IsDisposed(Window window)
        {
            return new WindowInteropHelper(window).Handle == IntPtr.Zero;
        }
    }
}
