using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace Commodore_64_Emulator
{
    public partial class MainWindow : Form
    {
        // importing function to convert c# KeyCode data types to char
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int ToUnicode(uint virtualKeyCode, uint scanCode, byte[] keyboardState, StringBuilder receivingBuffer, int bufferSize, uint flags);

        // timer for flashing where the cursor is
        Timer cursorTick = new Timer();
        public static bool cursorVisible = false;

        // shared variables for debug window
        DebugWindow debugWindow;
        public static PerformanceCounter cpuUsage; // object used to monitor cpu usage
        public static PerformanceCounter ramUsage; // object used to monitor ram usage
        public static bool debugWindowOpen = false;
        public static double lastFrameTime;

        // shared variables for keyboard window
        KeyboardWindow keyboardWindow;
        public static bool keyboardWindowOpen = false;
        public static int pressedKey;

        // valid text characters, in order of apperance in the character set
        private static string charMap = "@ABCDEFGHIJKLMNOPQRSTUVWXYZ[£]^_ !\"#$%&'()*+,-./0123456789:;<=>?";

        public static string[,] variables = new string[256, 2];
        public static int variableCount = 0;

        public static bool doingFileStuff = false; // true when the "save file" window opens, have to pause drawing whilst saving, errors otherwise

        Bitmap zoomedDisplay; // the final image the GUI displays

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            SetupEnviroment();

            UpdateDisplay();
        }

        private void SetupEnviroment() // prepare the application before main execution
        {
            // reads user config file
            FileHandler.ReadAppConfig();

            // gets window size properties
            int[] dimensions = GraphicsHandler.GetDimensions();
            int width = dimensions[0];
            int height = dimensions[1];
            int borderSize = GLOBAL.AppConfig.border_size;
            int displayScale = GLOBAL.AppConfig.display_scale;

            // gets the user's display resolution
            int windowWidth = Screen.FromControl(this).Bounds.Width;
            int windowHeight = Screen.FromControl(this).Bounds.Height;

            // gets the wanted size for the window
            int newWidth = (width + (borderSize * 2)) * displayScale;
            int newHeight = (height + (borderSize * 2)) * displayScale;

            if (newWidth > windowWidth || newHeight > windowHeight - 50) // if resulting window would be larger than resolution, -50 height accounting for the taskbar
            {
                // reset the scale to the default value
                ErrorHandler.ShowSystemError(6);
                GLOBAL.AppConfig.display_scale = 3;

                // calculate a new size
                displayScale = GLOBAL.AppConfig.display_scale;
                newWidth = (width + (borderSize * 2)) * displayScale;
                newHeight = (height + (borderSize * 2)) * displayScale;
            }

            // changes GUI elements depending on the display scale value and border width
            displayPictureBox.Size = new Size(newWidth, newHeight); // the area showing the main program
            this.Size = new Size(newWidth + 15, newHeight + 88); // the window size. extra size accounts for window border and buttons
            zoomedDisplay = new Bitmap(newWidth, newHeight); // final image the user sees

            // changes button location based on display scale and border width
            int newY = newHeight; // right below main program area
            int newButtonWidth = newWidth / 4; // 4 buttons
            // location
            toggleKeyboardButton.Location = new Point(newButtonWidth * 0, newY);
            toggleDebugButton.Location = new Point(newButtonWidth * 1, newY);
            LoadProgramButton.Location = new Point(newButtonWidth * 2, newY);
            SaveProgramButton.Location = new Point(newButtonWidth * 3, newY);
            // dimensions
            toggleKeyboardButton.Size = new Size(newButtonWidth, 50);
            toggleDebugButton.Size = new Size(newButtonWidth, 50);
            LoadProgramButton.Size = new Size(newButtonWidth, 50);
            SaveProgramButton.Size = new Size(newButtonWidth, 50);

            GLOBAL.C64_ROM.LoadCharsetData(); // loads character set
            GLOBAL.VRAM.FillContents(0x20); // fills the vram (characters on screen) with spaces, hex 0x20

            // setup display and print the default text on boot
            displayPictureBox.Image = GraphicsHandler.GetDisplay();
            GLOBAL.C64_ROM.ShowStartupText();
            GLOBAL.cursorPos = new int[] { 0, 32 };
            PrintReady(8);
            GLOBAL.cursorPos = new int[] { 0, 48 };

            // sets up the performance monitoring object
            string currentProcessName = Process.GetCurrentProcess().ProcessName;
            cpuUsage = new PerformanceCounter("Process", "% Processor Time", currentProcessName, true);
            ramUsage = new PerformanceCounter("Process", "Private Bytes", currentProcessName, true);

            // cursor fires CursorTick_Event three times a second
            cursorTick.Interval = 333;
            cursorTick.Tick += new EventHandler(CursorTick_Event);
            cursorTick.Enabled = true;
        }

        private void UpdateDisplay() // redraws the display to the screen, cannot be in GraphicsHandler due to drawing to the forms elements
        {
            // start timer to see how long frame takes
            Stopwatch frameDrawTime = Stopwatch.StartNew();

            // draws the black borders around the main window
            GraphicsHandler.DrawBorder();

            int[] vram = GLOBAL.VRAM.GetContents(); // gets the characters on screen

            // draw each character to the screen
            for (int i = 0; i < vram.Length; i++)
            {
                GraphicsHandler.DrawChar(i % 40, i / 40, vram[i]); // i % 40, i / 40 turns the 1D vram array to 2D co-ordinates for the GraphicsHandler object
            }

            // gets window size properties
            int[] dimensions = GraphicsHandler.GetDimensions();
            int width = dimensions[0];
            int height = dimensions[1];

            int borderSize = GLOBAL.AppConfig.border_size;
            int displayScale = GLOBAL.AppConfig.display_scale;

            // upscales the output by displayScale with nearest neighbour (for no blurring pixels)
            displayPictureBox.Image = GraphicsHandler.DrawCursor(GraphicsHandler.GetDisplay(), GLOBAL.cursorPos[0], GLOBAL.cursorPos[1], cursorVisible);
            Graphics graphics = Graphics.FromImage(zoomedDisplay);
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            // draw the upscaled image to the screen
            graphics.DrawImage(displayPictureBox.Image, 0, 0, (width + (borderSize * 2)) * displayScale, (height + (borderSize * 2)) * displayScale);
            displayPictureBox.Image = zoomedDisplay;
            // update screen and clear memory
            displayPictureBox.Refresh();
            graphics.Dispose();

            // store how long it took to draw that drame
            frameDrawTime.Stop();
            lastFrameTime = frameDrawTime.Elapsed.TotalMilliseconds;
        }

        public static void PrintReady(int offset) // draws the "READY." text with spacing above and below
        {
            byte[] text = { 0x12, 0x05, 0x01, 0x04, 0x19, 0x2e };
            int[] cursorPos = GLOBAL.cursorPos;
            cursorPos[0] = 0;
            cursorPos[1] += offset;

            for (int i = 0; i < text.Length; i++)
            {
                if (GLOBAL.cursorPos[1] >= 200)
                {
                    ShiftDisplayUp();
                }
                GLOBAL.VRAM.SetValue(((cursorPos[1] / 8) * 40) + (cursorPos[0] / 8) + i, text[i]);
            }
        }

        private string GetCharsFromKeys(Keys keys, bool shiftHeld)
        {
            StringBuilder buffer = new StringBuilder(256);
            byte[] keyboardState = new byte[256];

            if (shiftHeld)
            {
                keyboardState[(int)Keys.ShiftKey] = 0xff;
            }

            ToUnicode((uint)keys, 0, keyboardState, buffer, 256, 0);

            return buffer.ToString().ToUpper();
        }

        private void CursorTick_Event(Object sender, EventArgs e) // cursor toggle (called 3 times a second)
        {
            // flip the cursor visiblilty and update the display
            cursorVisible = !cursorVisible;
            UpdateDisplay();
        }

        private void MainWindow_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e) // key pressed event
        {
            this.Select(); // focus on a the form rather than buttons to capture all keys and prevent accidental button presses with enter/spacebar
            e.IsInputKey = true; // if pressing arrow keys, dont move focus on elements in the form

            // cursor position
            int posX = GLOBAL.cursorPos[0];
            int posY = GLOBAL.cursorPos[1];

            bool shiftHeld = (ModifierKeys & Keys.Shift) == Keys.Shift;

            // if the character pressed is in the charMap, add it to vram in the cursor location
            string charPressed = GetCharsFromKeys(e.KeyCode, shiftHeld); // c# KeyCode data type to the character equivilent
            if (charPressed != "" && charPressed != "_") // not empty or unused key - the unused key (leftwards arrow) is essentially a duplicate of "="
            {
                int charIndex = charMap.IndexOf(charPressed); // index of the character in charMap
                if (charIndex >= 0 && charIndex <= 127) // validity check for text characters
                {
                    // store character in vram and move the cursor
                    GLOBAL.VRAM.SetValue((posY / 8) * 40 + (posX / 8), charIndex);
                    posX += 8;
                }
            }

            // control characters (arrow keys and enter)
            switch (e.KeyCode)
            {
                case Keys.Up:
                    posY += -8;
                    break;
                case Keys.Down:
                    posY += 8;
                    break;
                case Keys.Left:
                    posX += -8;
                    break;
                case Keys.Right:
                    posX += 8;
                    break;
                case Keys.Enter:
                    switch (GLOBAL.ProgramRAM.CheckCommandType())
                    {
                        case "number":
                            GLOBAL.ProgramRAM.SaveLineToRAM();
                            posX = 0;
                            posY += 8;
                            break;
                        case "text":
                            if (GLOBAL.ProgramExecutor.BytesToString(GLOBAL.ProgramRAM.GetLatestLine()).Trim().IndexOf("RUN") != 0)
                            {
                                if (GLOBAL.ProgramExecutor.ExecuteCommand(GLOBAL.ProgramRAM.GetLatestLine()))
                                {
                                    PrintReady(24);
                                    posX = 0;
                                    posY += 32;
                                }
                                else
                                {
                                    PrintReady(8);
                                    posX = 0;
                                    posY += 32;
                                }
                            }
                            else
                            {
                                GLOBAL.ProgramExecutor.ExecuteCommand(GLOBAL.ProgramRAM.GetLatestLine());
                                posX = 0;
                                posY += 8 * GLOBAL.ProgramExecutor.lineCount + 24;
                                PrintReady(24);
                            }
                            break;
                        case "special":
                            ErrorHandler.ShowEmulatorError("syntax");
                            PrintReady(0);
                            posX = 0;
                            posY += 32;
                            break;
                        case "graphics":
                            ErrorHandler.ShowEmulatorError("syntax");
                            PrintReady(0);
                            posX = 0;
                            posY += 32;
                            break;
                        case "empty":
                            break;
                    }
                    break;
                case Keys.Back:
                    GLOBAL.VRAM.SetValue((posY / 8) * 40 + ((posX / 8) - 1), 0x20);
                    posX -= 8;
                    break;
            }

            // if cursor goes off the screen to the, reset its position on screen again
            if (posX >= 320) // right
            {
                posX = 0;
                posY += 8;
            }
            else if (posX < 0) // left
            {
                posX = 312;
                posY -= 8;
            }
            if (posY >= 200) // bottom
            {
                ShiftDisplayUp();
                posY -= 32;
            }
            else if (posY < 0) // top
            {
                posY += 8;
            }

            // update the cursor position and draw it to the screen
            GLOBAL.cursorPos = new int[] { posX, posY };
            cursorVisible = true;

            UpdateDisplay();
        }

        private void ToggleKeyboardButton_Click(object sender, EventArgs e)
        {
            if (!keyboardWindowOpen)
            {
                // create a new keyboard window and prevent any more from being opened
                keyboardWindow = new KeyboardWindow();
                keyboardWindow.Show();
                keyboardWindowOpen = true;
            }
            else
            {
                // close the open keyboard window
                keyboardWindow.Close();
            }
        }

        private void ToggleDebugButton_Click(object sender, EventArgs e) // button to open debug window pressed
        {
            if (!debugWindowOpen)
            {
                // create a new debug window and prevent any more from being opened
                debugWindow = new DebugWindow();
                debugWindow.Show();
                debugWindowOpen = true;
            }
            else
            {
                // close the open debug window
                debugWindow.Close();
            }
        }

        private void SaveProgramButton_Click(object sender, EventArgs e)
        {
            doingFileStuff = true;
            FileHandler.SaveProgram();
            doingFileStuff = false;
        }

        private void LoadProgramButton_Click(object sender, EventArgs e)
        {
            doingFileStuff = true;
            FileHandler.LoadProgram();
            doingFileStuff = false;
        }

        public static string GetCharMap()
        {
            return charMap;
        }

        public static void ShiftDisplayUp()
        {
            int[] VRAM = GLOBAL.VRAM.GetContents();

            for (int i = 0; i < VRAM.Length - 40; i++)
            {
                VRAM[i] = VRAM[i + 40];
            }

            for (int i = 0; i < VRAM.Length; i++)
            {
                GLOBAL.VRAM.SetValue(i, VRAM[i]);
            }

            for (int i = GLOBAL.VRAM.GetContents().Length - 40; i < GLOBAL.VRAM.GetContents().Length; i++)
            {
                GLOBAL.VRAM.SetValue(i, 0x20);
            }

            //to fix
            GLOBAL.cursorPos[1] -= 80;
            Console.WriteLine("X: " + GLOBAL.cursorPos[0] + ", Y: " + GLOBAL.cursorPos[1]);
        }
    }
}
