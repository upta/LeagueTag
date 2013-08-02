using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using LeagueTag.Handlers;

namespace LeagueTag
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, DispatcherTimer> runningCommands = new Dictionary<string, DispatcherTimer>();
        private readonly SoundPlayer commandAcceptedSound = new SoundPlayer( LeagueTag.Properties.Resources.begin_record );
        private readonly SpeechSynthesizer synthesizer = new SpeechSynthesizer();

        public MainWindow()
        {
            InitializeComponent();

            var config = new JsonConfigHandler( System.IO.Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "LeagueTag" ) );
            //config.Populate();
            config.Save();
            //config.Save(
            return;
            var engine = new SpeechRecognitionEngine();

            var builder = new GrammarBuilder();
            builder.Append( "tag" );
            builder.Append( new Choices( "baron", "dragon" ) );

            engine.RequestRecognizerUpdate();
            engine.LoadGrammar( new Grammar( builder ) );

            engine.SpeechRecognized += engine_SpeechRecognized;

            engine.SetInputToDefaultAudioDevice();
            engine.RecognizeAsync( RecognizeMode.Multiple );

            CompositionTarget.Rendering += CompositionTarget_Rendering;

            this.DataContext = this;
        }

        void CompositionTarget_Rendering( object sender, EventArgs e )
        {
            
        }

        void engine_SpeechRecognized( object sender, SpeechRecognizedEventArgs e )
        {
            if ( e.Result.Words.Count < 2 )
                return;

            var command = e.Result.Words[ 1 ].Text;

            if ( runningCommands.ContainsKey( command ) )
            {
                runningCommands[ command ].Stop();

                runningCommands.Remove( command );
            }

            var timer = new DispatcherTimer
            {
                Tag = command
            };
            timer.Tick += timer_Tick;

            this.runningCommands.Add( command, timer );

            if ( command == "baron" )
            {
                timer.Interval = new TimeSpan( 0, 6, 45 );
            }
            else if ( command == "dragon" )
            {
                timer.Interval = new TimeSpan( 0, 5, 45 );
            }

            commandAcceptedSound.Play();
            timer.Start();
        }

        void timer_Tick( object sender, EventArgs e )
        {
            var timer = sender as DispatcherTimer;
            var command = timer.Tag.ToString();

            this.synthesizer.SpeakAsync( command + " is up" );

            if ( runningCommands.ContainsKey( command ) )
            {
                runningCommands[ command ].Stop();

                runningCommands.Remove( command );
            }
        }
    }
}
