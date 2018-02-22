using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

using SharpTalk;
using CSCore;
using CSCore.SoundOut;

using TwitchLib;
using TwitchLib.Events.Client;
using TwitchLib.Enums;
using TwitchLib.Models.Client;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using CSCore.Streams;
using CSCore.Codecs.WAV;
using CSCore.Codecs.RAW;
using Cleverbot.Net;
using System.Linq;

namespace Twitch_TTS
{
    public partial class Form1 : Form
    {
        delegate void StringArgReturningVoidDelegate(string text);
        delegate void ChatArgReturningVoidDelegate(string text, string colorHex);
        public ConnectionCredentials credentials;
        public CleverbotSession cleverbotSession;
        public TwitchClient client;
        public RetroChatForm childForm;
        public FonixTalkEngine tts = new FonixTalkEngine();
        public List<WaveOut> ttsWavs = new List<WaveOut>();
        public string lastChatMessage = "";
        public string queuedMessage = "";
        public string[,] filter = {
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
            {"[:hl]", "[:dv ap 10]"},
            {"[:sd]", "[:dv ap 10][:dv pr 1]"},
            {"[:nv]", ""}
        };

        public List<List<string>> tones = new List<List<string>> {
            new List<string> {" ","0"},
            new List<string> {"|", "0"},
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
            new List<string> {"m","2093"}
        };
        public int moderators = 0;
        public static List<Tuple<string, Color>> buffer = new List<Tuple<string, Color>>();


        public string[] regexPattern = {
            @"\[:rate([^\]]+)\]",
            @"\[:comma([^\]]+)\]",
            @"\[:period([^\]]+)\]",
            @"((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)",
            @"\[:dial\]",
            @"\[:(?:tone|t)\]"
        };

        public bool playNext = true;

        public Form1()
        {
            InitializeComponent();
            GenerateDeviceList();
            LoadSettings();
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

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (client == null || !client.IsConnected && usernameTextBox.Text != "")
            {
                try
                {
                    credentials = new ConnectionCredentials(usernameTextBox.Text, OAuthTextBox.Text);
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

        private void DiconnectButton_Click(object sender, EventArgs e)
        {
            if (client != null && client.IsConnected)
            {
                SetStatus("Disconnecting...");
                client.LeaveChannel(Properties.Settings.Default.Username);

                client.Disconnect();
            }
        }

        private void SendCommandButton_Click(object sender, EventArgs e)
        {
            //client.SendMessage(chatCommandBox.Text);
            string text = chatCommandBox.Text;
            AddChat(chatCommandBox.Text);
            chatCommandBox.Text = "";
            new Thread(delegate () { TTS(text); }).Start();
        }

        private void ChatCommandBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string text = chatCommandBox.Text;
                AddChat(chatCommandBox.Text);
                chatCommandBox.Text = "";
                e.Handled = true;
                e.SuppressKeyPress = true;
                new Thread(delegate () { TTS(text); }).Start();
            }
        }

        private void AddChat(string text)
        {
            if (chatTextBox.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(AddChat);
                Invoke(d, new object[] { text });
            }
            else
            {
                chatTextBox.AppendText(text + "\r\n");
                foreach (char c in text.ToCharArray())
                {
                    buffer.Add(new Tuple<string, Color>(c.ToString(), Color.Empty));
                }

                buffer.Add(new Tuple<string, Color>("\r\n", Color.Empty));
            }
        }

        private void AddChat(string text, string colorHex)
        {
            if (chatTextBox.InvokeRequired)
            {
                ChatArgReturningVoidDelegate d = new ChatArgReturningVoidDelegate(AddChat);
                Invoke(d, new object[] { text, colorHex });
            }
            else
            {
                chatTextBox.AppendText(text, ColorTranslator.FromHtml(colorHex));
                foreach(char c in text.ToCharArray())
                {
                    buffer.Add(new Tuple<string, Color>(c.ToString(), ColorTranslator.FromHtml(colorHex)));
                }
            }
        }

        private void SetStatus(string text)
        {
            if (statusLabel.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(SetStatus);
                Invoke(d, new object[] { text });
            }
            else
            {
                statusLabel.Text = "Status: " + text;
            }
        }

        private int GetDeviceID()
        {
            if (deviceComboBox.InvokeRequired)
            {
                return (int)deviceComboBox.Invoke(
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
            if (chatModeComboBox.InvokeRequired)
            {
                return (int)chatModeComboBox.Invoke(
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
            if (voiceComboBox.InvokeRequired)
            {
                return (int)voiceComboBox.Invoke(
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
            if (voiceComboBox.InvokeRequired)
            {
                return (bool)cleverBotCheckBox.Invoke(
                    new Func<bool>(() => GetCleverbotEnabled())
                );
            }
            else
            {
                return cleverBotCheckBox.Checked;
            }
        }

        private int GetVPTempo()
        {
            if (vpTempo.InvokeRequired)
            {
                return (int)vpTempo.Invoke(
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
            }
        }

        private void OnChatMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Color != null)
            {
                AddChat(e.ChatMessage.DisplayName, e.ChatMessage.ColorHex);
                AddChat(": " + e.ChatMessage.Message);
            }
            else
            {
                AddChat(e.ChatMessage.DisplayName + ": " + e.ChatMessage.Message);
            }
            
            if (!e.ChatMessage.Message.StartsWith("!") && (
                e.ChatMessage.UserType == UserType.Broadcaster ||
                e.ChatMessage.UserType == UserType.Admin ||
                e.ChatMessage.UserType == UserType.GlobalModerator ||
                e.ChatMessage.IsModerator && moderatorsIgnoreCheckBox.Checked ||
                e.ChatMessage.Bits >= bitsThreshold.Value && bitsIgnoreCheckBox.Checked ||
                e.ChatMessage.IsSubscriber && subsIgnoreCheckBox.Checked ||
                e.ChatMessage.UserType == UserType.Viewer && usersIgnoreCheckBox.Checked)
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

        private void OnDonationPageClick(object sender, EventArgs e)
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
            Properties.Settings.Default.OAuth_Token = OAuthTextBox.Text;
            Properties.Settings.Default.DeviceID = deviceComboBox.SelectedIndex;
            Properties.Settings.Default.ChatModeID = chatModeComboBox.SelectedIndex;
            Properties.Settings.Default.EnableDialTones = dialToneCheckBox.Checked;
            Properties.Settings.Default.EnableModerators = moderatorsIgnoreCheckBox.Checked;
            Properties.Settings.Default.EnableBits = bitsIgnoreCheckBox.Checked;
            Properties.Settings.Default.EnableSubs = subsIgnoreCheckBox.Checked;
            Properties.Settings.Default.EnableUsers = usersIgnoreCheckBox.Checked;
            Properties.Settings.Default.BitsThreshold = (int)bitsThreshold.Value;
            Properties.Settings.Default.Voice = voiceComboBox.SelectedIndex;
            Properties.Settings.Default.RetroChatColorID = retroChatColorComboBox.SelectedIndex;
            Properties.Settings.Default.RetroChatFontSize = (float)retroChatFontSize.Value;
            Properties.Settings.Default.VPTempo = (int)vpTempo.Value;
            Properties.Settings.Default.Save();
        }

        private void LoadSettings()
        {
            usernameTextBox.Text = Properties.Settings.Default.Username;
            OAuthTextBox.Text = Properties.Settings.Default.OAuth_Token;
            deviceComboBox.SelectedIndex = Properties.Settings.Default.DeviceID;
            chatModeComboBox.SelectedIndex = Properties.Settings.Default.ChatModeID;
            dialToneCheckBox.Checked = Properties.Settings.Default.EnableDialTones;
            moderatorsIgnoreCheckBox.Checked = Properties.Settings.Default.EnableModerators;
            bitsIgnoreCheckBox.Checked = Properties.Settings.Default.EnableBits;
            subsIgnoreCheckBox.Checked = Properties.Settings.Default.EnableSubs;
            usersIgnoreCheckBox.Checked = Properties.Settings.Default.EnableUsers;
            bitsThreshold.Value = Properties.Settings.Default.BitsThreshold;
            voiceComboBox.SelectedIndex = Properties.Settings.Default.Voice;
            retroChatColorComboBox.SelectedIndex = Properties.Settings.Default.RetroChatColorID;
            retroChatFontSize.Value = (decimal)Properties.Settings.Default.RetroChatFontSize;
            vpTempo.Value = Properties.Settings.Default.VPTempo;
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

                if (!dialToneCheckBox.Checked)
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
                        Int32.TryParse(match.Groups[1].ToString(), out value);
                        if (value <= 1000 && value != 0)
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
                        int milis = GetVPTempo();

                        if (match.Success)
                        {
                            List<List<short>> superSine = new List<List<short>>();
                            try
                            {
                                foreach (char c in match.Groups[1].ToString())
                                {
                                    List<string> tone = tones.FirstOrDefault(stringToCheck => stringToCheck.Contains(c.ToString()));

                                    Int32.TryParse(tone[1], out frequency);
                                    if (c.ToString() == " ")
                                    {
                                        milis = milis;
                                    }
                                    superSine.Add(GenerateSineRaw(frequency, milis));
                                }
                                ttsArray.AddRange(CombineSineWaves(superSine));
                            }
                            catch (Exception)
                            {

                            }
                        }
                        else
                        {
                            foreach(char c in message)
                            {
                                List<string> tone = tones.FirstOrDefault(stringToCheck => stringToCheck.Contains(c.ToString()));
                                try
                                {
                                    Int32.TryParse(tone[1], out frequency);
                                    if (c.ToString() == " ")
                                    {
                                        milis = milis;
                                    }
                                    ttsArray.AddRange(GenerateSine(frequency, milis));
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
                    rgx = new Regex(@"(\[:(?:tone|t)[ \d,]+\])");
                    splitMessages = rgx.Split(currentMessage);
                    foreach (string message in splitMessages)
                    {
                        rgx = new Regex(@"\[:(?:tone|t)([ \d,]+)\]");
                        var match = rgx.Match(message);
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
                    catch (Exception e)
                    {

                    }
                }
            }

            if (GetCleverbotEnabled() && !cleverBot)
            {
                if (currentMessage.Length >= 1)
                {
                    SendToCleverbot(currentMessage);
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

        public static byte[] GenerateSine(int frequency, int msDuration, UInt16 volume = 16383)
        {
            List<byte> wavStream = new List<byte>();

            const short numChannels = 1;
            const int sampleRate = 11025;
            int samples = sampleRate * msDuration / 1000;
            
            double theta = (Math.PI * 2 * frequency) / (sampleRate * numChannels);
            double amp = volume >> 2;
            for (int i = 0; i < samples; i++)
            {
                short s = Convert.ToInt16(amp * Math.Sin(theta * i));
                wavStream.AddRange(BitConverter.GetBytes(s));
            }

            return wavStream.ToArray();
        }

        public static List<short> GenerateSineRaw(int frequency, int msDuration, UInt16 volume = 16383)
        {
            List<short> wavStream = new List<short>();

            const short numChannels = 1;
            const int sampleRate = 11025;
            int samples = sampleRate * msDuration / 1000;

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

        private void ShowRetroChatWindow(object sender, EventArgs e)
        {
            if (childForm == null || childForm.IsDisposed)
            {
                childForm = new RetroChatForm();
                switch (retroChatColorComboBox.SelectedIndex)
                {
                    case 0:
                        childForm.chatColor = ColorTranslator.FromHtml("#ff8100");
                        //childForm.chatBackground = Properties.Resources.Background_Amber;
                        break;
                    case 1:
                        childForm.chatColor = ColorTranslator.FromHtml("#0ccc68");
                        //childForm.chatBackground = Properties.Resources.Background_Green;
                        break;
                    default:
                        break;
                }
                childForm.chatSize = new Font(childForm.chatSize.FontFamily, (float)retroChatFontSize.Value, FontStyle.Bold);
                childForm.Show(this);
            }
        }

        private void OnRetroChatColorChanged(object sender, EventArgs e)
        {
            if (childForm != null)
            {
                switch (retroChatColorComboBox.SelectedIndex)
                {
                    case 0:
                        childForm.chatColor = ColorTranslator.FromHtml("#ff8100");
                        //childForm.chatBackground = Properties.Resources.Background_Amber;
                        break;
                    case 1:
                        childForm.chatColor = ColorTranslator.FromHtml("#0ccc68");
                        //childForm.chatBackground = Properties.Resources.Background_Green;
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnRetroChatSizeChanged(object sender, EventArgs e)
        {
            if (childForm != null)
            {
                childForm.chatSize = new Font(childForm.chatSize.FontFamily, (float)retroChatFontSize.Value, FontStyle.Bold);
            }
        }

        private void SendToCleverbot(string message)
        {
            if (cleverbotSession != null)
            {
                string response = cleverbotSession.Send(message);
                AddChat("John Madden", "#0000CD");
                AddChat(": " + response);
                new Thread(delegate () { TTS(response, 2, true); }).Start();
            }
        }
    }

    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }
    }
}
