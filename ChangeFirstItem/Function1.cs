using System;
using System.Net;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace ChangeFirstItem
{
    public class Function1
    {
        [FunctionName("ChangeFirst")]
        public void Run([TimerTrigger("0 0 */6 * * *")] TimerInfo myTimer, ILogger log)
        {
            WebClient wc = new WebClient();
            var res = wc.DownloadString("Placeholder");
        }
    }
}
