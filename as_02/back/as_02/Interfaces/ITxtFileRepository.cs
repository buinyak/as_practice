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
        TxtFile GetTxtFilebyName(TxtFile txtFile);
        dynamic GetAllTxtFiles();
        void UpdateTxtFile(TxtFile txtFile);
    }
}
