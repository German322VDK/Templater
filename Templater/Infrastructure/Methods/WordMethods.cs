using DocumentFormat.OpenXml.Packaging;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

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

        public static string GetTextFromWord(string filePath)
        {
            using (var word = WordprocessingDocument.Open(filePath, false))
            {
                return word.MainDocumentPart.Document.Body.InnerText;
            }
        }

        //более универсальный способ открытия любого файла дефолтным приложением 
        public static void OpenWord(string filePath)
        {
            var path = Path.GetFullPath(filePath);
            var proc = new Process();
            proc.StartInfo.FileName = path;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }
    }
}
