﻿using System;
using System.Collections.Generic;
using Alicargo.Core.AirWaybill;
using Alicargo.Core.Contracts.AirWaybill;
using Alicargo.Core.Contracts.Client;
using Alicargo.DataAccess.Contracts.Enums;
using Alicargo.Services.Abstract;
using Alicargo.Services.AirWaybill;
using Alicargo.Services.Application;
using Alicargo.Services.Calculation;
using Alicargo.Services.Email;
using Alicargo.Services.Users.Client;
using Ninject;
using Ninject.Syntax;
using Ninject.Web.Common;

namespace Alicargo.App_Start
{
	public static class CompositionRootHelper
	{
		public static readonly IDictionary<Type, Type[]> Decorators = new Dictionary<Type, Type[]>
		{
			{
				typeof(IAwbManager),
				new[]
				{
					typeof(AwbManager),
					typeof(AwbManagerWithMailing)
				}
			},
			{
				typeof(IAwbUpdateManager),
				new[]
				{
					typeof(AwbUpdateManager),
					typeof(AwbUpdateGtdManager),
					typeof(AwbUpdateManagerWithMailing)
				}
			},
			{
				typeof(IApplicationAwbManager),
				new[]
				{
					typeof(ApplicationAwbManager),
					typeof(ApplicationAwbManagerWithMailing)
				}
			},
			{
				typeof(IApplicationManager),
				new[]
				{
					typeof(ApplicationManager),
					typeof(ApplicationManagerWithEvent)
				}
			},
			{
				typeof(IClientManager),
				new[]
				{
					typeof(ClientManager),
					typeof(ClientManagerWithEvent),
					typeof(ClientManagerWithMailing)
				}
			},
			{
				typeof(IClientBalance),
				new[]
				{
					typeof(ClientBalance),
					typeof(ClientBalanceWithEvent)
				}
			},
			{
				typeof(ICalculationService),
				new[]
				{
					typeof(CalculationService),
					typeof(CalculationServiceWithBalace),
					typeof(CalculationServiceWithEvent)
				}
			}
		};

		public static IEnumerable<Type> BindDecorators(IKernel kernel, IDictionary<Type, Type[]> settings)
		{
			var binded = new HashSet<Type>();

			foreach (var item in settings)
			{
				var @interface = item.Key;
				var decorators = item.Value;

				for (var i = 0; i < decorators.Length; i++)
				{
					var current = decorators[i];
					if (i == decorators.Length - 1)
					{
						kernel.Bind(@interface).To(current).InRequestScope();
					}
					else
					{
						var next = decorators[i + 1];
						kernel.Bind(@interface).To(current).WhenInjectedInto(next).InRequestScope();
					}

					binded.Add(current);
				}
			}

			kernel.Bind<IClientBalance>().To<ClientBalance>().WhenInjectedInto<CalculationServiceWithBalace>();

			return binded;
		}

		public static Func<string> GetLanguage(IResolutionRoot kernel)
		{
			return () => kernel.Get<IIdentityService>().Language
						 ?? TwoLetterISOLanguageName.Russian;
		}
	}
}