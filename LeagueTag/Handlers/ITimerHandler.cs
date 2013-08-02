using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using LeagueTag.Models;

namespace LeagueTag.Handlers
{
    public interface ITimerHandler
    {
        ObservableCollection<TimedEvent> TimedEvents { get; set; }

        event EventHandler TimedEventCompleted;

        void Start( string command );
        void Clear( string command );
    }
}
