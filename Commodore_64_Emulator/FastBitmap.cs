using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Commodore_64_Emulator
{
    public class FastBitmap
    {
        /*
            This class is an extension of the default Bitmap class used in C#.
            Its main purpose is to provide MUCH faster speeds in the two main Bitmap functions, SetPixel and GetPixel.
            My testing shows that with the default Bitmap class, it would take ~50ms to draw 1 frame, or 25 frames per second
            With this class, drawing 1 frame only takes ~6ms, or ~167 frames per second. That's over an 8 times improvement in performance.
        
            The way this object differs is that the actual data is not stored in the Bitmap, but rather a large seperate array.
            This helps improve performance as changing the data from an array is MUCH faster than changing data in a Bitmap
            The improvement in performance most likley comes from the overhead in accessing a more complex object/class (Bitmap),
            rather than accessing a more fundemantal data type (array), which is just sequential values in memory.
        */

        private Bitmap contents; // the final result of the bitmap
        private int[] data; // the actual data of the the bitmap
        private int width;
        private int height;

        protected GCHandle dataHandle; // used in allocating/locating memory

        public FastBitmap(int newWidth, int newHeight)
        {
            // on creating object

            // sets the dimensions of the bitmap data array
            width = newWidth;
            height = newHeight;
            data = new int[width * height];

            // copies the data from "data" into "contents" directly, without using overhead such as the default SetPixel function
            // the dataHandle object is used to get the address in memory of the "data" array to copy to the bitmap
            dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            contents = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, dataHandle.AddrOfPinnedObject());
        }

        public void SetPixel(int x, int y, Color newColour)
        {
            // sets a pixel of co-ordinates x, y with the specified colour

            // x + (y * width) turns a 2D x, y position into a 1D sequential array index, newColour is then stored there
            data[x + (y * width)] = newColour.ToArgb();
        }

        public Color GetPixel(int x, int y)
        {
            // returns the colour of a pixel at co-ordinates x, y

            // x + (y * width) turns a 2D x, y position into a 1D sequential array index, the value at that index is then returned
            return Color.FromArgb(data[x + (y * width)]);
        }

        public Bitmap GetContents()
        {
            // returns the contents bitmap

            return contents;
        }
    }
}
