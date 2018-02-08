namespace Twitch_TTS
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chatCommandBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sendCommandButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.diconnectButton = new System.Windows.Forms.Button();
            this.deviceComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.OAuthTextBox = new System.Windows.Forms.TextBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.dialToneCheckBox = new System.Windows.Forms.CheckBox();
            this.chatModeComboBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.moderatorsIgnoreCheckBox = new System.Windows.Forms.CheckBox();
            this.bitsIgnoreCheckBox = new System.Windows.Forms.CheckBox();
            this.subsIgnoreCheckBox = new System.Windows.Forms.CheckBox();
            this.usersIgnoreCheckBox = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.bitsThreshold = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.chatTextBox = new System.Windows.Forms.RichTextBox();
            this.voiceComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.showretroChatButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.retroChatColorComboBox = new System.Windows.Forms.ComboBox();
            this.retroChatFontSize = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bitsThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.retroChatFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // chatCommandBox
            // 
            this.chatCommandBox.Location = new System.Drawing.Point(46, 387);
            this.chatCommandBox.Name = "chatCommandBox";
            this.chatCommandBox.Size = new System.Drawing.Size(432, 20);
            this.chatCommandBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 390);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Say:";
            // 
            // sendCommandButton
            // 
            this.sendCommandButton.Location = new System.Drawing.Point(484, 385);
            this.sendCommandButton.Name = "sendCommandButton";
            this.sendCommandButton.Size = new System.Drawing.Size(75, 23);
            this.sendCommandButton.TabIndex = 3;
            this.sendCommandButton.Text = "Send";
            this.sendCommandButton.UseVisualStyleBackColor = true;
            this.sendCommandButton.Click += new System.EventHandler(this.SendCommandButton_Click);
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(13, 12);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(97, 23);
            this.connectButton.TabIndex = 4;
            this.connectButton.Text = "Connect Bot";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // diconnectButton
            // 
            this.diconnectButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.diconnectButton.Location = new System.Drawing.Point(476, 12);
            this.diconnectButton.Name = "diconnectButton";
            this.diconnectButton.Size = new System.Drawing.Size(97, 23);
            this.diconnectButton.TabIndex = 5;
            this.diconnectButton.Text = "Disconnect Bot";
            this.diconnectButton.UseVisualStyleBackColor = true;
            this.diconnectButton.Click += new System.EventHandler(this.DiconnectButton_Click);
            // 
            // deviceComboBox
            // 
            this.deviceComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.deviceComboBox.FormattingEnabled = true;
            this.deviceComboBox.Location = new System.Drawing.Point(326, 102);
            this.deviceComboBox.Name = "deviceComboBox";
            this.deviceComboBox.Size = new System.Drawing.Size(240, 21);
            this.deviceComboBox.TabIndex = 6;
            this.deviceComboBox.SelectedIndexChanged += new System.EventHandler(this.OnSettingsChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Output Device";
            // 
            // OAuthTextBox
            // 
            this.OAuthTextBox.Location = new System.Drawing.Point(91, 66);
            this.OAuthTextBox.Name = "OAuthTextBox";
            this.OAuthTextBox.PasswordChar = '*';
            this.OAuthTextBox.Size = new System.Drawing.Size(468, 20);
            this.OAuthTextBox.TabIndex = 8;
            this.OAuthTextBox.TextChanged += new System.EventHandler(this.OnSettingsChanged);
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(91, 40);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(468, 20);
            this.usernameTextBox.TabIndex = 9;
            this.usernameTextBox.TextChanged += new System.EventHandler(this.OnSettingsChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Username:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "OAuth Token:";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(12, 105);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(109, 13);
            this.statusLabel.TabIndex = 12;
            this.statusLabel.Text = "Status: Disconnected";
            // 
            // dialToneCheckBox
            // 
            this.dialToneCheckBox.AutoSize = true;
            this.dialToneCheckBox.Location = new System.Drawing.Point(133, 26);
            this.dialToneCheckBox.Name = "dialToneCheckBox";
            this.dialToneCheckBox.Size = new System.Drawing.Size(77, 17);
            this.dialToneCheckBox.TabIndex = 13;
            this.dialToneCheckBox.Text = "Dial Tones";
            this.dialToneCheckBox.UseVisualStyleBackColor = true;
            this.dialToneCheckBox.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
            // 
            // chatModeComboBox
            // 
            this.chatModeComboBox.FormattingEnabled = true;
            this.chatModeComboBox.Items.AddRange(new object[] {
            "Skip",
            "Queue",
            "Anarchy"});
            this.chatModeComboBox.Location = new System.Drawing.Point(408, 419);
            this.chatModeComboBox.Name = "chatModeComboBox";
            this.chatModeComboBox.Size = new System.Drawing.Size(151, 21);
            this.chatModeComboBox.TabIndex = 14;
            this.chatModeComboBox.SelectedIndexChanged += new System.EventHandler(this.OnSettingsChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(365, 422);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Mode:";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.moderatorsIgnoreCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.bitsIgnoreCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.subsIgnoreCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.usersIgnoreCheckBox);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Controls.Add(this.dialToneCheckBox);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(46, 422);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(313, 58);
            this.flowLayoutPanel1.TabIndex = 17;
            // 
            // moderatorsIgnoreCheckBox
            // 
            this.moderatorsIgnoreCheckBox.AutoSize = true;
            this.moderatorsIgnoreCheckBox.Checked = true;
            this.moderatorsIgnoreCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.moderatorsIgnoreCheckBox.Location = new System.Drawing.Point(3, 3);
            this.moderatorsIgnoreCheckBox.Name = "moderatorsIgnoreCheckBox";
            this.moderatorsIgnoreCheckBox.Size = new System.Drawing.Size(79, 17);
            this.moderatorsIgnoreCheckBox.TabIndex = 14;
            this.moderatorsIgnoreCheckBox.Text = "Moderators";
            this.moderatorsIgnoreCheckBox.UseVisualStyleBackColor = true;
            this.moderatorsIgnoreCheckBox.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
            // 
            // bitsIgnoreCheckBox
            // 
            this.bitsIgnoreCheckBox.AutoSize = true;
            this.bitsIgnoreCheckBox.Checked = true;
            this.bitsIgnoreCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bitsIgnoreCheckBox.Location = new System.Drawing.Point(88, 3);
            this.bitsIgnoreCheckBox.Name = "bitsIgnoreCheckBox";
            this.bitsIgnoreCheckBox.Size = new System.Drawing.Size(43, 17);
            this.bitsIgnoreCheckBox.TabIndex = 0;
            this.bitsIgnoreCheckBox.Text = "Bits";
            this.bitsIgnoreCheckBox.UseVisualStyleBackColor = true;
            this.bitsIgnoreCheckBox.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
            // 
            // subsIgnoreCheckBox
            // 
            this.subsIgnoreCheckBox.AutoSize = true;
            this.subsIgnoreCheckBox.Checked = true;
            this.subsIgnoreCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.subsIgnoreCheckBox.Location = new System.Drawing.Point(137, 3);
            this.subsIgnoreCheckBox.Name = "subsIgnoreCheckBox";
            this.subsIgnoreCheckBox.Size = new System.Drawing.Size(81, 17);
            this.subsIgnoreCheckBox.TabIndex = 1;
            this.subsIgnoreCheckBox.Text = "Subscribers";
            this.subsIgnoreCheckBox.UseVisualStyleBackColor = true;
            this.subsIgnoreCheckBox.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
            // 
            // usersIgnoreCheckBox
            // 
            this.usersIgnoreCheckBox.AutoSize = true;
            this.usersIgnoreCheckBox.Checked = true;
            this.usersIgnoreCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.usersIgnoreCheckBox.Location = new System.Drawing.Point(224, 3);
            this.usersIgnoreCheckBox.Name = "usersIgnoreCheckBox";
            this.usersIgnoreCheckBox.Size = new System.Drawing.Size(53, 17);
            this.usersIgnoreCheckBox.TabIndex = 2;
            this.usersIgnoreCheckBox.Text = "Users";
            this.usersIgnoreCheckBox.UseVisualStyleBackColor = true;
            this.usersIgnoreCheckBox.CheckedChanged += new System.EventHandler(this.OnSettingsChanged);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label7);
            this.flowLayoutPanel2.Controls.Add(this.bitsThreshold);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 23);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(130, 25);
            this.flowLayoutPanel2.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Bits Threshold";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bitsThreshold
            // 
            this.bitsThreshold.Location = new System.Drawing.Point(83, 3);
            this.bitsThreshold.Name = "bitsThreshold";
            this.bitsThreshold.Size = new System.Drawing.Size(43, 20);
            this.bitsThreshold.TabIndex = 15;
            this.bitsThreshold.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.bitsThreshold.ValueChanged += new System.EventHandler(this.OnSettingsChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 422);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 18;
            this.label6.Text = "Filters:";
            // 
            // chatTextBox
            // 
            this.chatTextBox.HideSelection = false;
            this.chatTextBox.Location = new System.Drawing.Point(13, 160);
            this.chatTextBox.Name = "chatTextBox";
            this.chatTextBox.ReadOnly = true;
            this.chatTextBox.Size = new System.Drawing.Size(546, 221);
            this.chatTextBox.TabIndex = 0;
            this.chatTextBox.Text = "";
            // 
            // voiceComboBox
            // 
            this.voiceComboBox.FormattingEnabled = true;
            this.voiceComboBox.Items.AddRange(new object[] {
            "Paul",
            "Betty",
            "Harry",
            "Frank",
            "Dennis",
            "Kit",
            "Ursula",
            "Rita",
            "Wendy"});
            this.voiceComboBox.Location = new System.Drawing.Point(408, 129);
            this.voiceComboBox.Name = "voiceComboBox";
            this.voiceComboBox.Size = new System.Drawing.Size(158, 21);
            this.voiceComboBox.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(367, 132);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 20;
            this.label8.Text = "Voice:";
            // 
            // showretroChatButton
            // 
            this.showretroChatButton.Location = new System.Drawing.Point(408, 446);
            this.showretroChatButton.Name = "showretroChatButton";
            this.showretroChatButton.Size = new System.Drawing.Size(151, 23);
            this.showretroChatButton.TabIndex = 21;
            this.showretroChatButton.Text = "Show Retro Chat";
            this.showretroChatButton.UseVisualStyleBackColor = true;
            this.showretroChatButton.Click += new System.EventHandler(this.ShowRetroChatWindow);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ImageLocation = "https://ggs.sx/downloads/jms.png";
            this.pictureBox1.Location = new System.Drawing.Point(11, 530);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(560, 100);
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.OnDonationPageClick);
            // 
            // retroChatColorComboBox
            // 
            this.retroChatColorComboBox.FormattingEnabled = true;
            this.retroChatColorComboBox.Items.AddRange(new object[] {
            "Amber",
            "Green"});
            this.retroChatColorComboBox.Location = new System.Drawing.Point(497, 475);
            this.retroChatColorComboBox.Name = "retroChatColorComboBox";
            this.retroChatColorComboBox.Size = new System.Drawing.Size(62, 21);
            this.retroChatColorComboBox.TabIndex = 22;
            this.retroChatColorComboBox.Text = "Amber";
            this.retroChatColorComboBox.SelectedIndexChanged += new System.EventHandler(this.OnRetroChatColorChanged);
            // 
            // retroChatFontSize
            // 
            this.retroChatFontSize.DecimalPlaces = 2;
            this.retroChatFontSize.Location = new System.Drawing.Point(497, 502);
            this.retroChatFontSize.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.retroChatFontSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.retroChatFontSize.Name = "retroChatFontSize";
            this.retroChatFontSize.Size = new System.Drawing.Size(62, 20);
            this.retroChatFontSize.TabIndex = 23;
            this.retroChatFontSize.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.retroChatFontSize.ValueChanged += new System.EventHandler(this.OnRetroChatSizeChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(388, 478);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Retro chat text color";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(374, 504);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(117, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Retro chat font size (pt)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 642);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.retroChatFontSize);
            this.Controls.Add(this.retroChatColorComboBox);
            this.Controls.Add(this.showretroChatButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.voiceComboBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chatModeComboBox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.usernameTextBox);
            this.Controls.Add(this.OAuthTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.deviceComboBox);
            this.Controls.Add(this.diconnectButton);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.sendCommandButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chatCommandBox);
            this.Controls.Add(this.chatTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "JMS - Twitch Text to Speech Bot";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bitsThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.retroChatFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox chatCommandBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sendCommandButton;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Button diconnectButton;
        private System.Windows.Forms.ComboBox deviceComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox OAuthTextBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.CheckBox dialToneCheckBox;
        private System.Windows.Forms.ComboBox chatModeComboBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox bitsIgnoreCheckBox;
        private System.Windows.Forms.CheckBox subsIgnoreCheckBox;
        private System.Windows.Forms.CheckBox usersIgnoreCheckBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox moderatorsIgnoreCheckBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown bitsThreshold;
        private System.Windows.Forms.RichTextBox chatTextBox;
        private System.Windows.Forms.ComboBox voiceComboBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button showretroChatButton;
        private System.Windows.Forms.ComboBox retroChatColorComboBox;
        private System.Windows.Forms.NumericUpDown retroChatFontSize;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
    }
}

