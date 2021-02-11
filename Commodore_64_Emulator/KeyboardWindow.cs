using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Commodore_64_Emulator
{
    public partial class KeyboardWindow : Form
    {
        private byte[] textCharactersLayout =
        {
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x21, 0x22, 0x1c, 0x24, 0x25, 0x1e,
            0x11, 0x17, 0x05, 0x12, 0x14, 0x19, 0x15, 0x09, 0x0f, 0x10, 0x1b, 0x1d, 0x28, 0x29, 0x26, 0x2a,
            0x01, 0x13, 0x04, 0x06, 0x07, 0x08, 0x0a, 0x0b, 0x0c, 0x3a, 0x3b, 0x27, 0x00, 0x23, 0x2f, 0x3f,
            0x1a, 0x18, 0x03, 0x16, 0x02, 0x0e, 0x0d, 0x2c, 0x2e, 0x3c, 0x3e, 0x2d, 0x3d, 0x2b
        }; // the layout of the text characters in the GUI keyboard

        public KeyboardWindow()
        {
            InitializeComponent();
        }

        private void KeyboardWindow_Load(object sender, EventArgs e)
        {
            int scale = 4;
            Bitmap originalCharset = Properties.Resources.default_charset;
            int width = originalCharset.Width;
            int height = originalCharset.Height;
            Bitmap zoomedDisplay = new Bitmap(originalCharset.Width * scale, originalCharset.Height * scale);
            Bitmap newBitmap = new Bitmap(width + scale * ((width / 8) - 1), height + scale * ((height / 7) - 1));

            textCharactersImage.Size = new Size(zoomedDisplay.Width, zoomedDisplay.Height);
            this.Size = new Size(zoomedDisplay.Width + 36, zoomedDisplay.Height + 32);

            int x = 0;
            int y = 0;
            for (int i = 0; i < textCharactersLayout.Length; i++)
            {
                if (i == 0x30) // skip a space at the start of the 3rd row
                {
                    x++;
                }

                GraphicsHandler.DrawChar(x * 8 + x * scale, y * 8 + y * scale, textCharactersLayout[i], newBitmap);
                x++;
                if (x > 15)
                {
                    x = 0;
                    y++;
                }
            }

            textCharactersImage.Image = newBitmap;
            Graphics graphics = Graphics.FromImage(zoomedDisplay);
            graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            // draw the upscaled image to the screen
            graphics.DrawImage(textCharactersImage.Image, 0, 0, 128 * scale, 64 * scale);
            textCharactersImage.Image = zoomedDisplay;
            // update screen and clear memory
            textCharactersImage.Refresh();
            graphics.Dispose();
        }

        private void KeyboardWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            // allow another window to be opened again
            MainWindow.keyboardWindowOpen = false;
        }

        private void textCharactersImage_Click(object sender, EventArgs e)
        {
            Point cursorPos = textCharactersImage.PointToClient(Cursor.Position);

            if (cursorPos.X % 32 <= 20 && cursorPos.Y % 32 <= 20)
            {
                int posX = cursorPos.X / 32;
                int posY = cursorPos.Y / 32;

                if (posY == 3)
                {
                    posX--;
                }

                GLOBAL.VRAM.SetValue((GLOBAL.cursorPos[1] / 8) * 40 + (GLOBAL.cursorPos[0] / 8), textCharactersLayout[posY * 16 + posX]);
                GLOBAL.cursorPos[0] += 8;
            }
        }

        private void ControlButton_Click(object sender, EventArgs e)
        {
            string buttonName = ((Button)sender).Name;


            int posX = GLOBAL.cursorPos[0];
            int posY = GLOBAL.cursorPos[1];

            switch (buttonName)
            {
                case "upButton":
                    posY += -8;
                    break;
                case "downButton":
                    posY += 8;
                    break;
                case "leftButton":
                    posX += -8;
                    break;
                case "rightButton":
                    posX += 8;
                    break;
                case "enterButton":
                    switch (GLOBAL.ProgramRAM.CheckCommandType())
                    {
                        case "number":
                            GLOBAL.ProgramRAM.SaveLineToRAM();
                            posX = 0;
                            posY += 8;
                            break;
                        case "text":
                            if (GLOBAL.ProgramExecutor.ExecuteCommand(GLOBAL.ProgramRAM.GetLatestLine()))
                            {
                                MainWindow.PrintReady(24);
                                posX = 0;
                                posY += 32;
                            }
                            else
                            {
                                MainWindow.PrintReady(8);
                                posX = 0;
                                posY += 32;
                            }
                            break;
                        case "special":
                            ErrorHandler.ShowEmulatorError("syntax");
                            MainWindow.PrintReady(0);
                            posX = 0;
                            posY += 32;
                            break;
                        case "graphics":
                            ErrorHandler.ShowEmulatorError("syntax");
                            MainWindow.PrintReady(0);
                            posX = 0;
                            posY += 32;
                            break;
                        case "empty":
                            break;
                    }
                    break;
                case "backspaceButton":
                    GLOBAL.VRAM.SetValue((posY / 8) * 40 + ((posX / 8) - 1), 0x20);
                    posX -= 8;
                    break;
                case "spaceButton":
                    GLOBAL.VRAM.SetValue((posY / 8) * 40 + (posX / 8), 0x20);
                    posX += 8;
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
                MainWindow.ShiftDisplayUp();
                posY -= 32;
            }
            else if (posY < 0) // top
            {
                posY += 8;
            }

            // update the cursor position and draw it to the screen
            GLOBAL.cursorPos = new int[] { posX, posY };
            MainWindow.cursorVisible = true;
        }
    }
}
