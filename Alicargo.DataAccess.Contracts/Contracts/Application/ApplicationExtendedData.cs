﻿using System;
using Alicargo.DataAccess.Contracts.Enums;

namespace Alicargo.DataAccess.Contracts.Contracts.Application
{
	public sealed class ApplicationExtendedData
	{
		public long Id { get; set; }
		public DateTimeOffset CreationTimestamp { get; set; }
		public string Invoice { get; set; }
		public string Characteristic { get; set; }
		public string AddressLoad { get; set; }
		public string WarehouseWorkingTime { get; set; }
		public float? Weight { get; set; }
		public int? Count { get; set; }
		public float Volume { get; set; }
		public string TermsOfDelivery { get; set; }
		public DateTimeOffset StateChangeTimestamp { get; set; }
		public DateTimeOffset? DateInStock { get; set; }
		public DateTimeOffset? DateOfCargoReceipt { get; set; }
		public string TransitReference { get; set; }
		public long StateId { get; set; }
		public MethodOfDelivery MethodOfDelivery { get; set; }
		public ClassType? Class { get; set; }
		public int CurrencyId { get; set; }
		public decimal Value { get; set; }
		public long ForwarderId { get; set; }
		public string ForwarderName { get; set; }
		public int DisplayNumber { get; set; }

		public long ClientId { get; set; }
		public string ClientLegalEntity { get; set; }
		public string ClientNic { get; set; }
		public long ClientUserId { get; set; }
		public string ClientEmails { get; set; }

		public long? AirWaybillId { get; set; }
		public string AirWaybill { get; set; }
		public string AirWaybillGTD { get; set; }
		public DateTimeOffset? AirWaybillDateOfDeparture { get; set; }
		public DateTimeOffset? AirWaybillDateOfArrival { get; set; }

		public long TransitId { get; set; }
		public long TransitCityId { get; set; }
		public string TransitAddress { get; set; }
		public string TransitRecipientName { get; set; }
		public string TransitPhone { get; set; }
		public string TransitWarehouseWorkingTime { get; set; }
		public MethodOfTransit TransitMethodOfTransit { get; set; }
		public DeliveryType TransitDeliveryType { get; set; }
		public long CarrierId { get; set; }
		public string CarrierName { get; set; }
		public string CarrierPhone { get; set; }
		public string CarrierAddress { get; set; }
		public string CarrierContact { get; set; }
		public string CarrierEmail { get; set; }

		public string FactoryName { get; set; }
		public string FactoryPhone { get; set; }
		public string FactoryEmail { get; set; }
		public string FactoryContact { get; set; }
		public string MarkName { get; set; }		

		public decimal? FactureCost { get; set; }
		public decimal? FactureCostEdited { get; set; }
		public decimal? FactureCostExEdited { get; set; }
		public decimal? FactureCostEx { get; set; }		
		public decimal? ScotchCostEdited { get; set; }
		public decimal? SenderScotchCost { get; set; }
		public decimal? TariffPerKg { get; set; }
		public decimal? TransitCostEdited { get; set; }
		public decimal? TransitCost { get; set; }
		public decimal? PickupCostEdited { get; set; }
		public float InsuranceRate { get; set; }
		public decimal? CalculationTotalTariffCost { get; set; }
		public decimal? CalculationProfit { get; set; }

		public long SenderId { get; set; }
		public long CountryId { get; set; }
		public decimal? PickupCost { get; set; }
		public decimal? SenderRate { get; set; }
		public string SenderName { get; set; }
		public string SenderContact { get; set; }
		public string SenderPhone { get; set; }
		public string SenderAddress { get; set; }
		public string SenderEmail { get; set; }

		public decimal? GetAdjustedPickupCost()
		{
			return PickupCostEdited ?? PickupCost;
		}

		public decimal? GetAdjustedTransitCost()
		{
			return TransitCostEdited ?? TransitCost;
		}

		public decimal? GetAdjustedScotchCost()
		{
			return ScotchCostEdited ?? SenderScotchCost;
		}

		public decimal? GetAdjustedFactureCost()
		{
			return FactureCostEdited ?? FactureCost;
		}

		public decimal? GetAdjustedFactureCostEx()
		{
			return FactureCostExEdited ?? FactureCostEx;
		}
	}
}