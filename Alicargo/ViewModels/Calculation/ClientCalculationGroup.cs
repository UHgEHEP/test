﻿namespace Alicargo.ViewModels.Calculation
{
	public sealed class ClientCalculationGroup
	{
		// ReSharper disable InconsistentNaming

		public string field { get { return "AirWaybillId"; } }
		public string value { get; set; }
		public bool hasSubgroups { get { return false; } }
		public ClientCalculationItem[] items { get; set; }
		public object aggregates { get; set; }

		// ReSharper restore InconsistentNaming

		//public sealed class Aggregates
		//{
		//	public Aggregates(int count, float weigth, decimal value)
		//	{
		//		Value = new Holder<decimal>(value);
		//		Count = new Holder<int>(count);
		//		Weigth = new Holder<float>(weigth);
		//	}

		//	public Aggregates(CalculationItem[] items)
		//	{
		//		Count = new Holder<int>(items.Sum(x => x.Count ?? 0));
		//		Weigth = new Holder<float>(items.Sum(x => x.Weigth ?? 0));
		//		Value = new Holder<decimal>(items.Sum(x => x.Value));
		//		TotalTariffCost = new Holder<decimal>(items.Sum(x => x.TotalTariffCost));
		//		TotalSenderRate = new Holder<decimal>(items.Sum(x => x.TotalSenderRate));
		//		ScotchCost = new Holder<decimal>(items.Sum(x => x.ScotchCost ?? 0));
		//		TransitCost = new Holder<decimal>(items.Sum(x => x.TransitCost ?? 0));
		//		InsuranceCost = new Holder<decimal>(items.Sum(x => x.InsuranceCost));
		//		Profit = new Holder<decimal>(items.Sum(x => x.Profit));
		//	}

		//	public Holder<decimal> TotalSenderRate { get; private set; }
		//	public Holder<int> Count { get; private set; }
		//	public Holder<float> Weigth { get; private set; }
		//	public Holder<decimal> Value { get; private set; }
		//	public Holder<decimal> TotalTariffCost { get; private set; }
		//	public Holder<decimal> ScotchCost { get; private set; }
		//	public Holder<decimal> TransitCost { get; private set; }
		//	public Holder<decimal> InsuranceCost { get; private set; }
		//	public Holder<decimal> Profit { get; private set; }
		//}

		//public sealed class Holder<T>
		//{
		//	public readonly T sum;

		//	public Holder(T value)
		//	{
		//		sum = value;
		//	}
		//}		
	}
}