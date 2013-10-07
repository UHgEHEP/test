﻿using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Alicargo.App_Start;
using Ninject;
using Ninject.Web.Common;

namespace Alicargo
{
	public /*sealed*/ class MvcApplication : NinjectHttpApplication
	{
		private readonly CancellationTokenSource _jobTokenSource = new CancellationTokenSource();
		readonly string _connectionString = ConfigurationManager.ConnectionStrings["MainConnectionString"].ConnectionString;
		private Task _jobs;

		protected override IKernel CreateKernel()
		{
			var kernel = new StandardKernel();

			CompositionRoot.BindConnection(kernel, _connectionString);

			CompositionRoot.BindDataAccess(kernel, _connectionString, context => HttpContext.Current);

			CompositionRoot.BindServices(kernel);

			JobsHelper.BindJobs(kernel, _connectionString);

			_jobs = JobsHelper.RunJobs(kernel, _jobTokenSource);

			RegisterConfigs(kernel);

			return kernel;
		}

		protected override void OnApplicationStopped()
		{
			if (!_jobTokenSource.Token.IsCancellationRequested)
			{
				// todo: log on exception
				_jobTokenSource.Cancel(false);

				_jobs.Wait(JobsHelper.PausePeriod.Add(JobsHelper.PausePeriod));
			}
		}

		private static void RegisterConfigs(IKernel kernel)
		{
			AreaRegistration.RegisterAllAreas();

			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters, kernel);

			RouteConfig.RegisterRoutes(RouteTable.Routes);

			BinderConfig.RegisterBinders(System.Web.Mvc.ModelBinders.Binders);
		}
	}
}