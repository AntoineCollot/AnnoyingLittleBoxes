using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace AnnoyingLittleBox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TopMost = true;
            bool cancel = false;

            while (!cancel)
            {
                switch (MessageBox.Show("This software is not compatible with the current environment :(", "Friendly error",
        MessageBoxButtons.RetryCancel, MessageBoxIcon.Error))
                {
                    case DialogResult.Cancel:
                        cancel = true;
                        break;
                    case DialogResult.Retry:
                        break;
                    default:
                        cancel = true;
                        break;
                }
            }

            System.Threading.Thread.Sleep(GetStartTime());
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            e.Cancel = true;
            this.Visible = false;
            System.Threading.Thread.Sleep(GetRandomTime());
            SetRandomPosition();
            Text = GetRandomError();
            this.Visible = true;
        }

        protected override void WndProc(ref Message message)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_MOVE = 0xF010;

            switch (message.Msg)
            {
                case WM_SYSCOMMAND:
                    int command = message.WParam.ToInt32() & 0xfff0;
                    if (command == SC_MOVE)
                        return;
                    break;
            }

            base.WndProc(ref message);
        }

        void SetRandomPosition()
        {
            var wa = Screen.PrimaryScreen.WorkingArea;
            Random r = new Random();
            this.Location = new Point(r.Next(0,wa.Right- this.Width),r.Next(0,wa.Bottom - this.Height));
        }

        int GetRandomTime()
        {
            Random r = new Random();
            int randomId = r.Next(0, 10);
            if (randomId==0)
            {
                return new Random().Next(60000, 180000);
            }
            else
            {
                return new Random().Next(1000, 10000);
            }
        }

        int GetStartTime()
        {
            string fileName = "Settings.txt";
            if (File.Exists(fileName))
            {
                string[] lines = File.ReadAllLines(fileName);
                int time;
                if (lines.Length>0 && Int32.TryParse(lines[0], out time))
                    return time;
                else
                    return 30000;
            }
            else
                return 30000;
        }

        string GetRandomError()
        {
            string[] errors = {
                "ERROR_STACK_OVERFLOW_TERMINATED_EARLY_IN_THE_MORNING 0x3E994D",
                "ERROR_REGISTRY_RECOVERED_INEXTREMIS 0x5948CA9",
                "ERROR_DO_NOT_CROSS_THE_STREAMS 0x648E64F",
                "ERROR_NO_LOG_SPACE_AVAILABLE_FOR_IDIOTS 0xAE49A45F",
                "ERROR_CHILD_MUST_BE_VIOLATED 0x3EFF554D",
                "ERROR_WIZARD_MUST_BE_TERMINATED 0x3E994D",
                "ERROR_WIZARD_SHALL_NOT_PASS 0x496841C",
                "ERROR_SHUTDOWN_IN_PROGRESS_THANK_YOU_FOR_YOUR_PATIENCE 0x4962224D",
                "ERROR_FLOPPY_ID_MARK_NOT_FOUND 0x4541AAD",
                "ERROR_PORN_STACK_FULL 0x08104560E",
                "ERROR_PORNHUB_CANNOT_SHUTDOWN_WHILE_LIVESTREAMING 0x0642123AE6410",
                "ERROR_LIBERTINE_IS_NOT_A_CONDITION 0x6488AE442",
                "ERROR_EVERYTHING_IS_BROKEN 0x0081104EA",
                "ERROR_BIRD_DIED_MID_FLIGHT 0x4545AE888",
                "ERROR_ERRORS_RUNNING_OUT 0x7845RA225",
                "ERROR_BLUE_SCREEN_FAILED 0x008545RD1",
                "ERROR_CANNOT_SELF_TERMINATE 0x745A45400E",
                "ERROR_GIGOWATTS_ARE_TOO 0x05433A004C"
            };

            return errors[new Random().Next(0, errors.Length)];
        }
    }
}
