using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeagueTag.Models;

namespace LeagueTag.Handlers
{
    abstract class BaseConfigHandler : IConfigHandler
    {
        public IEnumerable<string> Commands
        {
            get
            {
                return this.Config.Durations.Keys.ToList();
            }
            set
            {
                throw new InvalidOperationException( "Can't set Commands" );
            }
        }
        public Config Config { get; set; }

        public TimeSpan DurationForCommand( string command )
        {
            if ( !this.Config.Durations.ContainsKey( command ) )
            {
                throw new ArgumentException( "Command does not exist: " + command );
            }

            return this.Config.Durations[ command ];
        }

        abstract public void Load();
        abstract public void Save();
    }
}
