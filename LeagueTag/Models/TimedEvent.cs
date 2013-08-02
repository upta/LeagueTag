using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Caliburn.Micro;

namespace LeagueTag.Models
{
    public class TimedEvent : PropertyChangedBase
    {
        public string Command { get; set; }
        public TimeSpan TimeRemaining
        {
            get
            {
                var time = ( this.startTime - DateTime.Now ).Add( this.duration );

                if ( time.TotalMilliseconds < 0 )
                {
                    return TimeSpan.FromTicks( 0 );
                }

                return time;
            }
        }

        private readonly TimeSpan duration;
        private DateTime startTime;

        public TimedEvent( string command, TimeSpan duration )
        {
            this.Command = command;
            this.duration = duration;
            this.startTime = DateTime.Now;
        }
    }
}
