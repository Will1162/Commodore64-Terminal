using System.Windows.Forms;

namespace Commodore_64_Emulator
{
    public class ErrorHandler
    {
        // handles errors, both in the program itself, and errors in the inputted commands

        public static void ShowSystemError(int errorCode)
        {
            // displays a message box and its corresponding error code/message in Windows

            string title = "";
            string text = "";
            MessageBoxButtons buttons = MessageBoxButtons.OK;

            switch (errorCode)
            {
                case 0:
                    title = "A error has occured";
                    text = "Error code " + errorCode.ToString() + "\n\nCould not read or find charset.png\nThe default charset will be used.";
                    buttons = MessageBoxButtons.OK;
                    break;
                case 1:
                    title = "A error has occured";
                    text = "Error code " + errorCode.ToString() + "\n\nCould not read app_config.json, invalid contents\nThe default settings will be used.";
                    buttons = MessageBoxButtons.OK;
                    break;
                case 2:
                    title = "A error has occured";
                    text = "Error code " + errorCode.ToString() + "\n\nInvalid charset.png dimensions (must be 128 x 64)\nThe default charset will be used.";
                    buttons = MessageBoxButtons.OK;
                    break;
                case 4:
                    title = "A error has occured";
                    text = "Error code " + errorCode.ToString() + "\n\nCould not find app_config.json. Make sure it is in the same directory as the .exe.\nThe default settings will be used.";
                    buttons = MessageBoxButtons.OK;
                    break;
                case 5:
                    title = "A error has occured";
                    text = "Error code " + errorCode.ToString() + "\n\nCould not find charset.png. Make sure the path in app_config.json is correct.\nFor example:\n\"C:/Users/JohnDoe/Documents/charset.png\"\nThe default charset will be used.";
                    buttons = MessageBoxButtons.OK;
                    break;
                case 6:
                    title = "A error has occured";
                    text = "Error code " + errorCode.ToString() + "\n\nDisplay scale value makes window larger than screen. Edit this in app_config.json\nThe default display scale will be used.";
                    buttons = MessageBoxButtons.OK;
                    break;
                case 7:
                    title = "A error has occured";
                    text = "Error code " + errorCode.ToString() + "\n\nInvalid background colour option, must be between 0 and 15. Edit this in app_config.json\nThe default colour will be used.";
                    buttons = MessageBoxButtons.OK;
                    break;
                case 8:
                    title = "A error has occured";
                    text = "Error code " + errorCode.ToString() + "\n\nInvalid foreground colour option, must be between 0 and 15. Edit this in app_config.json\nThe default colour will be used.";
                    buttons = MessageBoxButtons.OK;
                    break;
                case 9:
                    title = "A error has occured";
                    text = "Error code " + errorCode.ToString() + "\n\nInvalid border colour option, must be between 0 and 15. Edit this in app_config.json\nThe default colour will be used.";
                    buttons = MessageBoxButtons.OK;
                    break;
                default:
                    break;
            }

            MessageBox.Show(text, title, buttons);
        }

        public static void ShowEmulatorError(string errorReason)
        {
            // displays a error text line in the emulator

            errorReason = errorReason.ToUpper() + " ERROR";
            int[] cursorPos = GLOBAL.cursorPos;
            cursorPos[0] = 0;
            cursorPos[1] += 16;

            // write error
            for (int i = 0; i < errorReason.Length; i++)
            {
                GLOBAL.VRAM.SetValue((cursorPos[1] / 8) * 40 + (cursorPos[0] / 8), MainWindow.GetCharMap().IndexOf(errorReason[i]));
                cursorPos[0] += 8;
            }
        }

        public static void ShowEmulatorError(string errorReason, string extraInfo)
        {
            // displays a error text line in the emulator, with extra information

            errorReason = errorReason.ToUpper() + " ERROR - " + extraInfo.ToUpper();
            int[] cursorPos = GLOBAL.cursorPos;
            cursorPos[0] = 0;
            cursorPos[1] += 16;

            // write error
            for (int i = 0; i < errorReason.Length; i++)
            {
                GLOBAL.VRAM.SetValue((cursorPos[1] / 8) * 40 + (cursorPos[0] / 8), MainWindow.GetCharMap().IndexOf(errorReason[i]));
                cursorPos[0] += 8;
            }
        }
    }
}
