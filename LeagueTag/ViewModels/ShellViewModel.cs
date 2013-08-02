using Caliburn.Micro;
using LeagueTag.Handlers;
using LeagueTag.Models;
namespace LeagueTag 
{
    public class ShellViewModel : PropertyChangedBase, IShell 
    {
        public ITimerHandler TimerHandler { get; set; }

        private readonly IPlaybackHandler playbackHandler;

        public ShellViewModel( ITimerHandler timerHandler, 
                               IPlaybackHandler playbackHandler, 
                               ICommandHandler commandHandler )
        {
            this.TimerHandler = timerHandler;
            this.playbackHandler = playbackHandler;

            this.TimerHandler.TimedEventCompleted += ( se, ea ) =>
            {
                this.playbackHandler.PlayTimedEventComplete( se as TimedEvent );
            };

            commandHandler.CommandRecognized += ( se, ea ) =>
            {
                this.playbackHandler.PlayCommandAccepted();

                var command = se as Command;

                if ( command.Verb == "time" )
                {
                    this.TimerHandler.Start( command.Noun );
                }
                else if ( command.Verb == "clear" )
                {
                    this.TimerHandler.Clear( command.Noun );
                }
            };
        }

        public void AddBaron()
        {
            //this.TimerHandler.Start( "baron" );
        }

        public void AddDragon()
        {
            //this.TimerHandler.Start( "dragon" );
        }
    }
}