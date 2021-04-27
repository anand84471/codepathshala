using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StudentDashboard.Utilities
{
    public class TinyUrlService
    {
        public TinyUrlService()
        {
        }
        public async Task<string> GetTinyUrl(string url)
        {
            string tinyUrl = "";
            try
            {
                System.Uri address = new System.Uri("https://tinyurl.com/api-create.php?url=" + url);
                System.Net.WebClient client = new System.Net.WebClient();
                tinyUrl = await client.DownloadStringTaskAsync(address);
            }
            catch(Exception Ex)
            {
                throw new Exception();
            }
            return tinyUrl;
        }
    }
}