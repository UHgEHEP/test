﻿using System;
using Alicargo.Core.Models;
using Alicargo.ViewModels;
using Alicargo.ViewModels.Application;

namespace Alicargo.Services.Abstract
{
	public interface IApplicationManager
	{
		ApplicationEditModel Get(long id);
		void Update(long applicationId, ApplicationEditModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel);
		long Add(ApplicationEditModel model, CarrierSelectModel carrierModel, TransitEditModel transitModel);
		void Delete(long id);
		void SetState(long applicationId, long stateId);
		void SetTransitReference(long id, string transitReference);
		void SetDateOfCargoReceipt(long id, DateTimeOffset? dateOfCargoReceipt);
	}
}
