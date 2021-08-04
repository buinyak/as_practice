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
        public TxtFile GetTxtFilebyName(TxtFile txtFile)
        {
            if(File.Exists(@"txtfiles\"+txtFile.Type + "\\"+ txtFile.Name))
            {
                string text;
                using (StreamReader reader = new StreamReader(@"txtfiles\" + txtFile.Type + "\\" + txtFile.Name)){
                    text = reader.ReadLine();
                }

                txtFile.Text = text;
                return txtFile;
            }
            else
            {
                return null;
            }
            
            
        }
        public dynamic GetAllTxtFiles()
        {
            Dictionary<string,List<TxtFile>> txtFiles = new Dictionary<string,List<TxtFile>>();
            string[] dirs = Directory.GetDirectories(@"txtfiles\");
            foreach(string dir in dirs)
            {
                txtFiles.Add(dir.Substring(9), new List<TxtFile>());
                foreach (string file in Directory.GetFiles(@"txtfiles\"+dir.Substring(9)))
                {
                    txtFiles[dir.Substring(9)].Add(new TxtFile { Name = file.Substring(9) });
                }
            }
            return txtFiles;
        }
        public void CreateTxtFile(TxtFile txtFile)
        {
            using (StreamWriter streamWriter = new StreamWriter(@"txtfiles\"+txtFile.Type + "\\" + txtFile.Name + ".txt"))
            {
                streamWriter.Write(txtFile.Text);
            }

        }

        public void UpdateTxtFile(TxtFile txtFile)
        {
            if (txtFile.Name.Substring(txtFile.Name.Length - 4) != ".txt") { txtFile.Name += ".txt"; }
            if (GetTxtFilebyName(txtFile) != null)
            {
                using (StreamWriter streamWriter = new StreamWriter(@"txtfiles\" + txtFile.Type + "\\" + txtFile.Name))
                {
                    streamWriter.Write(txtFile.Text);
                }
            }
        }
    }
}
