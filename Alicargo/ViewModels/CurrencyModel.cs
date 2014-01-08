﻿using System.ComponentModel.DataAnnotations;
using Alicargo.Core.Enums;
using Alicargo.Core.Resources;
using Alicargo.Utilities.Localization;

namespace Alicargo.ViewModels
{
	public sealed class CurrencyModel
	{
		public CurrencyModel()
		{
			CurrencyId = (int)CurrencyType.Euro;
		}

		[Required, DisplayNameLocalized(typeof(Entities), "Value")]
		public decimal Value { get; set; }

		[Required]
		public int CurrencyId { get; set; }
	}
}