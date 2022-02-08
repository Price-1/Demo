using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Demo.Metadata
{
    public class CfgReader
    {
        public static JObject RetrieveMetadataObject(string fileName)
        {
            string cfgPath = String.Format("D:/RageGTAW/server-files/dotnet/resources/Demo/Metadata/{0}.json", fileName);

            using (StreamReader r = new StreamReader(cfgPath))
            {
                string json = r.ReadToEnd();
                JObject retrievedObject = JObject.Parse(json);

                return retrievedObject;
            }
        }
    }
}
