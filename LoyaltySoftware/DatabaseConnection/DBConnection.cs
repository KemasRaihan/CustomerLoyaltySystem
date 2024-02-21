using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoyaltySoftware.Pages.Shared
{
    public class DBConnection
    {
        public string DatabaseString()
        {
            string DbString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=G:\My Drive\Sheffield_Hallam_University\First Year\Software Projects\CustomerLoyaltySystem\LoyaltySoftware\Data\LoyaltyDB.mdf";
            return DbString;
        }
    }
}
