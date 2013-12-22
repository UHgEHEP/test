﻿using System.Collections.Generic;
using Alicargo.Contracts.Contracts;
using Alicargo.Contracts.Contracts.State;

namespace Alicargo.Contracts.Repositories
{
	public interface IStateRepository
	{
		long Add(string language, StateData data);
		IReadOnlyDictionary<long, StateData> Get(string language, params long[] ids);
		StateListItem[] All();
		void Update(long id, string language, StateData data);
		void Delete(long id);
	}
}