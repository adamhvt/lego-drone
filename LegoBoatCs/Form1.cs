using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lego;
using Ice;

namespace LegoBoatCs
{
    public partial class Form1 : Form
    {
        Communicator communicator;
        ServerPrx server;

        public Form1()
        {
            InitializeComponent();

            try
            {
                this.communicator = Util.initialize();
                this.server = ServerPrxHelper.checkedCast(this.communicator.stringToProxy("server:tcp -h lego.amk.uni-obuda.hu -p 8082"));
            }
            catch (System.Exception ex)
            {
                System.Console.Error.WriteLine(ex);
            }

            this.mainTimer.Start();
        }

        private void addTagetPosButton_Click(object sender, EventArgs e)
        {
            float latitude = Convert.ToSingle(this.latitudeTextBox.Text);
            float longitude = Convert.ToSingle(this.longitudeTextBox.Text);

            server.addTargetPosition(latitude, longitude);
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            this.server.moveByButton("forward");
        }

        private void leftButton_Click(object sender, EventArgs e)
        {
            this.server.moveByButton("left");
        }

        private void rightButton_Click(object sender, EventArgs e)
        {
            this.server.moveByButton("right");
        }

        private void backwardButton_Click(object sender, EventArgs e)
        {
            this.server.moveByButton("backward");
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.server.start();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            this.server.stop();
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            this.server.reset();
        }

        private void mainTimer_Tick(object sender, EventArgs e)
        {
            this.logLabel.Text = server.getActualData();
        }

    }
}
