﻿using Alicargo.Areas.Admin.Models;

namespace Alicargo.Areas.Admin.Serivices
{
	public interface IBillManager
	{
		void SaveBill(long id, BillModel model);
	}
}