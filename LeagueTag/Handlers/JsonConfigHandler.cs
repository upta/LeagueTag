using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LeagueTag.Models;
using Newtonsoft.Json;

namespace LeagueTag.Handlers
{
    class JsonConfigHandler : BaseConfigHandler
    {
        private readonly string path;

        public JsonConfigHandler( string folder )
        {
            if ( !Directory.Exists( folder ) )
            {
                Directory.CreateDirectory( folder );
            }

            this.path = Path.Combine( folder, "config.json" );
        }

        public void Populate()
        {
            //this.durations.Add( "baron", new TimeSpan( 0, 7, 0 ) );
            //this.durations.Add( "dragon", new TimeSpan( 0, 6, 0 ) );
            //this.durations.Add( "our blue", new TimeSpan( 0, 5, 0 ) );
            //this.durations.Add( "our red", new TimeSpan( 0, 5, 0 ) );
            //this.durations.Add( "their blue", new TimeSpan( 0, 5, 0 ) );
            //this.durations.Add( "their red", new TimeSpan( 0, 5, 0 ) );
        }

        public override void Load()
        {
            var json = File.ReadAllText( this.path );

            this.Config = JsonConvert.DeserializeObject<Config>( json );
        }

        public override void Save()
        {
            var json = JsonConvert.SerializeObject( this.Config, Formatting.Indented );

            File.WriteAllText( this.path, json );
        }
    }
}
