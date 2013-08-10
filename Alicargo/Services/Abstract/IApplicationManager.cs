﻿using System;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationManager
	{
		ApplicationEditModel Get(long id);
		void Update(long id, ApplicationEditModel model, CarrierSelectModel carrierSelectModel);
		long Add(ApplicationEditModel model, CarrierSelectModel carrierSelectModel);
		void Delete(long id);
		void SetState(long applicationId, long stateId);
		void SetTransitReference(long id, string transitReference);
		void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt);
	}
}
