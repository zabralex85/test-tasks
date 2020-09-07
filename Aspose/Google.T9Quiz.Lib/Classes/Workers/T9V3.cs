using System;
using System.Collections.Generic;
using System.Text;

namespace Google.T9Quiz.Lib.Classes.Workers
{
    public sealed class T9V3 : IWrapper
    {
        private Dictionary<char, ValueTuple<short, string>> _numbers = new Dictionary<char, ValueTuple<short, string>>
        {
            { ' ',  new ValueTuple<short, string>(0, "0")},
            { 'a',  new ValueTuple<short, string>(1, "2")},
            { 'b',  new ValueTuple<short, string>(1, "22")},
            { 'c',  new ValueTuple<short, string>(1, "222")},
            { 'd',  new ValueTuple<short, string>(2, "3")},
            { 'e',  new ValueTuple<short, string>(2, "33")},
            { 'f',  new ValueTuple<short, string>(2, "333")},
            { 'g',  new ValueTuple<short, string>(3, "4")},
            { 'h',  new ValueTuple<short, string>(3, "44")},
            { 'i',  new ValueTuple<short, string>(3, "444")},
            { 'j',  new ValueTuple<short, string>(4, "5")},
            { 'k',  new ValueTuple<short, string>(4, "55")},
            { 'l',  new ValueTuple<short, string>(4, "555")},
            { 'm',  new ValueTuple<short, string>(5, "6")},
            { 'n',  new ValueTuple<short, string>(5, "66")},
            { 'o',  new ValueTuple<short, string>(5, "666")},
            { 'p',  new ValueTuple<short, string>(6, "7")},
            { 'q',  new ValueTuple<short, string>(6, "77")},
            { 'r',  new ValueTuple<short, string>(6, "777")},
            { 's',  new ValueTuple<short, string>(6, "7777")},
            { 't',  new ValueTuple<short, string>(7, "8")},
            { 'u',  new ValueTuple<short, string>(7, "88")},
            { 'v',  new ValueTuple<short, string>(7, "888")},
            { 'w',  new ValueTuple<short, string>(8, "9")},
            { 'x',  new ValueTuple<short, string>(8, "99")},
            { 'y',  new ValueTuple<short, string>(8, "999")},
            { 'z',  new ValueTuple<short, string>(8, "9999")},
        };        

        public string GetNumberCodes(string input)
        {
            StringBuilder result = new StringBuilder();
            int previosGroup = -1;
            for (int i = 0; i < input.Length; i++)
            {
                ValueTuple<short, string> mapForCode;
                if (!_numbers.TryGetValue(input[i], out mapForCode)) continue;

                if(previosGroup == mapForCode.Item1)
                {
                    result.Append(" ");
                }
 
                result.Append(mapForCode.Item2);
                previosGroup = mapForCode.Item1;
            }

            return result.ToString();
        }
    }
}
