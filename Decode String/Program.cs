//https://leetcode.com/problems/decode-string/
//Accepted
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decode_String
{
    class Program
    {
        static void Main(string[] args)
        {
            string encodedStr = "3[a]2[bc]";
            Console.WriteLine(new Program().DecodeString(encodedStr));
            Console.Read();
        }

        public string DecodeString(string s)
        {
            char[] _s = s.ToCharArray();

            int lastLeftBrk = 0, firstRightBrk = 0, num = 0, leftBit = 0, rightBit = 0;

            string temp = "", target="";

            //int loop = 0;

            while (s.Contains("["))
            {
                // find the last left bracket, first right bracket, and number of repeats.
                // the index of right bracket must bigger than the index of left bracket.
                lastLeftBrk = _s.Select((c, i) => new { i, c }).Where(x => x.c == '[').Select(x => x.i).ToArray().Last();
                firstRightBrk = _s.Select((c, i) => new { i, c }).Where(x => x.c == ']' && x.i > lastLeftBrk).Select(x => x.i).ToArray().First();
                
                rightBit = lastLeftBrk - 1;
                leftBit = rightBit;
                for (int i = rightBit - 1; i > -1; i--)
                {
                    if (_s[i] >= '0' && _s[i] <= '9')
                    {
                        leftBit = i;
                    }
                    else
                    {
                        break;
                    }
                }

                num = Convert.ToInt32(s.Substring(leftBit, (rightBit - leftBit) + 1));

                // decode this block.
                for (int i = 0; i < num; i++)
                {
                    temp += s.Substring(lastLeftBrk + 1, (firstRightBrk - lastLeftBrk) - 1);
                }

                // build the target to replace.
                target = s.Substring(leftBit, ((firstRightBrk - lastLeftBrk) + 1) + s.Substring(leftBit, (rightBit - leftBit) + 1).Length);
                
                // replace the string and make a new array.
                s = s.Replace(target, temp);
                _s = s.ToCharArray();

                Console.WriteLine("temp={0}, num={1}, target={2}", temp, num, target);
                Console.WriteLine("s={0}\n", s);

                // reset temp
                temp = "";
            }

            return s;
        }
    }
}
