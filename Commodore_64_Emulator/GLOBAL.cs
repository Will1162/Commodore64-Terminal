namespace Commodore_64_Emulator
{
    public static class GLOBAL
    {
        // this class hold important parts of the program which need to be accessable everywhere

        public static int[] cursorPos = { 0, 40 }; //holds the cursor pixel position [x, y]
        public static App_Config AppConfig = new App_Config(); // holds the app_config.json values, if valid
        public static ROM C64_ROM = new ROM(); // the main area for constant values
        public static RAM VRAM = new RAM(40 * 25); // the positions of what characters are where on the screen (40 x 25 character grid slots)
        public static ProgramRAM ProgramRAM = new ProgramRAM(512);
        public static ProgramExecutor ProgramExecutor = new ProgramExecutor();
    }
}
