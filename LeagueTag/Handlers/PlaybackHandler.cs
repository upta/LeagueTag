using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Speech.Synthesis;
using System.Text;
using LeagueTag.Models;

namespace LeagueTag.Handlers
{
    public class PlaybackHandler : IPlaybackHandler
    {
        private readonly SpeechSynthesizer synthesizer;

        public PlaybackHandler()
        {
            this.synthesizer = new SpeechSynthesizer();
            this.synthesizer.SetOutputToDefaultAudioDevice();
        }

        public void PlayCommandAccepted()
        {
            new SoundPlayer( LeagueTag.Properties.Resources.begin_record ).Play();
        }

        public void PlayTimedEventComplete( TimedEvent timedEvent )
        {
            this.synthesizer.SpeakAsync( timedEvent.Command + " is up" );
        }
    }
}
