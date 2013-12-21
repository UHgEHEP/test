﻿using System.Collections.Generic;
using Alicargo.Contracts.Contracts.User;

namespace Alicargo.Contracts.Repositories.User
{
	public interface ISenderRepository
	{
		long? GetByUserId(long userId);
		SenderData Get(long senderId);
		Dictionary<long, decimal> GetTariffs(params long[] ids);
		long Add(SenderData data, string password);
		void Update(long senderId, SenderData data);
		long GetUserId(long senderId);
		UserData[] GetAll();
	}
}