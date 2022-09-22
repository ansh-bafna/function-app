using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FounderCommunityAzureFunction
{
    public class CXOModel
    {
        public string AccountId { get; set; }
        public string FullName { get; set; }

        public string Company { get; set; }
        public string profilePic { get; set; }
        public string coverPic { get; set; }
        public string education { get; set; }
        public DateTime FHDoj { get; set; }
        public int yearsOfExperience { get; set; }
        public int HoursContributed { get; set; }
        public int numStartupsHelped { get; set; }
        public int investmentDone { get; set; }
        public int responseTimeInMin { get; set; }
        public string email { get; set; }
        public string linkedin { get; set; }
        public string cxoDescription { get; set; }
        public string Country { get; set; }
        
    }
}
