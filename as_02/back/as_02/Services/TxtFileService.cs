using as_02.Interfaces;
using as_02.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace as_02.Services
{
    public class TxtFileService : ITxtFileService
    {
        public TxtFile GetTxtFilebyName(string name)
        {
            if(File.Exists(@"txtfiles\" + name))
            {
                string text;
                using (StreamReader reader = new StreamReader(@"txtfiles\" + name)){
                    text = reader.ReadLine();
                }
                TxtFile txtFile = new TxtFile();
                txtFile.Name = name;
                txtFile.Text = text;
                return txtFile;
            }
            else
            {
                return null;
            }
            
            
        }

        public List<TxtFile> GetAllTxtFiles()
        {
            string[] allFilesPaths = Directory.GetFiles(@"txtfiles\");
            List<TxtFile> allFiles = new List<TxtFile>();
            foreach (string path in allFilesPaths)
            {
                allFiles.Add(GetTxtFilebyName(path.Substring(9)));
            }
            return allFiles;
        }
        public void CreateTxtFile(TxtFile txtFile)
        {
            using (StreamWriter streamWriter = new StreamWriter(@"txtfiles\" + txtFile.Name + ".txt"))
            {
                streamWriter.Write(txtFile.Text);
            }

        }

        public void UpdateTxtFile(TxtFile txtFile)
        {
            if (txtFile.Name.Substring(txtFile.Name.Length - 4) != ".txt") { txtFile.Name += ".txt"; }
            if (GetTxtFilebyName(txtFile.Name) != null)
            {
                using (StreamWriter streamWriter = new StreamWriter(@"txtfiles\" + txtFile.Name))
                {
                    streamWriter.Write(txtFile.Text);
                }
            }
        }
    }
}
