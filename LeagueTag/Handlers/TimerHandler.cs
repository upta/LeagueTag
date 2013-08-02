using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Threading;
using Caliburn.Micro;
using LeagueTag.Models;

namespace LeagueTag.Handlers
{
    public class TimerHandler : PropertyChangedBase, ITimerHandler
    {        
        public ObservableCollection<TimedEvent> TimedEvents { get; set; }

        public event EventHandler TimedEventCompleted;

        private readonly IConfigHandler config;
        private readonly DispatcherTimer clock = new DispatcherTimer
        {
            Interval = TimeSpan.FromMilliseconds( 1000 / 60f )
        };

        public TimerHandler( IConfigHandler config )
        {
            this.config = config;

            this.TimedEvents = new ObservableCollection<TimedEvent>();

            this.clock.Tick += ( se, ea ) =>
            {
                foreach ( var t in this.TimedEvents.ToList() )
                {
                    t.NotifyOfPropertyChange( "TimeRemaining" );

                    if ( t.TimeRemaining.Ticks == 0 )
                    {
                        if ( this.TimedEventCompleted != null )
                        {
                            this.TimedEventCompleted( t, null );
                        }

                        this.TimedEvents.Remove( t );
                    }
                }
            };
            this.clock.Start();
        }


        public void Clear( string command )
        {
            if ( this.TimedEvents.Any( a => a.Command == command ) )
            {
                this.TimedEvents.Remove( this.TimedEvents.Single( a => a.Command == command ) );
            }
        }

        public void Start( string command )
        {
            this.Clear( command );

            var duration = this.config.DurationForCommand( command );
            this.TimedEvents.Add( new TimedEvent( command, duration ) );
        }
    }
}