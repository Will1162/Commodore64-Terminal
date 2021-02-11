namespace Commodore_64_Emulator
{
    public class ProgramRAM : RAM
    {
        private int currentIndex;

        public ProgramRAM(int size) : base(size)
        {
            contents = new int[size];
        }

        public void SaveLineToRAM()
        {
            int[] cursorPos = GLOBAL.cursorPos;
            cursorPos[0] = 0;

            if (currentIndex + 40 > contents.Length)
            {
                int[] temp = new int[contents.Length];

                for (int i = 0; i < contents.Length; i++)
                {
                    temp[i] = contents[i];
                }

                contents = new int[contents.Length + 512];

                for (int i = 0; i < temp.Length; i++)
                {
                    contents[i] = temp[i];
                }
            }

            for (int i = 0; i < 40; i++)
            {
                contents[currentIndex] = GLOBAL.VRAM.GetValue(((cursorPos[1] / 8) * 40) + (cursorPos[0] / 8));
                cursorPos[0] += 8;
                currentIndex++;
            }
        }

        public byte[] GetLatestLine()
        {
            byte[] output = new byte[40];
            int[] cursorPos = GLOBAL.cursorPos;
            cursorPos[0] = 0;

            for (int i = 0; i < 40; i++)
            {
                output[i] = (byte)GLOBAL.VRAM.GetValue(((cursorPos[1] / 8) * 40) + (cursorPos[0] / 8));
                cursorPos[0] += 8;
            }

            return output;
        }

        public string CheckCommandType()
        {
            int[] cursorPos = GLOBAL.cursorPos;
            cursorPos[0] = 0;

            int value = GLOBAL.VRAM.GetValue(((cursorPos[1] / 8) * 40) + (cursorPos[0] / 8));
            for (int i = 0; i < 40; i++)
            {
                if (value == 32)
                {
                    cursorPos[0] += 8;
                    value = GLOBAL.VRAM.GetValue(((cursorPos[1] / 8) * 40) + (cursorPos[0] / 8));
                    continue;
                }
                else if (value >= 1 && value <= 26)
                {
                    return "text";
                }
                else if ((value >= 27 && value <= 47) || (value >= 58 && value <= 63))
                {
                    return "special";
                }
                else if (value >= 48 && value <= 57)
                {
                    return "number";
                }
                else if (value >= 64 && value <= 127)
                {
                    return "graphics";
                }
            }

            return "empty";
        }

        public int[] GetUsage()
        {
            int total = contents.Length;
            int free = total - currentIndex;
            int used = total - free;

            return new int[] { total, free, used };
        }

        public int GetCurrentIndex()
        {
            return currentIndex;
        }

        public void SetCurrentIndex(int newIndex)
        {
            currentIndex = newIndex;
        }

        public void ClearRAM()
        {
            contents = new int[512];
        }
    }
}
