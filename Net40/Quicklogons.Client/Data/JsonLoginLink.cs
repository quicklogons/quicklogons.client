using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Quicklogons.Client.Data
{
    [DataContract]
    public class JsonLoginLink : ILoginLink
    {
        [DataMember]
        public string ProviderName { get; set; }

        [DataMember]
        public string Url { get; set; }
    }
}
