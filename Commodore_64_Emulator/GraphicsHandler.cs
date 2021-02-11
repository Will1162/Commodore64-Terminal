using System.Drawing;

namespace Commodore_64_Emulator
{
    public class GraphicsHandler
    {
        // window dimensions
        private static int width = 320;
        private static int height = 200;
        private static int borderSize = GLOBAL.AppConfig.border_size;

        // the bitmap that is to be upscaled and drawn to the screen
        // a 

        private static FastBitmap display = new FastBitmap(width + (borderSize * 2), height + (borderSize * 2));

        public static void DrawRect(int x, int y, int w, int h, Color colour) // draws rectangle skipping the border
        {
            // start where the border ends
            x += borderSize;
            y += borderSize;

            for (int i = x; i < x + w; i++)
            {
                for (int j = y; j < y + h; j++)
                {
                    display.SetPixel(i, j, colour);
                }
            }
        }

        public static void DrawRect(int x, int y, int w, int h, Color colour, int drawOnBorder) // if drawOnBorder is set, drawing will start from 0, 0
        {
            for (int i = x; i < x + w; i++)
            {
                for (int j = y; j < y + h; j++)
                {
                    display.SetPixel(i, j, colour);
                }
            }
        }

        public static void DrawPixel(int x, int y, Color colour) // draws pixel skipping the border
        {
            // start where the border ends
            x += borderSize;
            y += borderSize;

            // when using x, y instead of y, x, the image is rotated 90 degrees, this seems to be the easiest fix
            display.SetPixel(y, x, colour);
        }

        public static void DrawPixel(int x, int y, Color colour, int skipBorder) // if skipBorder is set, drawing will start from 0, 0
        {
            display.SetPixel(y, x, colour);
        }

        public static void DrawPixel(int x, int y, Color colour, Bitmap bitmap) // returns the bitmap with changed pixel
        {
            bitmap.SetPixel(y, x, colour);
        }

        public static void DrawChar(int x, int y, int charID) // draws the character of ID charID at grid position x, y
        {
            for (int i = 0; i < 8; i++) // loop through character rows
            {
                for (int j = 0; j < 8; j++) // loop through character columns
                {
                    if (charID >= 0 && charID <= 127)
                    {
                        // if j column bit is set (1 row is 8 bits, 1 bit for each column, or 1 byte)
                        if ((GLOBAL.C64_ROM.GetCharsetData(charID, i) & (0x80 >> j)) == 0x80 >> j)
                        {
                            DrawPixel(i + y * 8, j + x * 8, Colours.GetColour(GLOBAL.AppConfig.foreground_colour)); // set pixel light
                        }
                        else
                        {
                            DrawPixel(i + y * 8, j + x * 8, Colours.GetColour(GLOBAL.AppConfig.background_colour)); // set pixel dark
                        }
                    }
                }
            }
        }

        public static void DrawChar(int x, int y, int charID, Bitmap bitmap) // used exclusivley for drawing to the external keyboard gui
        {
            for (int i = 0; i < 8; i++) // loop through character rows
            {
                for (int j = 0; j < 8; j++) // loop through character columns
                {
                    if (charID >= 0 && charID <= 127)
                    {
                        // if j column bit is set (1 row is 8 bits, 1 bit for each column, or 1 byte)
                        if ((GLOBAL.C64_ROM.GetCharsetData(charID, i) & (0x80 >> j)) == 0x80 >> j)
                        {
                            DrawPixel(i + y, j + x, Colours.GetColour(0), bitmap); // set pixel black
                        }
                        else
                        {
                            DrawPixel(i + y, j + x, Colours.GetColour(4), bitmap); // set pixel white
                        }
                    }
                }
            }
        }

        public static Bitmap DrawCursor(Bitmap display, int x, int y, bool cursorVisible) // inverts 8x8 square where cursor is
        {
            if (cursorVisible && !MainWindow.doingFileStuff)
            {
                // loop through columns
                for (int i = x + borderSize; i < 8 + x + borderSize; i++)
                {
                    // loop through rows
                    for (int j = y + borderSize; j < 8 + y + borderSize; j++)
                    {
                        if (i >= 0 && i < 320 && j >= 0 && j < 200)
                        {
                            // swap light blue and dark blue colours
                            if (display.GetPixel(i, j) == Colours.GetColour(GLOBAL.AppConfig.foreground_colour))
                            {
                                display.SetPixel(i, j, Colours.GetColour(GLOBAL.AppConfig.background_colour));
                            }
                            else if (display.GetPixel(i, j) == Colours.GetColour(GLOBAL.AppConfig.background_colour))
                            {
                                display.SetPixel(i, j, Colours.GetColour(GLOBAL.AppConfig.foreground_colour));
                            }
                        }
                    }
                }
            }

            // return the new image with the cursor displayed on it
            return display;
        }

        public static void DrawBorder() // draws the back border to the screen
        {
            //       x                   y                    width                     height                     colour
            DrawRect(0,                  0,                   width + (borderSize * 2), borderSize,                Colours.GetColour(GLOBAL.AppConfig.border_colour), 1); // top
            DrawRect(0,                  height + borderSize, width + (borderSize * 2), borderSize,                Colours.GetColour(GLOBAL.AppConfig.border_colour), 1); // bottom
            DrawRect(0,                  0,                   borderSize,               height + (borderSize * 2), Colours.GetColour(GLOBAL.AppConfig.border_colour), 1); // left
            DrawRect(width + borderSize, 0,                   borderSize,               height + (borderSize * 2), Colours.GetColour(GLOBAL.AppConfig.border_colour), 1); // right
        }

        public static int[] GetDimensions() // returns the unscaled width and height
        {
            return new int[] { width, height };
        }

        public static Bitmap GetDisplay() // returns the display bitmap data
        {
            return display.GetContents();
        }
    }
}
