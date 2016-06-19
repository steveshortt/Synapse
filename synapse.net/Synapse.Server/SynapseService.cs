﻿using System;
using System.ServiceModel;
using System.ServiceProcess;

namespace Synapse.Server
{
	public partial class SynapseService : ServiceBase
	{
		public ServiceHost _serviceHost = null;

		public SynapseService()
		{
			InitializeComponent();
		}

		static void Main()
		{
#if DEBUG
			SynapseService s = new SynapseService();
			s.OnDebugMode_Start();
			System.Threading.Thread.Sleep( System.Threading.Timeout.Infinite );
			s.OnDebugMode_Stop();
#else
			ServiceBase.Run( new SynapseService() );
#endif
		}

		public void OnDebugMode_Start()
		{
			OnStart( null );
		}
		public void OnDebugMode_Stop()
		{
			OnStop();
		}

		protected override void OnStart(string[] args)
		{
			if( _serviceHost != null )
			{
				_serviceHost.Close();
			}

			_serviceHost = new ServiceHost( typeof( SynapseServer ) );
			_serviceHost.Open();
		}

		protected override void OnStop()
		{
			_serviceHost.Close();
		}
	}
}