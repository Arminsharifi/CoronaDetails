using System;
using System.Collections.Generic;

namespace CoronaDetails.Business
{
    public class Root
    {
        public string title { get; set; }
        public DateTime last_updated { get; set; }
        public List<Entry> entries { get; set; }
    }
}
