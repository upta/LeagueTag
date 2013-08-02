namespace LeagueTag
{
	using System;
	using System.Collections.Generic;
	using Caliburn.Micro;
    using LeagueTag.Handlers;

	public class AppBootstrapper : BootstrapperBase
	{
		SimpleContainer container;

		public AppBootstrapper()
		{
			Start();
		}

		protected override void Configure()
		{
            var savePath = System.IO.Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "LeagueTag" );
            var config = new JsonConfigHandler( savePath );
            config.Load();

            //var config = new TestConfigHandler();

			container = new SimpleContainer();
            
			container.Singleton<IWindowManager, WindowManager>();
			container.Singleton<IEventAggregator, EventAggregator>();
            container.Instance<IConfigHandler>( config );
            container.Instance<ITimerHandler>( new TimerHandler( config ) );
            container.Singleton<IPlaybackHandler, PlaybackHandler>();
            container.Singleton<ICommandHandler, VoiceCommandHandler>();
			container.PerRequest<IShell, ShellViewModel>();
		}

		protected override object GetInstance(Type service, string key)
		{
			var instance = container.GetInstance(service, key);
			if (instance != null)
				return instance;

			throw new InvalidOperationException("Could not locate any instances.");
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return container.GetAllInstances(service);
		}

		protected override void BuildUp(object instance)
		{
			container.BuildUp(instance);
		}

		protected override void OnStartup(object sender, System.Windows.StartupEventArgs e)
		{
			DisplayRootViewFor<IShell>();
		}
	}
}