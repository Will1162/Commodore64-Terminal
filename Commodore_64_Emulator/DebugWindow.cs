using System;
using System.Windows.Forms;

namespace Commodore_64_Emulator
{
    public partial class DebugWindow : Form
    {
        // this class shows basic information such as what resources the program is using

        Timer updateTimer = new Timer();
        float cpuUsage;
        int ramUsage;
        double frameTime;
        int[] programRamUsage;

        public DebugWindow()
        {
            InitializeComponent();
        }

        private void DebugWindow_Load(object sender, EventArgs e)
        {
            // when debug window is opened

            // when form loads, create a timer to call 3 times a second
            updateTimer.Interval = 333;
            updateTimer.Tick += new EventHandler(UpdateTimer_Event);
            updateTimer.Enabled = true;
        }

        private void UpdateTimer_Event(Object sender, EventArgs e)
        {
            // sets form values based on other program variables

            // update the application performance statisics
            cpuUsage = MainWindow.cpuUsage.NextValue();
            ramUsage = (int)MainWindow.ramUsage.NextValue();
            frameTime = MainWindow.lastFrameTime;
            programRamUsage = GLOBAL.ProgramRAM.GetUsage();

            // write the values to the debug info window
            frameRateValue.Text = (1000 / frameTime).ToString("0") + " FPS"; // frameRate = 1000 / frameTime
            frameTimeValue.Text = frameTime.ToString("0.0") + " ms";
            cpuUsageValue.Text = (cpuUsage / Environment.ProcessorCount).ToString("0.0") + " %";
            ramUsageValue.Text = (ramUsage / Math.Pow(1024, 2)).ToString("0") + " MB";
            programRAMTotalValue.Text = programRamUsage[0].ToString() + " Bytes";
            programRAMFreeValue.Text = programRamUsage[1].ToString() + " Bytes";
            programRAMUsedValue.Text = programRamUsage[2].ToString() + " Bytes";
        }

        private void DebugWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            // when DebugWindow form is closing

            // allow another window to be opened again
            MainWindow.debugWindowOpen = false;
        }
    }
}
