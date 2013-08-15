﻿using System;
using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface ICarrierRepository
	{
		CarrierData[] GetAll();
		Func<long> Add(CarrierData carrier);
		CarrierData Get(string name);
	}
}
