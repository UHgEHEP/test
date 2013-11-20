﻿using System;
using Alicargo.Core.Enums;

namespace Alicargo.Core.Services.Abstract
{
	public interface ILocalizationService
	{
		string GetDate(DateTimeOffset? date, string culture, TimeSpan? timeZone = null);
		string GetMethodOfDelivery(MethodOfDelivery methodOfDelivery, string culture);
		string GetMethodOfTransit(MethodOfTransit methodOfTransit, string culture);
		string GetDeliveryType(DeliveryType deliveryType, string culture);
	}
}
