using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class ConnectDB
    {//please change the db_file's path
        public static string GetDBPath()
        {
            return "C:/Users/a0979/source/repos/WebApplication4/WebApplication4/App_Data/bookdata.bin";
        }
    }
}
