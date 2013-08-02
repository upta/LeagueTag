using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeagueTag.Models
{
    public class Config
    {
        public List<string> Verbs { get; set; }
        public Dictionary<string, TimeSpan> Durations { get; set; }
    }
}
