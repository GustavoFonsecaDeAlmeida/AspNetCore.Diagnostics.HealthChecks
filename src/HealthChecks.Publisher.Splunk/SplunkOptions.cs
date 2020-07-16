using System;
using System.Collections.Generic;
using System.Text;

namespace HealthChecks.Publisher.Splunk
{
    public class SplunkOptions
    {
        public string Endpoint { get; set; }
        public string Token { get; set; }
    }
}
