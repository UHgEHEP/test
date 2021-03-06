﻿using Alicargo.Core.Contracts.Common;
using Alicargo.DataAccess.Contracts.Contracts.Awb;
using Alicargo.DataAccess.Contracts.Contracts.User;
using Alicargo.ViewModels.AirWaybill;

namespace Alicargo.Services.Abstract
{
	public interface IAwbPresenter
	{
		ListCollection<AirWaybillListItem> List(int take, int skip, long? brokerId, long? senderUserId, string language);
		AwbAdminModel Get(long id);
		AirWaybillData GetData(long id);
		AirWaybillAggregate GetAggregate(long id);
		AwbSenderModel GetSenderAwbModel(long id);
		BrokerData GetBroker(long brokerId);
	}
}