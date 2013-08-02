using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeagueTag.Handlers
{
    public interface ICommandHandler
    {
        event EventHandler CommandRecognized;
    }
}
