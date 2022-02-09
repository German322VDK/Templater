using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Word = Microsoft.Office.Interop.Word;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateFileName">path to template</param>
        /// <param name="docFileName">path to new doc</param>
        /// <param name="items">Marks</param>
        /// <returns>Result</returns>
        public static bool CreateDoc(string templateFileName, string docFileName, Dictionary<string, string> items)
        {
            FileInfo fileInfo;

            if (File.Exists(templateFileName))
            {
                fileInfo = new FileInfo(templateFileName);
            }
            else
            {
                throw new ArgumentException("File not found");
            }

            Word.Application app = null;

            try
            {
                app = new Word.Application();

                var file = fileInfo.FullName;

                var missing = Type.Missing;

                app.Documents.Open(file);

                foreach (var item in items)
                {
                    var find = app.Selection.Find;

                    find.Text = item.Key;

                    find.Replacement.Text = item.Value;

                    var wrap = Word.WdFindWrap.wdFindContinue;
                    var replace = Word.WdReplace.wdReplaceAll;

                    var result = find.Execute(
                        FindText: Type.Missing,
                        MatchCase: false,
                        MatchWholeWord: false,
                        MatchWildcards: false,
                        MatchSoundsLike: missing,
                        MatchAllWordForms: false,
                        Forward: true,
                        Wrap: wrap,
                        Format: false,
                        ReplaceWith: missing,
                        Replace: replace
                        );

                }

                app.ActiveDocument.SaveAs2(docFileName);

                app.Quit();

                return true;

            }
            catch (Exception e)
            {
                //Console.WriteLine(e.Message);
            }
            finally
            {
                if (app != null)
                {
                    app.Quit();
                }

            }

            return false;
        }
    }
}
