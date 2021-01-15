using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace joycontrol_client
{
    public partial class GUI : Form
    {

        string ip = "";
        int port = 7777;

        private bool running = false;
        private bool[] buttons = new bool[26];

        public GUI()
        {
            InitializeComponent();
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            BackgroundWorker bw = new BackgroundWorker();
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerAsync();
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            IPEndPoint ep = new IPEndPoint(ip.Length < 7 ? IPAddress.Broadcast : IPAddress.Parse(ip), port);
            while (running)
            {
                Thread.Sleep(20);
                s.SendTo(generatePacket(buttons), ep);
            }
        }

        #region KeyEvents
        //Temporary input
        //will be changed later

        private void GUI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)//0
            {
                buttons[0] = true;
            }
            if (e.KeyCode == Keys.A)//1
            {
                buttons[1] = true;
            }
            if (e.KeyCode == Keys.S)//2
            {
                buttons[2] = true;
            }
            if (e.KeyCode == Keys.D)//3
            {
                buttons[3] = true;
            }
            if (e.KeyCode == Keys.Q)//4 - UP
            {
                buttons[4] = true;
            }
            if (e.KeyCode == Keys.E)//5 - RIGHT
            {
                buttons[5] = true;
            }
            if (e.KeyCode == Keys.X)//6 - DOWN
            {
                buttons[6] = true;
            }
            if (e.KeyCode == Keys.Y)//7 - LEFT
            {
                buttons[7] = true;
            }
            if (e.KeyCode == Keys.R)//8 - L
            {
                buttons[8] = true;
            }
            if (e.KeyCode == Keys.F)//9 - ZL
            {
                buttons[9] = true;
            }
            if (e.KeyCode == Keys.C)//10 - L_STICK
            {
                buttons[10] = true;
            }
            if (e.KeyCode == Keys.D2)//11 - MINUS
            {
                buttons[11] = true;
            }
            if (e.KeyCode == Keys.D1)//12 - CAPTURE
            {
                buttons[12] = true;
            }


            if (e.KeyCode == Keys.U)//13
            {
                buttons[13] = true;
            }
            if (e.KeyCode == Keys.H)//14
            {
                buttons[14] = true;
            }
            if (e.KeyCode == Keys.J)//15
            {
                buttons[15] = true;
            }
            if (e.KeyCode == Keys.K)//16
            {
                buttons[16] = true;
            }
            if (e.KeyCode == Keys.I)//17 - X
            {
                buttons[17] = true;
            }
            if (e.KeyCode == Keys.Z)//18 - Y
            {
                buttons[18] = true;
            }
            if (e.KeyCode == Keys.N)//19 - B
            {
                buttons[19] = true;
            }
            if (e.KeyCode == Keys.M)//20 - A
            {
                buttons[20] = true;
            }
            if (e.KeyCode == Keys.O)//21 - R
            {
                buttons[21] = true;
            }
            if (e.KeyCode == Keys.L)//22 - ZR
            {
                buttons[22] = true;
            }
            if (e.KeyCode == Keys.OemPeriod)//23 - R_STICK
            {
                buttons[23] = true;
            }
            if (e.KeyCode == Keys.D7)//24 - PLUS
            {
                buttons[24] = true;
            }
            if (e.KeyCode == Keys.D6)//25 - HOME
            {
                buttons[25] = true;
            }
        }

        private void GUI_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)//0
            {
                buttons[0] = false;
            }
            if (e.KeyCode == Keys.A)//1
            {
                buttons[1] = false;
            }
            if (e.KeyCode == Keys.S)//2
            {
                buttons[2] = false;
            }
            if (e.KeyCode == Keys.D)//3
            {
                buttons[3] = false;
            }
            if (e.KeyCode == Keys.U)//13
            {
                buttons[13] = false;
            }
            if (e.KeyCode == Keys.H)//14
            {
                buttons[14] = false;
            }
            if (e.KeyCode == Keys.J)//15
            {
                buttons[15] = false;
            }
            if (e.KeyCode == Keys.K)//16
            {
                buttons[16] = false;
            }
        }

        #endregion

        public static byte[] generatePacket(bool[] states)
        {
            string tmp = "";
            if (!states[0] && !states[1] && !states[2] && !states[3])
            {
                tmp += " ls0";
            }
            if (states[0] && !states[1] && !states[2] && !states[3])
            {
                tmp += " ls1";
            }
            if (states[0] && !states[1] && !states[2] && states[3])
            {
                tmp += " ls2";
            }
            if (!states[0] && !states[1] && !states[2] && states[3])
            {
                tmp += " ls3";
            }
            if (!states[0] && !states[1] && states[2] && states[3])
            {
                tmp += " ls4";
            }
            if (!states[0] && !states[1] && states[2] && !states[3])
            {
                tmp += " ls5";
            }
            if (!states[0] && states[1] && states[2] && !states[3])
            {
                tmp += " ls6";
            }
            if (!states[0] && states[1] && !states[2] && !states[3])
            {
                tmp += " ls7";
            }
            if (states[0] && states[1] && !states[2] && !states[3])
            {
                tmp += " ls8";
            }
            if (!states[13] && !states[14] && !states[15] && !states[16])
            {
                tmp += " rs0";
            }
            if (states[13] && !states[14] && !states[15] && !states[16])
            {
                tmp += " rs1";
            }
            if (states[13] && !states[14] && !states[15] && states[16])
            {
                tmp += " rs2";
            }
            if (!states[13] && !states[14] && !states[15] && states[16])
            {
                tmp += " rs3";
            }
            if (!states[13] && !states[14] && states[15] && states[16])
            {
                tmp += " rs4";
            }
            if (!states[13] && !states[14] && states[15] && !states[16])
            {
                tmp += " rs5";
            }
            if (!states[13] && states[14] && states[15] && !states[16])
            {
                tmp += " rs6";
            }
            if (!states[13] && states[14] && !states[15] && !states[16])
            {
                tmp += " rs7";
            }
            if (states[13] && states[14] && !states[15] && !states[16])
            {
                tmp += " rs8";
            }
            if (states[4])
            {
                tmp += " du";
                states[4] = false;
            }
            if (states[5])
            {
                tmp += " dr";
                states[5] = false;
            }
            if (states[6])
            {
                tmp += " dd";
                states[6] = false;
            }
            if (states[7])
            {
                tmp += " dl";
                states[7] = false;
            }
            if (states[8])
            {
                tmp += " l1";
                states[8] = false;
            }
            if (states[9])
            {
                tmp += " l2";
                states[9] = false;
            }
            if (states[10])
            {
                tmp += " l3";
                states[10] = false;
            }
            if (states[11])
            {
                tmp += " m";
                states[11] = false;
            }
            if (states[12])
            {
                tmp += " fc";
                states[12] = false;
            }
            if (states[17])
            {
                tmp += " x";
                states[17] = false;
            }
            if (states[18])
            {
                tmp += " y";
                states[18] = false;
            }
            if (states[19])
            {
                tmp += " b";
                states[19] = false;
            }
            if (states[20])
            {
                tmp += " a";
                states[20] = false;
            }
            if (states[21])
            {
                tmp += " r1";
                states[21] = false;
            }
            if (states[22])
            {
                tmp += " r2";
                states[22] = false;
            }
            if (states[23])
            {
                tmp += " r3";
                states[23] = false;
            }
            if (states[24])
            {
                tmp += " p";
                states[24] = false;
            }
            if (states[25])
            {
                tmp += " fh";
                states[25] = false;
            }
            return Encoding.UTF8.GetBytes(tmp);
        }
    }
}
