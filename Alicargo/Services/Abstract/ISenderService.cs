﻿using Alicargo.ViewModels.User;

namespace Alicargo.Services.Abstract
{
	public interface ISenderService
	{
		SenderModel Get(long id);
		long Add(SenderModel model);
		void Update(long id, SenderModel model);
	}
}