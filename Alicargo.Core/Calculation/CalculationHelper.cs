﻿using System.Collections.Generic;
using Alicargo.DataAccess.Contracts.Contracts.Application;

namespace Alicargo.Core.Calculation
{
	public static class CalculationHelper
	{
		public static decimal GetInsuranceCost(decimal value, float insuranceRate)
		{
			return value * (decimal)insuranceRate;
		}

		public static decimal GetProfit(ApplicationEditData application, IReadOnlyDictionary<long, decimal> tariffs)
		{
			if(application.CalculationProfit != null)
			{
				return application.CalculationProfit.Value;
			}

			var totalTariffCost = GetTotalTariffCost(
				application.CalculationTotalTariffCost,
				application.TariffPerKg,
				application.Weight);

			var scotchCost = (application.ScotchCostEdited ?? GetSenderTapeTariff(tariffs, application.SenderId)) * application.Count;

			return totalTariffCost
			       + (scotchCost ?? 0)
			       + GetInsuranceCost(application.Value, application.InsuranceRate)
			       + (application.FactureCostEdited ?? application.FactureCost ?? 0)
			       + (application.FactureCostExEdited ?? application.FactureCostEx ?? 0)
			       + (application.PickupCostEdited ?? application.PickupCost ?? 0)
			       + (application.TransitCostEdited ?? application.TransitCost ?? 0);
		}

		public static decimal? GetSenderTapeTariff(
			IReadOnlyDictionary<long, decimal> tariffs,
			long? senderId)
		{
			// for senders should not display custom cost for tape

			return tariffs != null && senderId.HasValue && tariffs.ContainsKey(senderId.Value)
				? tariffs[senderId.Value]
				: (decimal?)null;
		}

		public static decimal GetTotalSenderRate(decimal? senderRate, float? weight)
		{
			return (senderRate ?? 0) * (decimal)(weight ?? 0);
		}

		public static decimal GetTotalTariffCost(decimal? calculationTotalTariffCost, decimal? tariffPerKg, float? weight)
		{
			return calculationTotalTariffCost ?? ((tariffPerKg ?? 0) * (decimal)(weight ?? 0));
		}
	}
}