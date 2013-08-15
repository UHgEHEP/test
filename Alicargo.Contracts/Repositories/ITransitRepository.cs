﻿using System;
using Alicargo.Contracts.Contracts;

namespace Alicargo.Contracts.Repositories
{
	public interface ITransitRepository
	{
		Func<long> Add(TransitData transit);
		void Update(TransitData transit);
		TransitData[] Get(params long[] ids);
		long? GetaApplicationId(long id);
		TransitData GetByApplication(long id);
		TransitData GetByClient(long clientId);
		void Delete(long transitId);
	}
}
