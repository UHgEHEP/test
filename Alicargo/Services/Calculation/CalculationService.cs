﻿using System.Linq;
using System.Web;
using Alicargo.Contracts.Repositories;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Calculation;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Services.Calculation
{
	public sealed class CalculationService : ICalculationService
	{
		private readonly IApplicationRepository _applicationRepository;
		private readonly IAwbRepository _awbRepository;
		private readonly IClientRepository _clientRepository;

		public CalculationService(
			IApplicationRepository applicationRepository,
			IAwbRepository awbRepository,
			IClientRepository clientRepository)
		{
			_applicationRepository = applicationRepository;
			_awbRepository = awbRepository;
			_clientRepository = clientRepository;
		}

		public CalculationAwb[] List(int take, long skip)
		{
			var awbs = _awbRepository.GetRange(take, skip);
			var applications = _applicationRepository.GetByAirWaybill(awbs.Select(x => x.Id).ToArray());
			var nics = _clientRepository.GetNicByApplications(applications.Select(x => x.Id).ToArray());

			return awbs.Select(x => new CalculationAwb
			{
				AwbDisplay = HttpUtility.HtmlDecode(AwbHelper.GetAirWayBillDisplay(x)),
				Rows = applications.Where(a => a.AirWaybillId == x.Id)
								   .Select(a => new CalculationListItem
								   {
									   ApplicationId = a.Id,
									   Value = a.Value,
									   Count = a.Count,
									   ClientNic = nics[a.Id],
									   Factory = a.FactoryName,
									   FactureCost = a.FactureCost,
									   Invoice = a.Invoice,
									   Mark = a.MarkName,
									   ScotchCost = a.ScotchCost,
									   TariffPerKg = a.TariffPerKg,
									   TransitCost = a.TransitCost,
									   ValueCurrencyId = a.CurrencyId,
									   Weigth = a.Weigth,
									   WithdrawCost = a.WithdrawCost
								   }).ToArray(),
				TotalCostOfSenderForWeight = x.TotalCostOfSenderForWeight
			}).ToArray();
		}
	}
}