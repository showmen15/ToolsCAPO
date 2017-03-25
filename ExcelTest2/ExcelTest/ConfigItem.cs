using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelTest
{
    public class ConfigItem
    {
        public int ConfigID { get; set; }
           public string Name {get;set;}

           public ConfigItem(int iConfigID, string sName)
           {
               ConfigID = iConfigID;
               Name = sName;
           }
    }
}
