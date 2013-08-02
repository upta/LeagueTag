using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeagueTag.Models;

namespace LeagueTag.Handlers
{
    public interface IConfigHandler
    {
        Config Config { get; set; }
        IEnumerable<string> Commands { get; set; }
        TimeSpan DurationForCommand( string command );
        void Load();
        void Save();
    }
}
