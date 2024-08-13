using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace String_Parser
{
    public class DataMessage
    {
        public string Val1 { get; set; } = string.Empty;
        public string Val2 { get; set; } = string.Empty;
        public string Val3 { get; set; } = string.Empty;
        public string Val4 { get; set; } = string.Empty;
        public decimal DecVal { get; set; }
        public bool IsEnabled { get; set; }

        public bool Enabled
        {
            get { return Enabled; }
            set { IsEnabled = value; }
        }
        public bool Disabled
        {
            get { return Disabled; }
            set { IsEnabled = !value; }
        }
    }
}
