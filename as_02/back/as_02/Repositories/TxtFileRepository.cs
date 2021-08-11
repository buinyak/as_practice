using as_02.Interfaces;
using as_02.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace as_02.Services
{
    public class TxtFileRepository : ITxtFileRepository
    {
        readonly IConfiguration _configuration;
        public TxtFileRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public TxtFile GetTxtFilebyName(TxtFile txtFile)
        {
            if(File.Exists(_configuration["TxtFilesPaths:txtFiles"] + txtFile.Type + "\\"+ txtFile.Name))
            {
                string text;
                using (StreamReader reader = new StreamReader(_configuration["TxtFilesPaths:txtFiles"] + txtFile.Type + "\\" + txtFile.Name)){
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
            string[] dirs = Directory.GetDirectories(_configuration["TxtFilesPaths:txtFiles"]);
            foreach(string dir in dirs)
            {
                string dirName = dir.Substring(9);
                txtFiles.Add(dirName, new List<TxtFile>());
                foreach (string file in Directory.GetFiles(_configuration["TxtFilesPaths:txtFiles"] + dirName))
                {
                    txtFiles[dirName].Add(GetTxtFilebyName(new TxtFile { Name = file.Substring(dir.Length+1),Type = dirName })) ;
                }
            }
            return txtFiles;
        }
        public void CreateTxtFile(TxtFile txtFile)
        {
            using (StreamWriter streamWriter = new StreamWriter(_configuration["TxtFilesPaths:txtFiles"] + txtFile.Type + "\\" + txtFile.Name + ".txt"))
            {
                streamWriter.Write(txtFile.Text);
            }

        }

        public void UpdateTxtFile(TxtFile txtFile)
        {
            if (txtFile.Name.Substring(txtFile.Name.Length - 4) != ".txt") { txtFile.Name += ".txt"; }
            {
                using (StreamWriter streamWriter = new StreamWriter(_configuration["TxtFilesPaths:txtFiles"] + txtFile.Type + "\\" + txtFile.Name))
                {
                    streamWriter.Write(txtFile.Text);
                }
            }
        }
    }
}
