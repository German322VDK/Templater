using System.Collections.Generic;

namespace Templater.Infrastructure.Methods
{
    public static class WordMethods
    {
        //ищет в тексте все подстроки типа "from...to"
        public static ICollection<string> SearchFromTo(string s, char from, char to)
        {
            var strs = new List<string>();

            List<char> str = new List<char>();

            bool flag = false;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == from)
                {
                    str = new List<char>();

                    flag = true;
                }
                if (flag)
                {
                    str.Add(s[i]);
                }

                if (s[i] == to)
                {
                    flag = false;

                    strs.Add(new string(str.ToArray()));
                }
            }

            return strs;
        }
    }
}
