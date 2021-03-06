﻿using Alicargo.DataAccess.Contracts.Contracts.Calculation;
using Alicargo.DataAccess.Contracts.Repositories;

namespace Alicargo.DataAccess.Repositories
{
	public sealed class CalculationRepository : ICalculationRepository
	{
		private readonly ISqlProcedureExecutor _executor;

		public CalculationRepository(ISqlProcedureExecutor executor)
		{
			_executor = executor;
		}

		public void Add(CalculationData data, long applicationId)
		{
			_executor.Execute("[dbo].[Calculation_Add]", new
			{
				data.ClientId,
				ApplicationHistoryId = applicationId,
				data.AirWaybillDisplay,
				data.ApplicationDisplay,
				data.MarkName,
				data.Weight,
				data.TariffPerKg,
				data.ScotchCost,
				data.InsuranceRate,
				data.FactureCost,
				data.FactureCostEx,
				data.TransitCost,
				data.PickupCost,
				data.FactoryName,
				data.CreationTimestamp,
				ClassId = data.Class,
				data.Count,
				data.Invoice,
				data.Value,
				data.Profit,
				data.TotalTariffCost
			});
		}

		public CalculationData GetByApplication(long applicationId)
		{
			return _executor.Query<CalculationData>("[dbo].[Calculation_GetByApplication]", new { applicationId });
		}

		public CalculationData[] GetByClient(long clientId)
		{
			return _executor.Array<CalculationData>("[dbo].[Calculation_GetByClient]", new { clientId });
		}

		public void RemoveByApplication(long applicationId)
		{
			_executor.Execute("[dbo].[Calculation_RemoveByApplication]", new { applicationId });
		}
	}
}