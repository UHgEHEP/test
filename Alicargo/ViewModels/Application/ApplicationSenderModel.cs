﻿using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Localization;
using Resources;

namespace Alicargo.ViewModels.Application
{
	public sealed class ApplicationSenderModel
	{
		public ApplicationSenderModel()
		{
			Currency = new CurrencyModel();
		}

		[DisplayNameLocalized(typeof (Entities), "Invoice")]
		public byte[] InvoiceFile { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Packing")]
		public byte[] PackingFile { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Swift")]
		public byte[] SwiftFile { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Invoice")]
		public string InvoiceFileName { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Swift")]
		public string SwiftFileName { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Packing")]
		public string PackingFileName { get; set; }

		[Required, DisplayNameLocalized(typeof (Entities), "FactoryName")]
		public string FactoryName { get; set; }

		[Required, DisplayNameLocalized(typeof (Entities), "Mark")]
		public string MarkName { get; set; }

		[Required, DisplayNameLocalized(typeof (Entities), "Invoice")]
		public string Invoice { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Weigth")]
		public float? Weigth { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Count")]
		public int? Count { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Volume"), Required]
		public float Volume { get; set; }

		[Required, DisplayNameLocalized(typeof (Entities), "Value")]
		public CurrencyModel Currency { get; set; }

		[DisplayNameLocalized(typeof (Entities), "FactureCost")]
		public decimal? FactureCost { get; set; }

		[DisplayNameLocalized(typeof (Entities), "WithdrawCost")]
		public decimal? WithdrawCost { get; set; }

		[DisplayNameLocalized(typeof (Entities), "Country")]
		public long? CountryId { get; set; }
	}
}