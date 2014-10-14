using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GhostRunner.Utils
{
    public class JSONHelper
    {
        public static String BuildStatusMessage(String status, String message)
        {
            Dictionary<String, String> results = new Dictionary<String, String>();
            results.Add("status", status);
            results.Add("message", message);

            return JsonConvert.SerializeObject(results, new KeyValuePairConverter());
        }
    }
}
