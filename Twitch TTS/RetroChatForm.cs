using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Twitch_TTS
{
    public partial class RetroChatForm : Form
    {
        public PrivateFontCollection pfc = new PrivateFontCollection();
        public Color chatColor
        {
            get { return retroChatTextBox.ForeColor; }
            set { retroChatTextBox.ForeColor = value; }
        }

        public Font chatSize
        {
            get { return retroChatTextBox.Font; }
            set { retroChatTextBox.Font = value; }
        }

        public Image chatBackground
        {
            get { return BackgroundImage; }
            set { BackgroundImage = value; }
        }

        public RetroChatForm()
        {
            InitializeComponent();
            CustomFont();
            retroChatTextBox.Font = new Font(pfc.Families[0], 12, FontStyle.Bold);
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            retroChatTimer.Interval = 2;
            retroChatTimer.Tick += new EventHandler(retroChatTimer_Tick);
            retroChatTimer.Enabled = true;
        }

        private void retroChatTimer_Tick(object sender, EventArgs e)
        {
            if (Form1.buffer.Count > 0)
            {
                if (Form1.buffer[0].Item2.IsEmpty)
                {
                    retroChatTextBox.AppendText(Form1.buffer[0].Item1.ToString());
                }
                else
                {
                    retroChatTextBox.AppendText(Form1.buffer[0].Item1.ToString(), Form1.buffer[0].Item2);
                }
                Form1.buffer.RemoveAt(0);
            }
        }

        private void CustomFont()
        {
            //Select your font from the resources.
            //My font here is "Digireu.ttf"
            int fontLength = Properties.Resources.VT220_mod.Length;

            // create a buffer to read in to
            byte[] fontdata = Properties.Resources.VT220_mod;

            // create an unsafe memory block for the font data
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);

            // copy the bytes to the unsafe memory block
            Marshal.Copy(fontdata, 0, data, fontLength);

            // pass the font to the font collection
            pfc.AddMemoryFont(data, fontLength);
        }
    }
}
