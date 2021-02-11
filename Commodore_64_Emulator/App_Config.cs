namespace Commodore_64_Emulator
{
    public class App_Config
    {
        // this class holds options the user can change about the look of the program

        public int display_scale { get; set; } // the amount to scale the GUI by
        public string custom_charset_path { get; set; } // the path to a custom character set image
        public int border_size { get; set; } // the thickness of the border
        public int background_colour { get; set; } // the area where text isnt
        public int foreground_colour { get; set; } // text colour
        public int border_colour { get; set; } // the area around the main program
    }
}
