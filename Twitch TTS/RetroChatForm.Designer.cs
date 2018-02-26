namespace Twitch_TTS
{
    partial class RetroChatForm
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
            this.components = new System.ComponentModel.Container();
            this.retroChatTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.retroChatTextBox = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.retroChatTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(1280, 720);
            this.panel1.TabIndex = 1;
            // 
            // retroChatTextBox
            // 
            this.retroChatTextBox.BackColor = System.Drawing.Color.Black;
            this.retroChatTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.retroChatTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.retroChatTextBox.Font = new System.Drawing.Font("Viner Hand ITC", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.retroChatTextBox.HideSelection = false;
            this.retroChatTextBox.Location = new System.Drawing.Point(10, 10);
            this.retroChatTextBox.Name = "retroChatTextBox";
            this.retroChatTextBox.ReadOnly = true;
            this.retroChatTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.retroChatTextBox.ShortcutsEnabled = false;
            this.retroChatTextBox.Size = new System.Drawing.Size(1260, 700);
            this.retroChatTextBox.TabIndex = 1;
            this.retroChatTextBox.Text = "";
            // 
            // RetroChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "RetroChatForm";
            this.Text = "RetroChat";
            this.TransparencyKey = System.Drawing.SystemColors.InactiveBorder;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer retroChatTimer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox retroChatTextBox;
    }
}