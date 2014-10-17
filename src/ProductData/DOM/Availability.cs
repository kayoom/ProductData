using System;

namespace ProductData.DOM
{
    public class Availability
    {
        public Boolean Active { get; set; }
        public Boolean Replaceable { get; set; }
        public uint LeadTime { get; set; }
        public uint ReplaceTime { get; set; }
        public uint InStock { get; set; }
    }
}