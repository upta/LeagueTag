using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LeagueTag.Handlers
{
    class TestConfigHandler : BaseConfigHandler
    {
        public TestConfigHandler()
        {
            //this.durations.Add( "baron", new TimeSpan( 0, 0, 2 ) );
            //this.durations.Add( "dragon", new TimeSpan( 0, 0, 2 ) );
            //this.durations.Add( "our blue", new TimeSpan( 0, 0, 2 ) );
            //this.durations.Add( "our red", new TimeSpan( 0, 0, 2 ) );
            //this.durations.Add( "their blue", new TimeSpan( 0, 0, 2 ) );
            //this.durations.Add( "their red", new TimeSpan( 0, 0, 2 ) );
        }

        public override void Load()
        {
        }

        public override void Save()
        {
        }
    }
}
