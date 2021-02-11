using System.Drawing;

namespace Commodore_64_Emulator
{
    public static class Colours
    {
        // this class contains the colours that can be used in the program

        // defines colours available to draw to the screen (these are the actual colour values used on the C64)
        private static Color Black       = Color.FromArgb(0  , 0  , 0  );
        private static Color Dark_Grey   = Color.FromArgb(98 , 98 , 98 );
        private static Color Mid_Grey    = Color.FromArgb(137, 137, 137);
        private static Color Light_Grey  = Color.FromArgb(173, 173, 173);
        private static Color White       = Color.FromArgb(255, 255, 255);
        private static Color Light_Red   = Color.FromArgb(203, 126, 117);
        private static Color Red         = Color.FromArgb(159, 78, 68);
        private static Color Brown       = Color.FromArgb(109, 84 , 18 );
        private static Color Orange      = Color.FromArgb(161, 104, 60 );
        private static Color Yellow      = Color.FromArgb(201, 212, 135);
        private static Color Dark_Green  = Color.FromArgb(92, 171, 94);
        private static Color Light_Green = Color.FromArgb(154, 226, 155);
        private static Color Cyan        = Color.FromArgb(106, 191, 198);
        private static Color Light_Blue  = Color.FromArgb(136, 126, 203);
        private static Color Dark_Blue   = Color.FromArgb(80 , 69 , 155);
        private static Color Pink        = Color.FromArgb(160, 87 , 163);

        public static Color GetColour(int colourIndex)
        {
            // returns the Color object of the specified colourIndex

            switch (colourIndex)
            {
                case 0:
                    return Black;
                case 1:
                    return Dark_Grey;
                case 2:
                    return Mid_Grey;
                case 3:
                    return Light_Grey;
                case 4:
                    return White;
                case 5:
                    return Light_Red;
                case 6:
                    return Red;
                case 7:
                    return Brown;
                case 8:
                    return Orange;
                case 9:
                    return Yellow;
                case 10:
                    return Dark_Green;
                case 11:
                    return Light_Green;
                case 12:
                    return Cyan;
                case 13:
                    return Light_Blue;
                case 14:
                    return Dark_Blue;
                case 15:
                    return Pink;
                default:
                    return Black; // if no or invalid colourIndex, return black
            }
        }
    }
}
