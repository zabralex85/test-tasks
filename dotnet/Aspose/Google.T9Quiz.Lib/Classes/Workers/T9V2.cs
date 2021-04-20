using Google.T9Quiz.Lib.Classes.Common;
using System.Text;

namespace Google.T9Quiz.Lib.Classes.Workers
{
    public sealed class T9V2 : IWrapper
    {
        public string GetNumberCodes(string input)
        {
            StringBuilder result = new StringBuilder();

            short prevIdexGroup = -2;

            for (int i = 0; i < input.Length; i++)
            {
                char charFromData = input[i];
                int index = charFromData;
                if((index < 97 || index > 122) && (index != 32)) continue;

                string val = "";
                short group = GetGroup(index, out val);

                if(group != -1 && !string.IsNullOrEmpty(val))
                {
                    if(prevIdexGroup != -2)
                    {
                        if(prevIdexGroup == group)
                        {
                            result.Append(" ");
                        }
                    }

                    result.Append(val);
                }

                prevIdexGroup = group;
            }

            return result.ToString();
        }

        private short GetGroup(int index, out string val)
        {
            val = "";
      
            if (index.IsWithin(112, 115))
            {
                switch (index)
                {
                    case 112: val = "7"; break;
                    case 113: val = "77"; break;
                    case 114: val = "777"; break;
                    case 115: val = "7777"; break;
                }
                return 6;
            }
            else if (index.IsWithin(119, 122))
            {
                switch (index)
                {
                    case 119: val = "9"; break;
                    case 120: val = "99"; break;
                    case 121: val = "999"; break;
                    case 122: val = "9999"; break;
                }
                return 8;
            }
            else if (index.IsWithin(97, 99))
            {
                switch (index)
                {
                    case 97: val = "2"; break;
                    case 98: val = "22"; break;
                    case 99: val = "222"; break;
                }
                return 1;
            }
            else if (index.IsWithin(100, 102))
            {
                switch (index)
                {
                    case 100: val = "3"; break;
                    case 101: val = "33"; break;
                    case 102: val = "333"; break;
                }
                return 2;
            }
            else if (index.IsWithin(103, 105))
            {
                switch (index)
                {
                    case 103: val = "4"; break;
                    case 104: val = "44"; break;
                    case 105: val = "444"; break;
                }
                return 3;
            }
            else if (index.IsWithin(106, 108))
            {
                switch (index)
                {
                    case 106: val = "5"; break;
                    case 107: val = "55"; break;
                    case 108: val = "555"; break;
                }
                return 4;
            }
            else if (index.IsWithin(109, 111))
            {
                switch (index)
                {
                    case 109: val = "6"; break;
                    case 110: val = "66"; break;
                    case 111: val = "666"; break;
                }
                return 5;
            }
            
            else if (index.IsWithin(116, 118))
            {
                switch (index)
                {
                    case 116: val = "8"; break;
                    case 117: val = "88"; break;
                    case 118: val = "888"; break;
                }
                return 7;
            }
            else if (index == 32)
            {
                val = "0";
                return 0;
            }

            return -1;
        }
    }
}
