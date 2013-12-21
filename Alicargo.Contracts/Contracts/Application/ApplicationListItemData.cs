﻿using System;

namespace Alicargo.Contracts.Contracts.Application
{
	public sealed class ApplicationListItemData
	{
		#region Additional

		public string ClientLegalEntity { get; set; }

		public string ClientNic { get; set; }

		public string AirWaybill { get; set; }

		public string TransitCity { get; set; }

		public string TransitAddress { get; set; }

		public string TransitRecipientName { get; set; }

		public string TransitPhone { get; set; }

		public string TransitWarehouseWorkingTime { get; set; }

		public int TransitMethodOfTransitId { get; set; }

		public int TransitDeliveryTypeId { get; set; }

		public string TransitCarrierName { get; set; }

		#endregion

		#region Application Data

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

		public string FactoryName { get; set; }

		public string FactoryPhone { get; set; }

		public string FactoryEmail { get; set; }

		public string FactoryContact { get; set; }

		public string MarkName { get; set; }

		public string TransitReference { get; set; }

		public long StateId { get; set; }

		public long? AirWaybillId { get; set; }

		public long? SenderId { get; set; }

		public long? CountryId { get; set; }

		public int MethodOfDeliveryId { get; set; }

		public int? ClassId { get; set; }

		public int CurrencyId { get; set; }

		public decimal Value { get; set; }

		public long TransitId { get; set; }

		public decimal? FactureCost { get; set; }

		public decimal? SenderFactureCost { get; set; }

		public decimal? ScotchCost { get; set; }

		public decimal? SenderScotchCost { get; set; }

		public decimal? TariffPerKg { get; set; }

		public decimal? TransitCost { get; set; }

		public decimal? ForwarderTransitCost { get; set; }

		public decimal? PickupCost { get; set; }

		public decimal? SenderPickupCost { get; set; }

		public decimal? SenderRate { get; set; }

		public long ClientId { get; set; }

		#endregion
	}
}
