using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Google.T9Quiz.Lib.Classes.Workers
{
    public sealed class T9 : IWrapper
    {
        private List<MapNumber[]> _numbers = new List<MapNumber[]>();

        public T9()
        {
            FillMap();
        }

        public string GetNumberCodes(string input)
        {
            if (_numbers.Count == 0)
                throw new ArgumentException("inconsistent map");

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < input.Length; i++)
            {
                MapNumber[] mapForCode = GetMapGroupForChar(input[i]);
                if (mapForCode == null) continue;

                string code = "";
                for (int z = 0; z < mapForCode.Length; z++)
                {
                    if (mapForCode[z].Char == input[i])
                    {
                        code = mapForCode[z].Number;
                    }

                    if (i >= 1)
                    {
                        if (mapForCode[z].Char == input[i - 1])
                        {
                            result.Append(" ");
                        }
                    }
                }

                result.Append(code);
            }

            return result.ToString();
        }

        private void FillMap()
        {
            _numbers.Add(new MapNumber[1] {
                new MapNumber() { Char = ' ', Number = "0" }
            });
            _numbers.Add(new MapNumber[3] {
                new MapNumber() { Char = 'a', Number = "2" },
                new MapNumber() { Char = 'b', Number = "22" },
                new MapNumber() { Char = 'c', Number = "222" }
            });
            _numbers.Add(new MapNumber[3] {
                new MapNumber() { Char = 'd', Number = "3" },
                new MapNumber() { Char = 'e', Number = "33" },
                new MapNumber() { Char = 'f', Number = "333" }
            });
            _numbers.Add(new MapNumber[3] {
                new MapNumber() { Char = 'g', Number = "4" },
                new MapNumber() { Char = 'h', Number = "44" },
                new MapNumber() { Char = 'i', Number = "444" }
            });
            _numbers.Add(new MapNumber[3] {
                new MapNumber() { Char = 'j', Number = "5" },
                new MapNumber() { Char = 'k', Number = "55" },
                new MapNumber() { Char = 'l', Number = "555" }
            });
            _numbers.Add(new MapNumber[3] {
                new MapNumber() { Char = 'm', Number = "6" },
                new MapNumber() { Char = 'n', Number = "66" },
                new MapNumber() { Char = 'o', Number = "666" }
            });
            _numbers.Add(new MapNumber[4] {
                new MapNumber() { Char = 'p', Number = "7" },
                new MapNumber() { Char = 'q', Number = "77" },
                new MapNumber() { Char = 'r', Number = "777" },
                new MapNumber() { Char = 's', Number = "7777" }
            });
            _numbers.Add(new MapNumber[3] {
                new MapNumber() { Char = 't', Number = "8" },
                new MapNumber() { Char = 'u', Number = "88" },
                new MapNumber() { Char = 'v', Number = "888" }
            });
            _numbers.Add(new MapNumber[4] {
                new MapNumber() { Char = 'w', Number = "9" },
                new MapNumber() { Char = 'x', Number = "99" },
                new MapNumber() { Char = 'y', Number = "999" },
                new MapNumber() { Char = 'z', Number = "9999" }
            });
        }

        private MapNumber[] GetMapGroupForChar(char v)
        {
            return _numbers.FirstOrDefault(n => n.Any(n2 => n2.Char == v));
        }
    }
}
