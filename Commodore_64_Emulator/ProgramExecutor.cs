using System;

namespace Commodore_64_Emulator
{
    public class ProgramExecutor
    {
        // this class runs the code the user enters

        private string[] validCommands =
        {
            "ABS", "ASC",   "ATN", "CHR",   "COS", "EXP", "INT", "LEFT", "LEN", "LET", "LOG",
            "MID", "PRINT", "REM", "RIGHT", "RND", "RUN", "SGN", "SIN",  "SQR", "TAN", "WAIT"
        };

        private int[] parameterCount =
        { 
            1, 1, 1, 1, 1, 1, 1, 2, 1, 2, 1,
            3, 1, 0, 2, 2, 0, 1, 1, 1, 1, 1
        };

        public int lineCount = 0;

        public bool ExecuteCommand(byte[] command)
        {
            if (BytesToString(command).Split(' ').Length - 1 != 40 && BytesToString(command).Split('@').Length - 1 != 40)
            {
                if (ValidCommand(RemoveLineNumber(BytesToString(command))))
                {
                    string[] filteredCommand = FilterCommand(RemoveLineNumber(BytesToString(command)));
                    if (filteredCommand[0] != "" && filteredCommand[1] != "")
                    {
                        lineCount++;
                        return RunCommand(filteredCommand[0], filteredCommand[1]);
                    }
                    else if (filteredCommand[1].Length == 0)
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "no argument given");
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    ErrorHandler.ShowEmulatorError("syntax", "unknown command");
                    return false;
                }
            }
            return false;
        }

        public string BytesToString(byte[] command)
        {
            string charMap = MainWindow.GetCharMap();
            string output = "";

            for (int i = 0; i < command.Length; i++)
            {
                output += charMap[command[i]];
            }

            return output;
        }

        private bool ValidCommand(string command)
        {
            command = command.Replace(" ", "");
            for (int i = 0; i < validCommands.Length; i++)
            {
                if (command.IndexOf(validCommands[i]) == 0)
                {
                    return true;
                }
            }

            return false;
        }

        private string[] FilterCommand(string command)
        {
            string commandName = "";
            string parameters = "";

            command = command.Trim();

            for (int i = 0; i < validCommands.Length; i++)
            {
                if (command.IndexOf(validCommands[i]) == 0)
                {
                    commandName = command.Substring(0, validCommands[i].Length);
                    parameters = ReplaceAt(command, 0, validCommands[i].Length, "").Trim();

                    if (parameters.Length > 0)
                    {
                        if (parameters[0] == '(' && parameters[parameters.Length - 1] == ')')
                        {
                            parameters = RemoveBrackets(parameters);
                        }
                        else
                        {
                            ErrorHandler.ShowEmulatorError("syntax", "missing brackets ()");
                            break;
                        }
                    }
                    else
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "missing brackets ()");
                        break;
                    }
                }
            }

            return new string[] { commandName, parameters };
        }

        private void PrintText(string text)
        {
            int[] cursorPos = GLOBAL.cursorPos;
            cursorPos[0] += 8;

            for (int i = 0; i < text.Length; i++)
            {
                string charMap = MainWindow.GetCharMap();
                int charID = charMap.IndexOf(text[i]);
                GLOBAL.VRAM.SetValue((cursorPos[1] / 8) * 40 + ((cursorPos[0] / 8) - 1), charID);
                cursorPos[0] += 8;
            }
        }

        private bool RunCommand(string commandName, string parameters)
        {
            switch (commandName)
            {
                case "ABS":
                    if (parameters.Split(',').Length == parameterCount[0])
                    {
                        int value = 0;
                        try
                        {
                            value = int.Parse(parameters);
                        }
                        catch
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "not integer");
                        }

                        string output = Math.Abs(value).ToString();
                        PrintText(output);
                    }
                    else if (parameters.Split(',').Length > parameterCount[0])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "ASC":
                    if (parameters.Split(',').Length == parameterCount[1])
                    {
                        if (parameters[0] == '"' && parameters[parameters.Length - 1] == '"')
                        {
                            parameters = RemoveQuotes(parameters);
                            if (parameters.Length > 0)
                            {

                                string charMap = MainWindow.GetCharMap();

                                string output = charMap.IndexOf(parameters[0]).ToString();

                                PrintText(output);
                            }
                            else
                            {
                                ErrorHandler.ShowEmulatorError("syntax", "empty string");
                                return false;
                            }
                        }
                        else
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "not string");
                            return false;
                        }
                    }
                    else if (parameters.Split(',').Length > parameterCount[1])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "ATN":
                    if (parameters.Split(',').Length == parameterCount[2])
                    {
                        float value = 0;
                        try
                        {
                            value = float.Parse(parameters);
                        }
                        catch
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "not float");
                            return false;
                        }

                        string output = Math.Atan(value).ToString();
                        PrintText(output);
                    }
                    else if (parameters.Split(',').Length > parameterCount[2])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "CHR":
                    if (parameters.Split(',').Length == parameterCount[3])
                    {
                        int value = 0;
                        try
                        {
                            value = int.Parse(parameters);
                        }
                        catch
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "not integer");
                            return false;
                        }
                        if (value >= 0 && value <= 127)
                        {
                            string output = MainWindow.GetCharMap()[value].ToString();
                            PrintText(output);
                        }
                        else
                        {
                            ErrorHandler.ShowEmulatorError("syntax", "invalid integer");
                            return false;
                        }
                    }
                    else if (parameters.Split(',').Length > parameterCount[3])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "COS":
                    if (parameters.Split(',').Length == parameterCount[4])
                    {
                        float value = 0;
                        try
                        {
                            value = float.Parse(parameters);
                        }
                        catch
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "not float");
                            return false;
                        }

                        string output = Math.Cos(value).ToString();
                        PrintText(output);
                    }
                    else if (parameters.Split(',').Length > parameterCount[4])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "EXP":
                    if (parameters.Split(',').Length == parameterCount[5])
                    {
                        float value = 0;
                        try
                        {
                            value = float.Parse(parameters);
                        }
                        catch
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "not float");
                            return false;
                        }

                        string output = Math.Exp(value).ToString();
                        PrintText(output);
                    }
                    else if (parameters.Split(',').Length > parameterCount[5])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "INT":
                    if (parameters.Split(',').Length == parameterCount[6])
                    {
                        float value = 0;
                        try
                        {
                            value = float.Parse(parameters);
                        }
                        catch
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "not float");
                            return false;
                        }

                        string output = Math.Round(value).ToString();
                        PrintText(output);
                    }
                    else if (parameters.Split(',').Length > parameterCount[6])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "LEFT":
                    if (parameters.Split(',').Length == parameterCount[7])
                    {
                        string[] parametersArray = parameters.Split(',');
                        parametersArray[0] = parametersArray[0].Trim();
                        parametersArray[1] = parametersArray[1].Trim();
                        if (parametersArray[0][0] == '"' && parametersArray[0][parameters[0].ToString().Length - 1] == '"')
                        {
                            parametersArray[0] = RemoveQuotes(parametersArray[0]);
                            try
                            {
                                int index = int.Parse(parametersArray[1]);

                                if (parameters.Length > 0)
                                {
                                    string output = parametersArray[0].Substring(0, index);

                                    PrintText(output);
                                }
                                else
                                {
                                    ErrorHandler.ShowEmulatorError("syntax", "empty string");
                                    return false;
                                }
                            }
                            catch
                            {
                                ErrorHandler.ShowEmulatorError("invalid type", "parameter 2 not int");
                                return false;
                            }
                        }
                        else
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "parameter 1 not string");
                            return false;
                        }
                    }
                    else if (parameters.Split(',').Length > parameterCount[7])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "LEN":
                    if (parameters.Split(',').Length == parameterCount[8])
                    {
                        if (parameters[0] == '"' && parameters[parameters.Length - 1] == '"')
                        {
                            parameters = RemoveQuotes(parameters);
                            if (parameters.Length > 0)
                            {
                                string output = parameters.Length.ToString();

                                PrintText(output);
                            }
                            else
                            {
                                ErrorHandler.ShowEmulatorError("syntax", "empty string");
                                return false;
                            }
                        }
                        else
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "not string");
                            return false;
                        }
                    }
                    else if (parameters.Split(',').Length > parameterCount[8])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "LET":
                    if (parameters.Split(',').Length == parameterCount[9])
                    {
                        string[] parametersArray = parameters.Split(',');
                        parametersArray[0] = parametersArray[0].Trim();
                        parametersArray[1] = parametersArray[1].Trim();
                        if (parametersArray[0][0] == '"' && parametersArray[0][parameters[0].ToString().Length - 1] == '"')
                        {
                            if (parametersArray[1][0] == '"' && parametersArray[1][parameters[1].ToString().Length - 1] == '"')
                            {
                                if (parameters.Length > 0)
                                {
                                    MainWindow.variables[MainWindow.variableCount, 0] = "$" + RemoveQuotes(parametersArray[0]);
                                    MainWindow.variables[MainWindow.variableCount, 1] = RemoveQuotes(parametersArray[1]);
                                }
                                else
                                {
                                    ErrorHandler.ShowEmulatorError("syntax", "empty parameters");
                                    return false;
                                }
                            }
                            else
                            {
                                ErrorHandler.ShowEmulatorError("invalid type", "parameter 2 not string");
                                return false;
                            }
                        }
                        else
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "parameter 1 not string");
                            return false;
                        }
                    }
                    else if (parameters.Split(',').Length > parameterCount[9])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "LOG":
                    if (parameters.Split(',').Length == parameterCount[10])
                    {
                        float value = 0;
                        try
                        {
                            value = float.Parse(parameters);
                        }
                        catch
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "not float");
                            return false;
                        }

                        string output = Math.Log(value).ToString();
                        PrintText(output);
                    }
                    else if (parameters.Split(',').Length > parameterCount[10])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "MID":
                    if (parameters.Split(',').Length == parameterCount[11])
                    {
                        string[] parametersArray = parameters.Split(',');
                        parametersArray[0] = parametersArray[0].Trim();
                        parametersArray[1] = parametersArray[1].Trim();
                        parametersArray[2] = parametersArray[2].Trim();
                        if (parametersArray[0][0] == '"' && parametersArray[0][parameters[0].ToString().Length - 1] == '"')
                        {
                            parametersArray[0] = RemoveQuotes(parametersArray[0]);
                            try
                            {
                                int index1 = int.Parse(parametersArray[1]);

                                try
                                {
                                    int index2 = int.Parse(parametersArray[2]);

                                    if (parameters.Length > 0)
                                    {
                                        string output = parametersArray[0].Substring(index1, index2);

                                        PrintText(output);
                                    }
                                    else
                                    {
                                        ErrorHandler.ShowEmulatorError("syntax", "empty string");
                                        return false;
                                    }
                                }
                                catch
                                {
                                    ErrorHandler.ShowEmulatorError("invalid type", "parameter 3 invalid");
                                    return false;
                                }
                            }
                            catch
                            {
                                ErrorHandler.ShowEmulatorError("invalid type", "parameter 2 invalid");
                                return false;
                            }
                        }
                        else
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "parameter 1 not string");
                            return false;
                        }
                    }
                    else if (parameters.Split(',').Length > parameterCount[11])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "PRINT":
                    if (parameters.Split(',').Length == parameterCount[12])
                    {
                        parameters = ReplaceVariables(parameters);
                        if (parameters[0] == '"' && parameters[parameters.Length - 1] == '"')
                        {
                            parameters = RemoveQuotes(parameters);

                            PrintText(parameters);
                        }
                        else
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "not string");
                            return false;
                        }
                    }
                    else if (parameters.Split(',').Length > parameterCount[12])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    else
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments given");
                        return false;
                    }
                    break;
                case "REM":
                    break;
                case "RIGHT":
                    if (parameters.Split(',').Length == parameterCount[14])
                    {
                        string[] parametersArray = parameters.Split(',');
                        parametersArray[0] = parametersArray[0].Trim();
                        parametersArray[1] = parametersArray[1].Trim();
                        if (parametersArray[0][0] == '"' && parametersArray[0][parameters[0].ToString().Length - 1] == '"')
                        {
                            parametersArray[0] = RemoveQuotes(parametersArray[0]);
                            try
                            {
                                int index = int.Parse(parametersArray[1]);

                                if (parameters.Length > 0)
                                {
                                    string output = parametersArray[0].Substring(Math.Max(0, parametersArray[0].Length - index));

                                    PrintText(output);
                                }
                                else
                                {
                                    ErrorHandler.ShowEmulatorError("syntax", "empty string");
                                    return false;
                                }
                            }
                            catch
                            {
                                ErrorHandler.ShowEmulatorError("invalid type", "parameter 2 not int");
                                return false;
                            }
                        }
                        else
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "parameter 1 not string");
                            return false;
                        }
                    }
                    else if (parameters.Split(',').Length > parameterCount[14])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "RND":
                    if (parameters.Split(',').Length == parameterCount[15])
                    {
                        string[] parametersArray = parameters.Split(',');
                        parametersArray[0] = parametersArray[0].Trim();
                        parametersArray[1] = parametersArray[1].Trim();

                        Random rnd = new Random();

                        int value1 = 0;
                        int value2 = 0;

                        try
                        {
                            value1 = int.Parse(parametersArray[0]);
                            value2 = int.Parse(parametersArray[1]);

                            string output = rnd.Next(value1, value2 + 1).ToString();
                            PrintText(output);
                        }
                        catch
                        {
                            ErrorHandler.ShowEmulatorError("syntax", "invalid range");
                            return false;
                        }
                    }
                    else if (parameters.Split(',').Length > parameterCount[15])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "RUN":
                    RunFromProgramRAM();
                    break;
                case "SGN":
                    if (parameters.Split(',').Length == parameterCount[17])
                    {
                        float value = 0;
                        try
                        {
                            value = float.Parse(parameters);
                        }
                        catch
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "not float");
                            return false;
                        }

                        string output;
                        if (value > 0)
                        {
                            output = "1";
                        }
                        else if (value == 0)
                        {
                            output = "0";
                        }
                        else
                        {
                            output = "-1";
                        }

                        PrintText(output);
                    }
                    else if (parameters.Split(',').Length > parameterCount[17])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "SIN":
                    if (parameters.Split(',').Length == parameterCount[18])
                    {
                        float value = 0;
                        try
                        {
                            value = float.Parse(parameters);
                        }
                        catch
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "not float");
                            return false;
                        }

                        string output = Math.Sin(value).ToString();
                        PrintText(output);
                    }
                    else if (parameters.Split(',').Length > parameterCount[18])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "SQR":
                    if (parameters.Split(',').Length == parameterCount[19])
                    {
                        float value = 0;
                        try
                        {
                            value = float.Parse(parameters);
                        }
                        catch
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "not float");
                            return false;
                        }

                        string output = Math.Sqrt(value).ToString();
                        PrintText(output);
                    }
                    else if (parameters.Split(',').Length > parameterCount[19])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "TAN":
                    if (parameters.Split(',').Length == parameterCount[20])
                    {
                        float value = 0;
                        try
                        {
                            value = float.Parse(parameters);
                        }
                        catch
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "not float");
                            return false;
                        }

                        string output = Math.Tan(value).ToString();
                        PrintText(output);
                    }
                    else if (parameters.Split(',').Length > parameterCount[20])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
                case "WAIT":
                    if (parameters.Split(',').Length == parameterCount[21])
                    {
                        int value = 0;
                        try
                        {
                            value = int.Parse(parameters);
                        }
                        catch
                        {
                            ErrorHandler.ShowEmulatorError("invalid type", "not integer");
                            return false;
                        }

                        System.Threading.Thread.Sleep(value);
                    }
                    else if (parameters.Split(',').Length > parameterCount[21])
                    {
                        ErrorHandler.ShowEmulatorError("syntax", "too many arguments");
                    }
                    break;
            }

            return true;
        }

        private string ReplaceAt(string input, int index, int length, string replace)
        {
            return input.Remove(index, Math.Min(length, input.Length - index)).Insert(index, replace);
        }

        private string RemoveBrackets(string input)
        {
            int firstIndex = input.IndexOf("(");
            int lastIndex = input.LastIndexOf(")") - 1;


            return ReplaceAt(ReplaceAt(input, firstIndex, 1, ""), lastIndex, 1, "");
        }

        private string RemoveQuotes(string input)
        {
            int firstIndex = input.IndexOf('"');
            int lastIndex = input.LastIndexOf('"') - 1;


            return ReplaceAt(ReplaceAt(input, firstIndex, 1, ""), lastIndex, 1, "");
        }

        private string ReplaceVariables(string input)
        {
            string[,] variables = MainWindow.variables;
            int count = 0;

            for (int i = 0; i < variables.GetLength(0); i++)
            {
                if (variables[i, 0] != null)
                {
                    input = input.Replace(variables[i, 0], variables[i, 1]);
                    count++;
                }
            }
            if (count > 0)
            {
                return '"' + input + '"';
            }
            else
            {
                return input;
            }
        }

        private void RunFromProgramRAM()
        {
            lineCount = 0;
            GLOBAL.ProgramRAM.SetCurrentIndex(0);

            int[] loadedContents = GLOBAL.ProgramRAM.GetContents();
            int[,] programContents = new int[loadedContents.Length / 40, 40];
            byte[,] finalProgram = new byte[loadedContents.Length / 40, 40];

            int count = 0;
            for (int i = 0; i < loadedContents.Length; i++)
            {
                if (loadedContents[i] == 0)
                {
                    count++;
                }
            }
            if (count == loadedContents.Length)
            {
                ErrorHandler.ShowEmulatorError("system", "empty program");
                return;
            }

            for (int i = 0; i < loadedContents.Length; i++)
            {
                int row = i / 40;

                if (row < loadedContents.Length / 40)
                {
                    programContents[row, i % 40] = loadedContents[i];
                }
            }

            for (int i = 0; i < programContents.GetLength(0); i++)
            {
                for (int j = 0; j < programContents.GetLength(1); j++)
                {
                    finalProgram[i, j] = (byte)programContents[i, j];
                }
            }

            int[] pos = GLOBAL.cursorPos;
            for (int i = 0; i < finalProgram.GetLength(0); i++)
            {
                byte[] singleCommand = new byte[finalProgram.GetLength(1)];
                for (int j = 0; j < singleCommand.Length; j++)
                {
                    singleCommand[j] = finalProgram[i, j];
                }

                if (BytesToString(singleCommand).Split(' ').Length - 1 != 40 && BytesToString(singleCommand).Split('@').Length - 1 != 40)
                {
                    lineCount++;
                    pos[0] = 0;
                    pos[1] += 8;
                    GLOBAL.cursorPos = pos;
                    ExecuteCommand(singleCommand);
                }
            }

            pos[0] = 0;
            pos[1] -= 8;
            GLOBAL.cursorPos = pos;
        }

        private string RemoveLineNumber(string input)
        {
            string output = "";
            bool carryOn = true;
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsDigit(input[i]) && carryOn)
                {
                    output += " ";
                }
                else
                {
                    output += input[i];
                    carryOn = false;
                }
            }

            return output;
        }
    }
}
