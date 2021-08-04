using as_02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace as_02.Interfaces
{
    public interface ITxtFileService
    {
        void CreateTxtFile(TxtFile txtFile);
        TxtFile GetTxtFilebyName(string name);
        List<TxtFile> GetAllTxtFiles();
        void UpdateTxtFile(TxtFile txtFile);
    }
}
