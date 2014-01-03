﻿using System;

namespace Alicargo.Jobs.Balance.Entities
{
	public sealed class PaymentEventData
	{
		public decimal AbsMoney { get; set; }
		public string Comment { get; set; }
		public DateTimeOffset Timestamp { get; set; }
	}
}