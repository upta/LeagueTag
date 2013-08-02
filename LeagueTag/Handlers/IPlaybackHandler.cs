using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeagueTag.Models;

namespace LeagueTag.Handlers
{
    public interface IPlaybackHandler
    {
        void PlayCommandAccepted();
        void PlayTimedEventComplete( TimedEvent timedEvent );
    }
}
