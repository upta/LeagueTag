using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Windows;
using LeagueTag.Models;

namespace LeagueTag.Handlers
{
    public class VoiceCommandHandler : ICommandHandler
    {
        public event EventHandler CommandRecognized;

        private readonly SpeechRecognitionEngine engine;
        private readonly ITimerHandler timerHandler;

        public VoiceCommandHandler( IConfigHandler config, ITimerHandler timerHandler )
        {
            this.engine = new SpeechRecognitionEngine();
            this.timerHandler = timerHandler;

            var builder = new GrammarBuilder();
            builder.Append( new Choices( config.Config.Verbs.ToArray() ) );
            builder.Append( new Choices( config.Commands.ToArray() ) );

            this.engine.RequestRecognizerUpdate();
            this.engine.LoadGrammar( new Grammar( builder ) );

            this.engine.SpeechRecognized += engine_SpeechRecognized;

            this.engine.SetInputToDefaultAudioDevice();
            this.engine.RecognizeAsync( RecognizeMode.Multiple );
        }

        
        private void engine_SpeechRecognized( object sender, SpeechRecognizedEventArgs e )
        {
            if ( e.Result.Words.Count < 2 || e.Result.Confidence < 0.9 )
            {
                return;
            }

            if ( this.CommandRecognized != null )
            {
                var command = new Command
                {
                    Verb = e.Result.Words[ 0 ].Text,
                    Noun = e.Result.Text.Replace( e.Result.Words[ 0 ].Text, "" ).Trim()
                };

                this.CommandRecognized( command, null );
            }
        }
    }
}
