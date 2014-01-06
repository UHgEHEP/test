﻿using System.Collections.Generic;
using System.Linq;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.Application;
using Alicargo.Contracts.Repositories;
using Alicargo.Contracts.Repositories.Application;
using Alicargo.Contracts.Repositories.User;
using Alicargo.Core.Enums;
using Alicargo.Core.Helpers;
using Alicargo.Services.Abstract;
using Alicargo.ViewModels.Calculation;
using Alicargo.ViewModels.Calculation.Admin;
using Alicargo.ViewModels.Helpers;

namespace Alicargo.Services.Calculation
{
	public sealed class AdminCalculationPresenter : IAdminCalculationPresenter
	{
		private readonly IApplicationRepository _applications;
		private readonly IAwbRepository _awbs;
		private readonly IClientRepository _clients;
		private readonly ISenderRepository _senders;
		private readonly IClientBalanceRepository _balances;

		public AdminCalculationPresenter(
			IApplicationRepository applications,
			IAwbRepository awbs,
			ISenderRepository senders,
			IClientRepository clients,
			IClientBalanceRepository balances)
		{
			_applications = applications;
			_awbs = awbs;
			_senders = senders;
			_clients = clients;
			_balances = balances;
		}

		public CalculationListCollection List(int take, long skip)
		{
			var awbs = _awbs.GetRange(take, skip).OrderByDescending(awb => awb.CreationTimestamp).ToArray();

			return List(awbs);
		}

		public CalculationListCollection Row(long awbId)
		{
			var data = _awbs.Get(awbId);

			return List(data);
		}

		private CalculationListCollection List(IList<AirWaybillData> data)
		{
			var awbs = data.ToDictionary(x => x.Id, x => x);

			var applications = _applications.GetByAirWaybill(awbs.Select(x => x.Key).ToArray()).ToArray();

			var tariffs = _senders.GetTariffs(applications.Select(x => x.SenderId).ToArray());

			var items = GetItems(applications, tariffs);

			var info = GetInfo(data, applications, tariffs);

			var groups = GetGroups(data, items);

			return new CalculationListCollection
			{
				Groups = groups.ToArray(),
				Total = _awbs.Count(),
				Info = info,
				TotalBalance = _balances.SumBalance().ToString("N2")
			};
		}

		private static CalculationAwbInfo[] GetInfo(
			IEnumerable<AirWaybillData> awbs,
			IEnumerable<ApplicationData> items,
			IReadOnlyDictionary<long, decimal> tariffs)
		{
			return awbs.Select(awb =>
			{
				var rows = items.Where(a => a.AirWaybillId == awb.Id).ToArray();

				var info = new CalculationAwbInfo
				{
					AirWaybillId = awb.Id,
					TotalCostOfSenderForWeight = awb.TotalCostOfSenderForWeight ?? 0,
					FlightCost = awb.FlightCost ?? 0,
					CustomCost = awb.CustomCost ?? 0,
					BrokerCost = awb.BrokerCost ?? 0,
					AdditionalCost = awb.AdditionalCost,
					TotalSenderRate = rows.Sum(x => CalculationHelper.GetTotalSenderRate(x.SenderRate, x.Weight)),
					TotalScotchCost = rows.Sum(x => CalculationHelper.GetSenderScotchCost(tariffs, x.SenderId, x.Count) ?? 0),
					TotalFactureCost = rows.Sum(x => x.FactureCost ?? 0),
					TotalPickupCost = rows.Sum(x => x.PickupCost ?? 0),
					TotalTransitCost = rows.Sum(x => x.TransitCost ?? 0),
					TotalInsuranceCost = rows.Sum(x => CalculationHelper.GetInsuranceCost(x.Value)),
					BrokerCostPerKg = null,
					CostPerKgOfSender = null,
					CustomCostPerKg = null,
					FlightCostPerKg = null,
					ProfitPerKg = null,
					Profit = 0xBAD
				};

				info.Profit = rows.Sum(x => CalculationHelper.GetProfit(x, tariffs)) - info.TotalExpenses;

				var totalWeight = (decimal)rows.Sum(x => x.Weight ?? 0);

				if (totalWeight != 0)
				{
					info.ProfitPerKg = info.Profit / totalWeight;
					info.CostPerKgOfSender = info.TotalSenderRate / totalWeight;
					info.FlightCostPerKg = info.FlightCost / totalWeight;
					info.CustomCostPerKg = info.CustomCost / totalWeight;
					info.BrokerCostPerKg = info.BrokerCost / totalWeight;
				}

				return info;
			}).ToArray();
		}

		private static List<CalculationGroup> GetGroups(IEnumerable<AirWaybillData> data, IEnumerable<CalculationItem> items)
		{
			return data.Select(awb =>
			{
				var rows = items.Where(a => a.AirWaybillId == awb.Id).ToArray();

				return new CalculationGroup
				{
					AirWaybillId = awb.Id,
					items = rows,
					value = new { id = awb.Id, text = AwbHelper.GetAirWaybillDisplay(awb) },
					aggregates = new CalculationGroup.Aggregates(rows)
				};
			}).ToList();
		}

		private IEnumerable<CalculationItem> GetItems(
			ApplicationData[] applications,
			IReadOnlyDictionary<long, decimal> tariffs)
		{
			var appIds = applications.Select(x => x.Id).ToArray();
			var calculations = _applications.GetCalculations(appIds);
			var nics = _clients.GetNicByApplications(appIds);

			return applications.Select(a => new CalculationItem
			{
				ApplicationId = a.Id,
				Value = a.Value,
				Count = a.Count,
				ClientNic = nics[a.Id],
				Factory = a.FactoryName,
				FactureCost = a.FactureCostEdited ?? a.FactureCost,
				Invoice = a.Invoice,
				Mark = a.MarkName,
				ScotchCost = a.ScotchCostEdited ?? CalculationHelper.GetSenderScotchCost(tariffs, a.SenderId, a.Count),
				TariffPerKg = a.TariffPerKg,
				SenderRate = a.SenderRate,
				TransitCost = a.TransitCostEdited ?? a.TransitCost,
				ValueCurrencyId = a.CurrencyId,
				Weight = a.Weight,
				PickupCost = a.PickupCostEdited ?? a.PickupCost, // ReSharper disable PossibleInvalidOperationException
				AirWaybillId = a.AirWaybillId.Value, // ReSharper restore PossibleInvalidOperationException
				DisplayNumber = ApplicationHelper.GetDisplayNumber(a.Id, a.Count),
				TotalTariffCost = CalculationHelper.GetTotalTariffCost(a.TariffPerKg, a.Weight),
				Profit = CalculationHelper.GetProfit(a, tariffs),
				InsuranceCost = CalculationHelper.GetInsuranceCost(a.Value),
				TotalSenderRate = CalculationHelper.GetTotalSenderRate(a.SenderRate, a.Weight),
				IsCalculated = calculations.ContainsKey(a.Id),
				ClassId = (ClassType?)a.ClassId
			}).OrderBy(x => x.ClientNic).ThenByDescending(x => x.ApplicationId).ToArray();
		}
	}
}