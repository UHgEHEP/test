﻿using Alicargo.ViewModels;

namespace Alicargo.Services.Abstract
{
	public interface ITransitService
	{
		long Add(TransitEditModel transit, long? forsedCarrierId);
		void Update(long transitId, TransitEditModel transit, long? forsedCarrierId, long? applicationId);
		void Delete(long transitId);
	}
}