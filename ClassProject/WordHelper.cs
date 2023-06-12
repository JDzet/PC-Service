using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;

namespace PC_Service.ClassProject
{
   
    internal class WordHelper
    {
        private FileInfo _fileInfo;
        public WordHelper(string fileName)
        {
            if (File.Exists(fileName))
            {
                _fileInfo = new FileInfo(fileName);
            }
            else
            {
                throw new ArgumentException("Файл отсутсвует");
            }
        }

        internal bool Process(Dictionary<string, object> items)
        {
            Word.Application app = null;
            try
            {
                app = new Word.Application();
                Object file = _fileInfo.FullName;
                Object missing = Type.Missing;

                app.Documents.Open(file);

                foreach (var item in items) 
                {
                    Word.Find find = app.Selection.Find;
                    find.Text = item.Key;
                    if (item.Value != null)
                    {
                        find.Replacement.Text = item.Value.ToString();
                    }
                    else 
                    {
                        find.Replacement.Text = "";
                    }

                    Object wrap = Word.WdFindWrap.wdFindContinue;
                    Object replace = Word.WdReplace.wdReplaceAll;

                    find.Execute(FindText: Type.Missing,
                        MatchCase: false,
                        MatchWholeWord: false,
                        MatchWildcards: false,
                        MatchSoundsLike: missing,
                        MatchAllWordForms: false,
                        Forward: false,
                        Wrap: wrap,
                        Format: false,
                        ReplaceWith: missing, Replace: replace);
                }

                Object newFileName = Path.Combine(_fileInfo.DirectoryName, DateTime.Now.ToString(" ddMMyyyy HHmmss ") + _fileInfo.Name);
                string filePath = newFileName.ToString();

                app.ActiveDocument.SaveAs2(filePath);
                app.ActiveDocument.Close();


                byte[] fileBytes = File.ReadAllBytes(filePath);
                

                using (DataDB.entities = new EntitiesMain())
                {
                    Orders new_order = DataDB.entities.Orders.OrderByDescending(x => x.OrderID).FirstOrDefault();
                    new_order.FileData = fileBytes;

                    DataDB.entities.SaveChanges();
                }
                System.Diagnostics.Process.Start(filePath);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                if (app != null)
                    app.Quit();

            }
            return false;
        }


    }
}
