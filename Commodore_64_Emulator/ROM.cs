using System.Drawing;
using System.IO;

namespace Commodore_64_Emulator
{
    public class ROM
    {
        private int charsetLength = 128; // the length of the character set
        private Bitmap charsetImg; // the bitmap of the character set used, should be black and white
        private byte[,] charsetData; // the bitmap in a form that can be more easily read by the program

        // the default text shown at the top of the screen on boot (40 x 4 size)
        private byte[] startupText = {
            0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43,
            0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43, 0x43,
            0x20, 0x20, 0x2a, 0x20, 0x2a, 0x20, 0x2a, 0x20, 0x20, 0x20, 0x03, 0x0f, 0x0d, 0x0d, 0x0f, 0x04, 0x0f, 0x12, 0x05, 0x36,
            0x34, 0x20, 0x05, 0x0d, 0x15, 0x0c, 0x01, 0x14, 0x0f, 0x12, 0x20, 0x20, 0x20, 0x2a, 0x20, 0x2a, 0x20, 0x2a, 0x20, 0x20,
            0x20, 0x2a, 0x20, 0x2a, 0x20, 0x2a, 0x20, 0x20, 0x02, 0x19, 0x20, 0x17, 0x09, 0x0c, 0x0c, 0x20, 0x02, 0x15, 0x12, 0x0c,
            0x01, 0x0e, 0x04, 0x20, 0x20, 0x32, 0x30, 0x32, 0x30, 0x2d, 0x32, 0x31, 0x20, 0x20, 0x2a, 0x20, 0x2a, 0x20, 0x2a, 0x20,
            0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45,
            0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45, 0x45
        };

        public void LoadCharsetData() // turns the charsetImg into charsetData
        {
            int errorCode = 0;
            try
            {
                if (GLOBAL.AppConfig.custom_charset_path.Length == 0) // if no path is set in app_config.json, use the default
                {
                    charsetImg = new Bitmap(Properties.Resources.default_charset);
                }
                else
                {
                    if (!File.Exists(GLOBAL.AppConfig.custom_charset_path)) // if file given doesn't exist, throw error
                    {
                        errorCode = 5;
                        throw new System.Exception("Character set image not found");
                    }

                    charsetImg = new Bitmap(GLOBAL.AppConfig.custom_charset_path); // use custom character set image

                    if (charsetImg.Width != 128 || charsetImg.Height != 64) // if wrong dimensions, throw error
                    {
                        errorCode = 2;
                        throw new System.Exception("Invalid charset dimensions");
                    }
                }
            }
            catch
            {
                // show the error given and use default character set
                ErrorHandler.ShowSystemError(errorCode);
                charsetImg = new Bitmap(Properties.Resources.default_charset);
            }

            charsetData = new byte[charsetLength, 8];

            // loop through all characters, one by one, looping through each of their pixels and adding them to charsetData
            // character set image rows (in characters, not pixels)
            for (int h = 0; h < 8; h++)
            {
                // character set image columns (in pixels, not characters)
                for (int i = 0; i < 128; i += 8)
                {
                    // individual character rows
                    for (int j = h * 8; j < h * 8 + 8; j++)
                    {
                        byte temp = 0;

                        // individual character columns
                        for (int k = 0; k < 8; k++)
                        {
                            // turns 1 row of 1 character into 1 byte. each pixel is 1 bit of data, black or white
                            Color pixel = charsetImg.GetPixel(i + (7 - k), j);
                            byte bit = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                            if (bit <= 127)
                            {
                                bit = 0;
                            }
                            else if (bit > 127)
                            {
                                bit = 255;
                            }
                            temp += (byte)(bit & (0x01 << k));
                        }

                        // sets the character row byte into charsetData in its correct location
                        charsetData[(i / 8) + (h * 16), j % 8] = temp;
                    }
                }
            }
        }

        public void ShowStartupText() // draws the starting text on boot
        {
            // copies startupText into the VRAM (characters on screen)
            for (int i = 0; i < startupText.Length; i++)
            {
                GLOBAL.VRAM.SetValue(i, startupText[i]);
            }
        }

        public byte GetCharsetData(int charID, int i) // returns a specific character's data
        {
            return charsetData[charID, i];
        }
    }
}
