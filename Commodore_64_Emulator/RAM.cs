namespace Commodore_64_Emulator
{
    public class RAM
    {
        protected int[] contents;

        public RAM(int size) // declaration requires size for memory
        {
            contents = new int[size];
        }

        public int[] GetContents() // returns entire contents
        {
            return contents;
        }

        public int GetValue(int index) // returns single value in memory
        {
            return contents[index];
        }

        public void SetValue(int index, int value) // sets single value in memory
        {
            if (index >= 0 && index < contents.Length)
            {
                contents[index] = value;
            }
        }

        public void FillContents(int value) // fills the contents with given value
        {
            for (int i = 0; i < contents.Length; i++)
            {
                contents[i] = value;
            }
        }
    }
}
