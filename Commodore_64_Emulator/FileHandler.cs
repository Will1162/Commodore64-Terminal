using System.Text.Json;
using System.IO;
using System.Windows.Forms;

namespace Commodore_64_Emulator
{
    public class FileHandler 
    {
        // this class handles the majority of the input and output of the file and interacting with files

        public static void ReadAppConfig()
        {
            // reads the app_config.json

            try
            {
                if (!File.Exists(Application.StartupPath + @"\app_config.json"))
                {
                    ErrorHandler.ShowSystemError(4); // if config file isnt found
                }
                // attempt to read user json file, and set the AppConfig variables to the given values
                string json_data = File.ReadAllText(Application.StartupPath + @"\app_config.json").Replace(@"\", "/");
                GLOBAL.AppConfig = JsonSerializer.Deserialize<App_Config>(json_data);
            }
            catch
            {
                // if json file is invalid
                ErrorHandler.ShowSystemError(1);
                // use default configiration
                GLOBAL.AppConfig = JsonSerializer.Deserialize<App_Config>(Properties.Resources.default_app_config);
            }

            // background colour validity check
            if (GLOBAL.AppConfig.background_colour < 0 || GLOBAL.AppConfig.background_colour > 15)
            {
                ErrorHandler.ShowSystemError(7);
                GLOBAL.AppConfig.background_colour = 14;
            }

            // foreground colour validity check
            if (GLOBAL.AppConfig.foreground_colour < 0 || GLOBAL.AppConfig.foreground_colour > 15)
            {
                ErrorHandler.ShowSystemError(8);
                GLOBAL.AppConfig.foreground_colour = 13;
            }

            // border colour validity check
            if (GLOBAL.AppConfig.border_colour < 0 || GLOBAL.AppConfig.border_colour > 15)
            {
                ErrorHandler.ShowSystemError(9);
                GLOBAL.AppConfig.border_colour = 0;
            }
        }

        public static void SaveProgram()
        {
            // saves the program saved in ProgramRAM, to a file

            // remove excess unused data and copy data to a new file
            byte[] contents = TrimProgram(GLOBAL.ProgramRAM.GetContents());
            byte[] byteContents = new byte[contents.Length];

            for (int i = 0; i < contents.Length; i++)
            {
                byteContents[i] = contents[i];
            }

            // open a Windows save file prompt
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "program";
            saveFileDialog.Filter = "CW64 Files|*.cw64";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (saveFileDialog.FileName != "")
                {
                    File.WriteAllBytes(saveFileDialog.FileName, byteContents);
                }
            }
        }

        public static void LoadProgram()
        {
            // loads a user chosen file into ProgramRAM

            // Windows will ask the user to select a file
            OpenFileDialog loadFileDialog = new OpenFileDialog();
            loadFileDialog.Filter = "CW64 Files|*.cw64";
            if (loadFileDialog.ShowDialog() == DialogResult.OK)
            {
                GLOBAL.ProgramRAM.FillContents(0x20);
                byte[] byteFile = File.ReadAllBytes(loadFileDialog.FileName);

                string[] program = new string[byteFile.Length / 40]; // each line of the program stored here
                string charMap = MainWindow.GetCharMap();

                for (int i = 0; i < byteFile.Length; i++)
                {
                    int row = i / 40;
                    if (row >= byteFile.Length / 40)
                    {
                        row--;
                    }
                    program[row] += charMap[byteFile[i]];
                }

                for (int i = 0; i < program.Length; i++)
                {
                    for (int j = 0; j < program[i].Length; j++)
                    {
                        GLOBAL.ProgramRAM.SetValue(i * 40 + j, charMap.IndexOf(program[i][j]));
                    }
                }
            }
        }

        public static byte[] TrimProgram(int[] oldProgram)
        {
            int lastCharIndex = GLOBAL.ProgramRAM.GetCurrentIndex();

            byte[] newProgram = new byte[lastCharIndex];

            for (int i = 0; i < lastCharIndex; i++)
            {
                newProgram[i] = (byte)oldProgram[i];
            }

            return newProgram;
        }
    }
}
